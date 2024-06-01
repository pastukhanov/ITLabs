using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CandyFactory.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CandyFactory.ViewModels
{
    public class FactoryViewModel : INotifyPropertyChanged
    {
        private Factory _factory;
        private Loader _loader;
        private string _factoryState;
        private string _loaderState;
        private bool _isStartEnabled;
        private bool _isStopEnabled;
        private bool _canAddSugar;
        private bool _canFixFactory;

        public ObservableCollection<string> Logs { get; set; }

        public string FactoryState
        {
            get => _factoryState;
            set
            {
                _factoryState = value;
                OnPropertyChanged();
                UpdateCommandStates();
            }
        }

        public string LoaderState
        {
            get => _loaderState;
            set
            {
                _loaderState = value;
                OnPropertyChanged();
            }
        }

        public bool IsStartEnabled
        {
            get => _isStartEnabled;
            set
            {
                _isStartEnabled = value;
                OnPropertyChanged();
                ((Command)StartCommand).ChangeCanExecute();
            }
        }

        public bool IsStopEnabled
        {
            get => _isStopEnabled;
            set
            {
                _isStopEnabled = value;
                OnPropertyChanged();
                ((Command)StopCommand).ChangeCanExecute();
            }
        }

        public bool CanAddSugar
        {
            get => _canAddSugar;
            set
            {
                _canAddSugar = value;
                OnPropertyChanged();
                ((Command)AddSugarCommand).ChangeCanExecute();
            }
        }

        public bool CanFixFactory
        {
            get => _canFixFactory;
            set
            {
                _canFixFactory = value;
                OnPropertyChanged();
                ((Command)FixFactoryCommand).ChangeCanExecute();
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand AddSugarCommand { get; }
        public ICommand FixFactoryCommand { get; }

        public FactoryViewModel()
        {
            _factory = new Factory();
            _loader = new Loader();

            _factory.SugarEnded += OnSugarEnded;
            _factory.AccidentOccurred += OnAccidentOccurred;

            _factory.FactoryStateChanged += OnFactoryStateChanged;
            _loader.LoaderStateChanged += OnLoaderStateChanged;

            Logs = new ObservableCollection<string>();

            StartCommand = new Command(StartFactory, () => IsStartEnabled);
            StopCommand = new Command(StopFactory, () => IsStopEnabled);
            AddSugarCommand = new Command(() => _factory.AddSugar(), () => CanAddSugar);
            FixFactoryCommand = new Command(() => _factory.FixFactory(), () => CanFixFactory);

            IsStartEnabled = true;
            IsStopEnabled = false;
            CanAddSugar = false;
            CanFixFactory = false;
        }

        private void StartFactory()
        {
            _factory.Start();
            _loader.Start();
            AddLog("Фабрика и погрузчик запущены.");
            IsStartEnabled = false;
            IsStopEnabled = true;
            CanAddSugar = true;
            CanFixFactory = true;
        }

        private void StopFactory()
        {
            _factory.Stop();
            _loader.Stop();
            AddLog("Фабрика и погрузчик остановлены.");
            IsStartEnabled = true;
            IsStopEnabled = false;
            CanAddSugar = false;
            CanFixFactory = false;
            
        }

        private void OnSugarEnded(object sender, SugarEventArgs e)
        {
            AddLog(e.Message);
            IsStartEnabled = false;
            // IsStopEnabled = false;
            CanAddSugar = true;
            // CanFixFactory = !CanFixFactory;
        }

        private void OnAccidentOccurred(object sender, AccidentEventArgs e)
        {
            AddLog(e.Message);
            IsStartEnabled = false;
            // IsStopEnabled = false;
            // CanAddSugar = false;
            CanFixFactory = true;
        }

        private void OnFactoryStateChanged(object sender, string state)
        {
            FactoryState = state;
            AddLog(state);
        }

        private void OnLoaderStateChanged(object sender, string state)
        {
            LoaderState = state;
            AddLog(state);
        }

        private void AddLog(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Logs.Add(message);
            });
        }

        private void UpdateCommandStates()
        {
            ((Command)StartCommand).ChangeCanExecute();
            ((Command)StopCommand).ChangeCanExecute();
            ((Command)AddSugarCommand).ChangeCanExecute();
            ((Command)FixFactoryCommand).ChangeCanExecute();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
