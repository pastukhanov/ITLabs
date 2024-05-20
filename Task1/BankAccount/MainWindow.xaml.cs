using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankAccount
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Deposit();
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Withdraw();
        }

        private void TakeCredit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TakeCredit();
        }

        private void RepayCredit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RepayCredit();
        }
    }
}