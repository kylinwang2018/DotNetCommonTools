using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DotNetCommonTools
{
    public static class ConfigurationHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string appsettingsJsonFileName = "appsettings.json";

        /// <summary>
        /// 
        /// </summary>
        public static string appsettingsXmlFileName = "app.config";

        /// <summary>
        /// 
        /// </summary>
        private static IConfigurationRoot configuration = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool BuildConfigurationRoot()
        {
            if (!VerifySettingFileExist())
                throw new FileNotFoundException("No config file found.");

            configuration = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile(appsettingsJsonFileName, true)
                            .AddXmlFile(appsettingsXmlFileName, true)
                            .Build();
            return true;
        }

        public static bool VerifySettingFileExist()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + appsettingsJsonFileName) ||
                File.Exists(AppDomain.CurrentDomain.BaseDirectory + appsettingsXmlFileName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// <para>Returns the specified ConnectionString from App.config.</para>
        /// <para>If ConnectionString does not exist, returns <c>null</c>.</para>
        /// </summary>
        /// <param name="name">Name of the connection string in App.config.</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
