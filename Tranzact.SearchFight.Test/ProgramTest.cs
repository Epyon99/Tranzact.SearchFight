using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Tranzact.SearchFight.Test
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void TestProgram_Main()
        {
            string[] inputWords = { ".net", "java", "python" };
            string expectedOutputRegex = @".net Bing:\d+ Google:\d+ 
java Bing:\d+ Google:\d+ 
python Bing:\d+ Google:\d+ 
Google winner: .net,java,python
Total Winner: Google";

            Regex rx = new(expectedOutputRegex, RegexOptions.IgnoreCase);
            var result = false;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(inputWords);
                result = rx.IsMatch(sw.ToString().Trim());
            }

            Assert.AreEqual(true, result);
        }
    }
}
