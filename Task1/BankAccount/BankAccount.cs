using System;

namespace BankAccount;

public class BankAccount
{
    // Банковские реквизиты
    public string BankName { get; }
    public string INN { get; }
    public string BIK { get; }
    public string CorrAccount { get; }

// Свойства доступные только для чтения
    public decimal Balance { get; private set; }
    public decimal WithdrawalFee { get; }
    public decimal CreditInterestRate { get; }

// Конструктор класса
    public BankAccount(string bankName, string inn, string bik, string corrAccount, decimal balance, decimal withdrawalFee, decimal creditInterestRate)
    {
        BankName = bankName;
        INN = inn;
        BIK = bik;
        CorrAccount = corrAccount;
        Balance = balance;
        WithdrawalFee = withdrawalFee;
        CreditInterestRate = creditInterestRate;
    }

// Методы
    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        decimal totalAmount = amount + WithdrawalFee;
        if (Balance - totalAmount < 0)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= totalAmount;
    }

    public void TakeCredit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        Balance += amount;
    }

    public void RepayCredit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        if (Balance - amount < 0)
        {
            throw new InvalidOperationException("Insufficient funds to repay.");
        }

        Balance -= amount;
    }

    public void ApplyInterest()
    {
        Balance += Balance * (CreditInterestRate / 100);
    }
}