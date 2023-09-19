using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;

namespace ConsoleApp
{
    internal class LoggerDemo
    {
        private ILogger<LoggerDemo> _logger;

        public LoggerDemo(ILogger<LoggerDemo> logger)
        {
            _logger = logger;
        }


        public void Work()
        {
            _logger.LogTrace($"Begin {nameof(Work)}");


            try
            {

                using (var scope1 = _logger.BeginScope(nameof(Work)))
                using (var scope3 = _logger.BeginScope("my format {0}...", GetType().Name))
                using (var scope4 = _logger.BeginScope(new Dictionary<string, string> { { "a", "1" }, { "b", "2" } }))
                {

                    for (int i = 0; i < 10; i++)
                    {
                        using (var scope2 = _logger.BeginScope(i.ToString()))
                        {

                            try
                            {
                                _logger.LogDebug("Working...");

                                if (i == 5)
                                    throw new IndexOutOfRangeException($"Index {5}");
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e, "Opss.. błąd..");
                            }


                            if (i == 9)
                                throw new Exception("Bad value...");
                        }
                    }
                }

            }
            catch(Exception e) when (LogError(e))
            {
            }

            _logger.LogTrace($"End {nameof(Work)}");
        }

        private bool LogError(Exception e)
        {
            _logger.LogError(e, "Unhandled exception");
            return true;
        }
    }
}
