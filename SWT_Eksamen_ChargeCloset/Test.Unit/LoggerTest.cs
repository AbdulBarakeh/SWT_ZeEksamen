using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Classes.Logger;
using Classes.Control;
using NSubstitute;
using NUnit.Framework.Internal;
using Logger = Classes.Logger.Logger;

namespace Test.Unit
{
    [TestFixture]
    class LoggerTest
    {

        
        private Logger _uut;

        [SetUp]
        public void Setup()
        {

            _uut = new Logger();
        }

        [Test]
        public void LogLocked()
        {
            //Arrange
            string lineToBeLogged = "Testing the loggin";
            string result = String.Empty;
            
            //Act
            _uut.log(lineToBeLogged);
            using (StreamReader R = File.OpenText(Path.GetTempPath()+"logfile.txt"))
            {
                result = R.ReadLine();
            }

            //Assert
            Assert.That(result,Is.EqualTo(lineToBeLogged));
        }
    }
}
