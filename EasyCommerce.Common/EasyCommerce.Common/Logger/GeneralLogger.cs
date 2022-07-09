using Microsoft.Extensions.Logging;

namespace EasyCommerce.Common.Logger
{
    public class GeneralLogger : IGeneralLogger
    {
        private readonly ILogger<GeneralLogger> _logger;
        public GeneralLogger(ILogger<GeneralLogger> logger) => (_logger) = logger;
        public void HandlerException(Exception ex)
        {

        }
    }
}
