namespace app.Services.Common.Logger
{
	public class FileLogger : ILogger
	{
		private string filePath;
		private static object _lock = new object();
		public FileLogger(string path)
		{
			filePath = path;
		}
		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (formatter != null)
			{
				lock (_lock)
				{
					string fileName = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt";
					File.AppendAllText($"{filePath}/{fileName}",
						$"\n\n#################### {DateTime.Now} {Environment.NewLine}{formatter(state, exception)}{Environment.NewLine}"
						);
				}
			}
		}
	}
}
