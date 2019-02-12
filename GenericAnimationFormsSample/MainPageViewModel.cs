using System.ComponentModel;
using System.Windows.Input;

namespace GenericAnimationFormsSample
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        public ICommand ShakeCommand { get; set; }

        public string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                if (_userName?.Length == 5)
                {
                    ShakeCommand.Execute(null);
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
