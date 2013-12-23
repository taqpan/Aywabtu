using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Aywabtu.ViewModel;

namespace Aywabtu.Command {
    public class ScanCommand : CommandBase {
        private readonly HashSet<string> processNameFilter = new HashSet<string> {
            "explorer", "sidebar", "Aywabtu", "Aywabtu.vshost"
        };
        private readonly MainViewModel mainViewModel;

        public ScanCommand(MainViewModel mainViewModel) {
            this.mainViewModel = mainViewModel;
        }

        public override bool CanExecute(object parameter) {
            return true;
        }

        public override void Execute(object parameter) {
            Task.Factory.StartNew(exec);
        }

        private void exec() {
            App.Current.Dispatcher.Invoke(new Action(
                () => mainViewModel.Items.Clear()));

            Win32Api.EnumWindows((hWnd, lparam) => {
                try {
                    int windowTextBufLen = Math.Max(Win32Api.GetWindowTextLength(hWnd), 256);
                    var windowName = new StringBuilder(windowTextBufLen);
                    Win32Api.GetWindowText(hWnd, windowName, windowTextBufLen);

                    var r = new Win32Api.RECT();
                    Win32Api.GetWindowRect(hWnd, ref r);

                    UInt32 procId;
                    Win32Api.GetWindowThreadProcessId(hWnd, out procId);
                    var proc = Process.GetProcessById((int)procId);

                    var width = r.Right - r.Left;
                    var height = r.Bottom - r.Top;

                    if (width > 1 && height > 1 &&
                        !string.IsNullOrWhiteSpace(proc.ProcessName) &&
                        !string.IsNullOrWhiteSpace(windowName.ToString()) &&
                        !processNameFilter.Contains(proc.ProcessName)) {

                        var item = new WindowInfoItemViewModel {
                            ProcessNameRegex = toRegexString(proc.ProcessName),
                            WindowNameRegex = toRegexString(windowName.ToString()),
                            Left = r.Left,
                            Top = r.Top,
                            Width = width,
                            Height = height,
                        };

                        Application.Current.Dispatcher.Invoke(new Action(
                            () => mainViewModel.Items.Add(item)));
                    }
                    return true;
                } catch {
                    return false;
                }
            }, IntPtr.Zero);
        }

        private string toRegexString(string s) {
            return
                "^"
                + s.Replace(@"\", @"\\")
                   .Replace(@".", @"\.")
                   .Replace(@"+", @"\+")
                   .Replace(@"*", @"\*")
                   .Replace(@"?", @"\?")
                   .Replace(@"[", @"\[")
                   .Replace(@"]", @"\]")
                   .Replace(@"(", @"\(")
                   .Replace(@")", @"\)")
                   .Replace(@"{", @"\{")
                   .Replace(@"}", @"\}")
                   .Replace(@"^", @"\^")
                   .Replace(@"$", @"\$")
                + "$";
        }
    }
}
