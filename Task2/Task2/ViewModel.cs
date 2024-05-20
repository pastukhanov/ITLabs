namespace Task2;

using System.ComponentModel;
using System.Windows.Input;

public class ViewModel : INotifyPropertyChanged
    {
        private readonly Airplane _airplane = new Airplane();
        private readonly Helicopter _helicopter = new Helicopter();

        private string _runwayLengthInput;
        public string RunwayLengthInput
        {
            get { return _runwayLengthInput; }
            set
            {
                _runwayLengthInput = value;
                OnPropertyChanged(nameof(RunwayLengthInput));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand TakeOffAirplaneCommand { get; }
        public ICommand TakeOffHelicopterCommand { get; }
        public ICommand LandHelicopterCommand { get; set; }

        public ICommand LandAirplaneCommand { get; set; }

        public ViewModel()
        {
            TakeOffAirplaneCommand = new RelayCommand(TakeOffAirplane);
            LandAirplaneCommand = new RelayCommand(LandAirplane);
            TakeOffHelicopterCommand = new RelayCommand(TakeOffHelicopter);
            LandHelicopterCommand = new RelayCommand(LandHelicopter);
        }

        

        private void TakeOffAirplane(object parameter)
        {
            if (double.TryParse(RunwayLengthInput, out double runwayLength))
            {
                _airplane.RunwayLength = runwayLength;
                if (_airplane.TakeOff())
                {
                    ErrorMessage = "--!----САМОЛЕТ УСПЕШНО ВЗЛЕТЕЛ!----!--";
                }
                else
                {
                    ErrorMessage = "---!!!---ВЗЛЕТОЧНАЯ ПОЛОСА КОРОТКАЯ (МЕНЬШЕ 500 МЕТРОВ)!---!!!---";
                }
            }
            else
            {
                ErrorMessage = "---!!!---НЕКОРЕКТНАЯ ДЛИНА ВЗЛЕТОЧНОЙ ПОЛОСЫ---!!!---";
            }
        }

        private void TakeOffHelicopter(object parameter)
        {
            if (_helicopter.TakeOff())
            {
                ErrorMessage = "--!----ВЕТРОЛЕТ ВЗЛЕТЕЛ!----!--";
            }
        }
        
        private void LandAirplane(object parameter)
        {
            if (_airplane.Land())
            {
                ErrorMessage = "--!----САМОЛЕТ УСПЕШНО ПРИЗЕМЛИЛСЯ!----!--";
            }
        }

        private void LandHelicopter(object parameter)
        {
            if (_helicopter.Land())
            {
                ErrorMessage = "--!----ВЕТРОЛЕТ ПРИЗЕМЛИЛСЯ!----!--";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
