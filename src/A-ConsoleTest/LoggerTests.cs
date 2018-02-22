using System;
using AInfrastructure;

namespace AConsoleTest
{
    public class LoggerTests : BaseTestClass
    {
        private readonly ILogger _logger;

        public LoggerTests(ILogger logger)
        {
            _logger = logger;
        }

        public override bool Test()
        {
            _logger.LogDebug(() => $"A simple message 1+1={1 + 1}");
            YetAnotherMethod();
            return true;
        }

        private void YetAnotherMethod()
        {
            _logger.LogDebug("Test calling logger from another method");
        }
    }
}
