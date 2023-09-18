using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class DebugOutputService : IOutputService
    {
        public void ShowText(string text)
        {
            Debug.WriteLine(text);
        }
    }
}
