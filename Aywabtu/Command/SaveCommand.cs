using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows.Input;
using Aywabtu.Properties;
using Aywabtu.ViewModel;
using Microsoft.Win32;

namespace Aywabtu.Command {
    public class SaveCommand : CommandBase {
        private readonly MainViewModel mainViewModel;

        public SaveCommand(MainViewModel mainViewModel) {
            this.mainViewModel = mainViewModel;
        }

        public override bool CanExecute(object parameter) {
            return mainViewModel.Items.Any();
        }

        public override void Execute(object parameter) {
            var sfd = new SaveFileDialog();
            sfd.Filter = "All files (*.*)|*.*";
            if (!string.IsNullOrWhiteSpace(Settings.Default.LastFile)) {
                sfd.FileName = Settings.Default.LastFile;
            }
            if (sfd.ShowDialog() != true) {
                return;
            }
            Settings.Default.LastFile = sfd.FileName;

            var serializer = new DataContractJsonSerializer(typeof(IEnumerable<WindowInfoItemViewModel>));
            using (var s = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
                serializer.WriteObject(s, mainViewModel.Items);
            }
        }
    }
}
