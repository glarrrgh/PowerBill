using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBillCMD.Tests
{
    [TestClass]
    public class PowerBillCMDTests
    {
        [TestMethod]
        public void TestParseArgsWithNothing()
        {
            Assert.ThrowsException<ParseArgException>(() => { new ParseArgs(new string[] { }); });
            Assert.ThrowsException<ParseArgException>(() => 
            {
                new ParseArgs(new string[] { "1", "1.2.3", "2", "1.3.3", "3.2", "2.3" });
            });
        }
    }
}
