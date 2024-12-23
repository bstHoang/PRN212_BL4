// See https://aka.ms/new-console-template for more information
string[] name = {"John" , "Tom" , "Jerry" , "Cat" , "Dog" };
//foreach (string s in name.OrderBy(x => x)) { 
//    Console.WriteLine(s);
//}
var obj1 = name.OrderBy(x => x).Select(x => new { Name = x , Length = x.Length});
var obj = from names in name orderby names select (new { Name = names, Length = names.Length });
foreach (var x in obj) { 
    Console.WriteLine($"Name = {x.Name} , Length = {x.Length}");
}
int[] n1 = { 1, 3, -2, -4, -7, -3, -8, 12, 19, 6, 9, 10, 14 };
var nQuery = from tmp in n1 where !(tmp >0 && tmp >12 ) select tmp;
foreach (var x in nQuery) {
    Console.WriteLine(x);
}

List<int> numbers = new List<int> { 6, 0, 999, 11, 443, 6, 1, 24, 54};
var temp = numbers.OrderByDescending(x => x).ToArray();
var top5 = numbers.Where(x => x >= temp[4]).Select(x => x);
foreach (var x in top5) { 
    Console.WriteLine(x);
}