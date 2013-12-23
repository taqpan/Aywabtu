using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Aywabtu.Command;
using Aywabtu.Properties;
using GalaSoft.MvvmLight;

namespace Aywabtu.ViewModel {
    public class MainViewModel : ViewModelBase {

        public MainViewModel() {
            LoadCommand.Execute(Settings.Default.LastFile);
        }

        private ObservableCollection<WindowInfoItemViewModel> items = new ObservableCollection<WindowInfoItemViewModel>();
        public ObservableCollection<WindowInfoItemViewModel> Items {
            get { return items; }
            set {
                items = value;
                RaisePropertyChanged("Items");
            }
        }

        private WindowInfoItemViewModel selectedItem;
        public WindowInfoItemViewModel SelectedItem {
            get { return selectedItem; }
            set {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private ICommand newEntryCommand;
        public ICommand NewEntryCommand {
            get { return newEntryCommand ?? (newEntryCommand = new NewEntryCommand(this)); }
        }

        private ICommand arrangeCommand;
        public ICommand ArrangeCommand {
            get { return arrangeCommand ?? (arrangeCommand = new ArrangeCommand(this)); }
        }

        private ICommand scanCommand;
        public ICommand ScanCommand {
            get { return scanCommand ?? (scanCommand = new ScanCommand(this)); }
        }

        private ICommand loadCommand;
        public ICommand LoadCommand {
            get { return loadCommand ?? (loadCommand = new LoadCommand(this)); }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand {
            get { return saveCommand ?? (saveCommand = new SaveCommand(this)); }
        }
    }
}