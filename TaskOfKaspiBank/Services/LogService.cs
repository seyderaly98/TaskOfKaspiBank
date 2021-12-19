using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace TaskOfKaspiBank.Services
{
    public class LogService
    {
        private IHostEnvironment _environment;

        public LogService(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GetLogData(string orderId)
        {
            var filePath = Path.Combine(_environment.ContentRootPath,@$"wwwroot\log\orderChangeHistory\{orderId}.txt");
            if (!System.IO.File.Exists(filePath)) return null;
            using var sr = new StreamReader(filePath, System.Text.Encoding.Default);
            return sr.ReadToEnd();
        }

        public void Logger(string orderId,string logText)
        {
            var directoryPath = Path.Combine(_environment.ContentRootPath,$"wwwroot/log/orderChangeHistory/");
            var filePath = Path.Combine(directoryPath, $"{orderId}.txt");
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            using var file = new StreamWriter(filePath, true, Encoding.Default);
            file.WriteLine( $"{DateTime.Now} | {logText}");
        }
    }
}