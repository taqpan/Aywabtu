using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Input;
using Aywabtu.Properties;
using Aywabtu.ViewModel;
using Microsoft.Win32;

namespace Aywabtu.Command {
    public class LoadCommand : CommandBase {
        private readonly MainViewModel mainViewModel;

        public LoadCommand(MainViewModel mainViewModel) {
            this.mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter) {
            var filename = parameter as string;
            if (filename == null) {
                var sfd = new OpenFileDialog();
                sfd.Filter = "All files (*.*)|*.*";
                if (!string.IsNullOrWhiteSpace(Settings.Default.LastFile)) {
                    sfd.FileName = Settings.Default.LastFile;
                }
                if (sfd.ShowDialog() != true) {
                    return;
                }
                filename = sfd.FileName;
            }
            Settings.Default.LastFile = filename;

            if (!System.IO.File.Exists(filename)) {
                return;
            }

            var serializer = new DataContractJsonSerializer(typeof(IEnumerable<WindowInfoItemViewModel>));
            using (var s = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                var data = serializer.ReadObject(s) as IEnumerable<WindowInfoItemViewModel>;
                if (data != null) {
                    mainViewModel.Items.Clear();
                    foreach (var row in data) {
                        mainViewModel.Items.Add(row);
                    }
                }
            }
        }
    }
}
