// Account.cs
public class Account
{
    private readonly string id;
    private readonly string currency;
    private int balance;

    public Account(string id, string currency, int balance)
    {
        this.id = id;
        this.currency = currency;
        this.balance = balance;
    }

    public string Currency => currency;
    public int Balance => balance;
    public string Id => id;

    public void Withdraw(int amount)
    {
        if (balance < amount)
            throw new ArgumentException("Insufficient funds");
        balance -= amount;
    }

    public void Deposit(int amount)
    {
        balance += amount;
    }
}
