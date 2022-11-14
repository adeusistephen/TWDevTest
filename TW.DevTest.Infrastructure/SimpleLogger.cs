using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TW.DevTest.Core.Contract.Response;
using TW.DevTest.Core.Enum;
using TW.DevTest.Core.Interfaces;

namespace TW.DevTest.Infrastructure
{
    public class SimpleLogger : ISimpleLogger
    {
        public SimpleLogger()
        {

        }

        public void Debug(string message)
        {
            WriteToFile(message, LogLevel.Debug);
        }
        public void Info(string message)
        {
            WriteToFile(message, LogLevel.Info);
        }
        public void Error(string message)
        {

            WriteToFile(message, LogLevel.Error);
        }

        public void Warning(string message)
        {
            WriteToFile(message, LogLevel.Warning);
        }

        private AppSettingsResponse ReadAppSettings()
        {

            return new AppSettingsResponse()
            {
                DirectoryPath = "Logs"
            };
        }

        private void WriteToFile(string message, LogLevel logLevel)


        {
            AppSettingsResponse appSetting = ReadAppSettings();
            string? path = System.IO.Path.GetFullPath(@"..\\..\\..\\");
            try
            {
                //2022-09-30 14:01:17.192
                string messageToLog = String.Format($"{DateTime.Now:yyyy-MM-dd hh:mm:ss tt} [{Enum.GetName<LogLevel>(logLevel)}] {message}");

                string dirPath = path + $@"{appSetting?.DirectoryPath}";

                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                string filePath = $@"{dirPath}\log-{DateTime.Now:yyyyMMdd}.txt";

                if (!File.Exists(filePath))
                {
                    using FileStream fileStream = File.Create(filePath);
                    using StreamWriter streamWritter = new(fileStream);
                    streamWritter.WriteLine(messageToLog);

                }
                else
                {
                    using FileStream fileStream = File.OpenRead(filePath);
                    using StreamReader streamReader = new(fileStream);
                    string readString = streamReader.ReadToEnd();
                    streamReader.Close();
                   
                    readString = readString + Environment.NewLine + messageToLog;
                    using FileStream fileStreamWrite = File.OpenWrite(filePath);
                    using StreamWriter streamWritter = new(fileStreamWrite);
                    streamWritter.WriteLine(readString);
                    streamWritter.Close();

                }
              

              

            }
            catch (Exception ex)
            {
                File.WriteAllText(path + $@"log-{DateTime.Now:yyyyMMdd}.txt", ex.Message);
            }

        }
    }
}
