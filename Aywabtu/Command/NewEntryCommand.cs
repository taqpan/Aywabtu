using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Aywabtu.ViewModel;

namespace Aywabtu.Command {
    public class NewEntryCommand : CommandBase {
        private readonly MainViewModel mainViewModel;

        public NewEntryCommand(MainViewModel mainViewModel) {
            this.mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter) {
            mainViewModel.Items.Add(new WindowInfoItemViewModel {
                ProcessNameRegex = "^Notepad$",
                WindowNameRegex = "^Notepad$",
                Left = 0,
                Top = 0,
                Width = 400,
                Height = 300
            });
        }
    }
}
