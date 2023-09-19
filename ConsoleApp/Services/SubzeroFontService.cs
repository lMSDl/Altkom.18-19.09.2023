using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class SubzeroFontService : IFontService
    {
        public SubzeroFontService(ILogger<SubzeroFontService> logger)
        {
            logger.LogInformation("Konstruktor SubzeroFontService");
        }
        public string Render(string @string)
        {
            return Figgle.FiggleFonts.SubZero.Render(@string);
        }
    }
}
