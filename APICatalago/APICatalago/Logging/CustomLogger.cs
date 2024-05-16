namespace APICatalago.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public bool IsEnable(LogLevel logLevel)
        {
            return logLevel == loggerConfig.logLevel;
        }

        public IDisposable BeginScope<TSate>(TSate state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.logLevel;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formater)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formater(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }
        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"C:\Users\NATHANAEL\nathanael_log.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
