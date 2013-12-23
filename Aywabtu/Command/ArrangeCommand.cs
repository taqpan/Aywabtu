using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Aywabtu.ViewModel;

namespace Aywabtu.Command {
    public class ArrangeCommand : CommandBase {
        private readonly MainViewModel mainViewModel;

        public ArrangeCommand(MainViewModel mainViewModel) {
            this.mainViewModel = mainViewModel;
        }

        public override bool CanExecute(object parameter) {
            return mainViewModel.Items.Any();
        }

        public override void Execute(object parameter) {
            Task.Factory.StartNew(ExecuteBlocking);
        }

        public void ExecuteBlocking() {
            var tuples = mainViewModel.Items.Select(item =>
                new Tuple<WindowInfoItemViewModel, Regex, Regex>(
                    item,
                    new Regex(item.ProcessNameRegex),
                    new Regex(item.WindowNameRegex))
                ).ToList();

            Win32Api.EnumWindows((hWnd, lparam) => {
                try {
                    int windowTextBufLen = Math.Max(Win32Api.GetWindowTextLength(hWnd), 256);
                    StringBuilder windowName = new StringBuilder(windowTextBufLen);
                    Win32Api.GetWindowText(hWnd, windowName, windowTextBufLen);

                    UInt32 procId;
                    Win32Api.GetWindowThreadProcessId(hWnd, out procId);
                    var proc = Process.GetProcessById((int)procId);

                    foreach (var t in tuples) {
                        if (proc.MainWindowHandle != IntPtr.Zero) {
                            if (t.Item2.IsMatch(proc.ProcessName) &&
                                t.Item3.IsMatch(windowName.ToString())) {
                                Win32Api.MoveWindow(hWnd, t.Item1.Left, t.Item1.Top, t.Item1.Width, t.Item1.Height, 1);
                            }
                        }
                    }

                    return true;
                } catch {
                    return false;
                }
            }, IntPtr.Zero);
        }
    }
}
