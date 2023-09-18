using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ArrowsFontService : IFontService
    {
        public ArrowsFontService()
        {
            Console.WriteLine("Konstruktor ArrowsFontService");
        }
        public string Render(string @string)
        {
            return Figgle.FiggleFonts.Arrows.Render(@string);
        }
    }
}
