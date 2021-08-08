using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Common.Exceptions;
using Tranzact.SearchFight.Configuration;

namespace Tranzact.SearchFight.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void TestConfigurationManager_GetConfiguration()
        {
            string filePath = ".\\Resources\\appsettings.json";
            ConfigurationManager configurationManager = new(filePath);
            var result = configurationManager.GetConfiguration();
            Assert.IsNotNull(result);
            Assert.AreEqual("Bing", result.BingSearchEngine.Provider);
            Assert.AreEqual("test", result.BingSearchEngine.Key);
            Assert.AreEqual("test", result.BingSearchEngine.URI);
            Assert.AreEqual("Google", result.GoogleSearchEngine.Provider);
            Assert.AreEqual("test", result.GoogleSearchEngine.Key);
            Assert.AreEqual("test", result.GoogleSearchEngine.URI);
            Assert.AreEqual("test", result.GoogleSearchEngine.CustomEngine);
        }

        [TestMethod]
        [ExpectedException(typeof(NoConfigurationFileException))]
        public void TestConfigurationManager_GetConfigurationNoFile()
        {
            string filePath = string.Empty;
            ConfigurationManager configurationManager = new(filePath);
            configurationManager.GetConfiguration();
        }

        [TestMethod]
        [ExpectedException(typeof(LessThanTwoEnginesException))]
        public void TestConfigurationManager_GetConfigurationLessEngines()
        {
            string filePath = ".\\Resources\\appsettings-noengine.json";
            ConfigurationManager configurationManager = new(filePath);
            configurationManager.GetConfiguration();
        }
    }
}
