//// See https://aka.ms/new-console-template for more information
//using System.Runtime.CompilerServices;

//Console.WriteLine("Hello, World!");
//int x = 3; int y = 4;
//int z = x + y;
//Console.WriteLine($"{x}+{y}={z}");
//Console.WriteLine($"{x}+{y}={abc.Sum(x,y)}");
//Student student = new Student() { Id=1 , Name ="Nguyen Van A"};
//student.Print();
//static class abc
//{
//    static public int Sum(int x, int y) { 
//        return x + y;
//    }

//    static public int Add(this int x ,int y) {
//        return (x + y);
//    }

//    static public void Print(this Student st) {
//        Console.WriteLine($"Studnent information ID :{st.Id} Name:{st.Name}");
//    }
//}

//class Student { 
//    public int Id { get; set; } 
//    public string Name { get; set; }
//}
public delegate bool myCheck(int x);
class Program {
    public int calc(int x, int y, myCheck myC) { 
        if (myC(x))
            return x+y;
        else return x-y;
    }

    public bool Check1(int x) { 
        return x %2 == 0;
    }

    public bool Check2(int x) {
        return x == (int)Math.Sqrt(x) * (int)Math.Sqrt(x);
    }

    public static void Main(string[] args) { 
        Program p = new Program();
        int x = 10;
        int y = 7;
        int z = p.calc(x, y, p.Check1);
        Console.WriteLine($"{x} and {y} = {z}");
    } 
}