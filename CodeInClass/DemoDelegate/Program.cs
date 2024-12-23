// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
class Program
{
    public delegate int myDelegate(int t1, int t2);//dang ki delegate
    public int sum(int x , int y)=> x + y;
    public int sub(int x, int y )=> x - y;
    public int multi(int x , int y) => x * y;

    public static void Main(string[] args) {
        Program p = new Program();
        int x = 20; int y = 10;
        myDelegate myD = new myDelegate(p.sum);// chi truyen ten ham , k truyen para
        //myD = (x,y) => x + y; // uy nhiem ham
        myD += p.sub;
        myD += p.multi;
        myD += p.sub;

        myD += (x, y) => { Console.WriteLine("D is called"); return x / y; };
        //---------------------

        int z = myD(x,y);
        Console.WriteLine($"Ket qua {x} and {y} = {z}");
        myD -= p.sub;
        Console.WriteLine(myD(x,y));
        myD -= p.sub;
        Console.WriteLine(myD(x, y));

    }
}