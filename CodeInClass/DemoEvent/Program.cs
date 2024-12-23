// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
public delegate void MyDelegate(int x, int y);
class Account
{
    private int amount;
    //public event MyDelegate AmountChanged;
    private event EventHandler<myArgs> _amountChanged;
    public event EventHandler<myArgs> AmountChanged
    {
        add
        {
            _amountChanged += value;
        }
        remove
        {
            _amountChanged -= value;
        }
    }

    public int Amount {
        get { return amount; }
        set
        {
            int old = amount;
            amount = value;
            if(amount != old)
             onChangeAmount(old, amount);
        }
    }
    private void onChangeAmount(int old, int _new)
    {
        if(_amountChanged != null)
        {
            _amountChanged.Invoke(null, new myArgs(old, _new));
        }
    }
}
class myArgs: EventArgs
{
    public int Old { get; set; }
    public int New { get; set; }
    public myArgs(int o, int n)
    {
        Old = o;
        New = n;
    }
}
class Using
{
    public static void Main(string[] args)
    {
        Using u = new Using();
        Account account = new Account();
        account.Amount = 100;
        account.AmountChanged += u.Accoun_AmountChanged;
        account.Amount = 200;
        account.Amount = 250;
    }
    private void Accoun_AmountChanged(object? sender, myArgs my)
    {
        if(my.Old!=my.New)
        Console.WriteLine($"So du cua ban duoc thay doi tu {my.Old} sang {my.New}");
    }
}