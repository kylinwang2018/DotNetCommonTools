using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetCommonTools;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DotNetCommonTools.Tests
{
    [TestClass()]
    public class ConfigurationHelperTests
    {
        [TestMethod()]
        public void GetConnectionStringTest()
        {
            string text = @"{
  ""ConnectionStrings"": {
    ""DefaultConnection"": ""Server=192.168.1.123;Database=FakeSQL;User Id=Fake;Password=Fake;MultipleActiveResultSets=true""
  }
    }";
            File.WriteAllText(@"appsettings.json", text);

            ConfigurationHelper.BuildConfigurationRoot();
            string result = ConfigurationHelper.GetConnectionString("DefaultConnection");

            Assert.AreEqual("Server=192.168.1.123;Database=FakeSQL;User Id=Fake;Password=Fake;MultipleActiveResultSets=true", result);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetConnectionStringTestInvalid()
        {
            ConfigurationHelper.appsettingsJsonFileName = "null";
            ConfigurationHelper.appsettingsXmlFileName = "null";
            ConfigurationHelper.BuildConfigurationRoot();
        }
    }
}