using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Task2;

namespace Task3
{
    public class ViewModel : INotifyPropertyChanged
    {
        
        private ObservableCollection<Type> _classNames;

        public ObservableCollection<Type> ClassNames
        {
            get { return _classNames; }
            set
            {
                _classNames = value;
                NotifyPropertyChanged("ClassNames");
            }
        }

        private ObservableCollection<string> _methodNames;

        public ObservableCollection<string> MethodNames
        {
            get { return _methodNames; }
            set
            {
                _methodNames = value;
                NotifyPropertyChanged("MethodNames");
            }
        }
        
        

        private List<Type> _aircraftTypes;
        

        
        public ViewModel()
        {
            ClassNames = new ObservableCollection<Type>();
            MethodNames = new ObservableCollection<string>();
            _aircraftTypes = new List<Type>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadAssembly(string assemblyPath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                var aircraftTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(Aircraft).IsAssignableFrom(t)).ToList();
                _aircraftTypes.Clear();
                _aircraftTypes.AddRange(aircraftTypes);
                ClassNames.Clear();
                foreach (var type in aircraftTypes)
                {
                    ClassNames.Add(type);
                }
                
                MethodNames.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assembly: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void LoadMethods(string className)
        {
            _selectedClassType = _aircraftTypes.FirstOrDefault(t => t.Name == className);
            if (_selectedClassType != null)
            {
                MethodNames.Clear();
                var methods = _selectedClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var method in methods)
                {
                    MethodNames.Add(method.Name);
                }
            }
            else
            {
                MethodNames.Clear();
            }
        }


        public void ExecuteMethod()
        {
            if (_selectedClassType != null)
            {
                var instance = Activator.CreateInstance(_selectedClassType) as Aircraft;
                if (instance != null)
                {
                    var method = _selectedClassType.GetMethod(SelectedMethodName);
                    if (method != null)
                    {
                        var result = method.Invoke(instance, null);
                        MessageBox.Show(result.ToString(), "Method Execution Result", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
        }

        private string _selectedMethodName;

        public string SelectedMethodName
        {
            get { return _selectedMethodName; }
            set
            {
                _selectedMethodName = value;
                NotifyPropertyChanged("SelectedMethodName");
            }
        }
        
        private Type _selectedClassType;
        
        public Type SelectedClassName
        {
            get { return _selectedClassType; }
            set
            {
                _selectedClassType = value;
                List<String> methods = new List<string>();
                foreach (var method in _selectedClassType.GetMethods())
                {
                    methods.Add(method.Name);
                }
                MethodNames = new ObservableCollection<string>(methods);
                NotifyPropertyChanged("SelectedClassName");
            }
        }
    }
}