using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace AspProject.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        /// <summary>
        /// коррректирует путь к файлу (берет лог-файл относительно директории запущенного приложения)
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static string CheckFilePath(string FilePath)
        {
            //if(!(FilePath != null && FilePath.Length > 0)) - до C#9
            if (FilePath is not { Length: > 0 })
                throw new ArgumentException("Указан некорректный путь к файлу", nameof(FilePath));

            if (Path.IsPathRooted(FilePath)) return FilePath;

            var assembly = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assembly!.Location);
            return Path.Combine(dir!, FilePath);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4net.config")
        {
            Factory.AddProvider(new Log4NetLoggerProvider(CheckFilePath(ConfigurationFile)));
            return Factory;
        }
    }
}
