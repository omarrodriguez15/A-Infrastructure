using System;
using AConsoleTest;
using AInfrastructure;

namespace AConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = null;
            try
            {
                Console.WriteLine("Hello World!");
                IClassResolver classResolver = new BaseClassResolver();

                classResolver.Get<LoggerTests>().Test();
            }
            catch (Exception ex)
            {
                logger?.LogException(ex);
                Console.WriteLine($"Exc: {ex?.Message}");
            }
        }
    }
}
