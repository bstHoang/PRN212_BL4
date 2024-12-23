// method main k tường minh
//entry point k tường minh
namespace Slot_1_2
{
    internal partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            //Class1 class1 = new Class1();
            //class1.HelloByVietNam();
            //var myInt = 20;
            //dynamic x = 5;
            //if (myInt > 10)
            //{
            //    x = "String";
            //}
            int x = 10;
            double y = 20.22213123;
            long z = 123_423;
            string s = @$"x=({x,5}) ,
                           y=({y,5:N3}) ,z ={z}";
            Console.WriteLine(s);
        }
    }
}   

