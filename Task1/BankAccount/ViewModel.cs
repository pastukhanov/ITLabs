using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankAccount;

public class ViewModel : INotifyPropertyChanged
{
    private BankAccount _bankAccount;

    public BankAccount BankAccount
    {
        get { return _bankAccount; }
        set
        {
            _bankAccount = value;
            OnPropertyChanged();
        }
    }

    private decimal _amount;

    public decimal Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            OnPropertyChanged();
        }
    }

    public ViewModel()
    {
        BankAccount = new BankAccount("My Bank", "1234567890", "9876543210", "12345678901234567890", 1000, 0.5m, 5);
    }

    public void Deposit()
    {
        BankAccount.Deposit(Amount);
        OnPropertyChanged(nameof(BankAccount)); // Notify UI that BankAccount property has changed
    }

    public void Withdraw()
    {
        BankAccount.Withdraw(Amount);
        OnPropertyChanged(nameof(BankAccount)); // Notify UI that BankAccount property has changed
    }

    public void TakeCredit()
    {
        BankAccount.TakeCredit(Amount);
        OnPropertyChanged(nameof(BankAccount)); // Notify UI that BankAccount property has changed
    }

    public void RepayCredit()
    {
        BankAccount.RepayCredit(Amount);
        OnPropertyChanged(nameof(BankAccount)); // Notify UI that BankAccount property has changed
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}