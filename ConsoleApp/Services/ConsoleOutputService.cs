using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class ConsoleOutputService : IOutputService
    {

        private IFontService? _fontService;

        public ConsoleOutputService() { }

        /*public ConsoleOutputService(IFontService fontService)
        {
            _fontService = fontService;
        }*/

        /*public ConsoleOutputService(IEnumerable<IFontService> fontServices)
        {
            _fontService = fontServices.Skip(new Random().Next(0, fontServices.Count())).First();
        }*/
        public ConsoleOutputService(IEnumerable<IFontService> fontServices, IConfiguration configuration)
        {
            _fontService = fontServices.Skip(configuration.GetValue<int>("Count")).First();
        }

        public void ShowText(string text)
        {
            Console.WriteLine( _fontService?.Render(text) ?? text);
        }
    }
}
