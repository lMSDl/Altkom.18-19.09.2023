using System;


//namespace ConsoleApp
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {


Console.WriteLine("Hello, World!");

JsonConvert.SerializeObject(new object());

//Nullable<int> a = null;
int? a = null;
string b = "a";

Console.WriteLine(b);
b = ToUpper(b)!; //! - wyłączenie ostrzeżenia o potencjalnym null

//<Nullable>enable</Nullable> - włączenie tej opcji powoduje, że wszystkie typy traktowane są jako wartościowe i powinniśmy jawnie określać, że mogą przyjąć wartość null
string? ToUpper(string? str /*!! - null-checking feature - dodaje podczas kompilacji (niejawnie) kod wyjątku jak poniżej */)
{
    //if (a == null)
    //    throw new ArgumentNullException(nameof(str));

    return str?.ToUpper();
}



 //       }
//    }
//}