using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SeleniumFramework.Core.Utilities
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot _config;

        static ConfigReader()
        {
            var coreProjectDirectory = AppDomain.CurrentDomain.BaseDirectory; // This assumes settings.json is in the output directory

            var builder = new ConfigurationBuilder()
                .SetBasePath(coreProjectDirectory)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true);

            _config = builder.Build();
        }

        public static string GetAppSetting(string key)
        {
            try
            {
                return _config["AppSettings:" + key];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading app setting '{key}': {ex.Message}");
            }
        }
    }
}
