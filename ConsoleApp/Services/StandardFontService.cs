using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class StandardFontService : IFontService
    {
        public StandardFontService()
        {
            Console.WriteLine("Konstruktor StandardFontService");
        }

        public string Render(string @string)
        {
            return @string;
        }
    }
}
