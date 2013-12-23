using System.Windows.Input;
using Aywabtu.Command;
using GalaSoft.MvvmLight;

namespace Aywabtu.ViewModel {
    public class WindowInfoItemViewModel : ViewModelBase {

        private string processNameRegex;
        public string ProcessNameRegex {
            get { return processNameRegex; }
            set {
                processNameRegex = value;
                RaisePropertyChanged("ProcessNameRegex");
            }
        }

        private string windowNameRegex;
        public string WindowNameRegex {
            get { return windowNameRegex; }
            set {
                windowNameRegex = value;
                RaisePropertyChanged("WindowNameRegex");
            }
        }

        private int left;
        public int Left{
            get { return left; }
            set {
                left = value;
                RaisePropertyChanged("Left");
            }
        }

        private int top;
        public int Top {
            get { return top; }
            set {
                top = value;
                RaisePropertyChanged("Top");
            }
        }

        private int width;
        public int Width {
            get { return width; }
            set {
                width = value;
                RaisePropertyChanged("Width");
            }
        }

        private int height;
        public int Height {
            get { return height; }
            set {
                height = value;
                RaisePropertyChanged("Height");
            }
        }
    }
}
