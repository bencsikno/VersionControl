using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;
using UnitTestExample.Entities;

namespace UnitTestExample.Test
{
     public class AccountControllerTestFixture
    {
        
    [Test,
    TestCase("abcd1234", false),
    TestCase("irf@uni-corvinus", false),
    TestCase("irf.uni-corvinus.hu", false),
    TestCase("irf@uni-corvinus.hu", true)

]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();

            
            var actualResult = accountController.ValidateEmail(email);

            
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test, 
            TestCase("Abcefhgi",false),
            TestCase("ABCEDFGHI123", false), 
            TestCase("abcdefghi123", false),
            TestCase("Abc123", false),
            TestCase("ABCdefghi123", true)]

        public void TestValidatePassword( string password, bool ecpectedResult)
        {
            var acountContoller = new AccountController();

            var actualResult = acountContoller.ValidatePassword(password);

            Assert.AreEqual(ecpectedResult, actualResult);
        }


        [
    Test,
    TestCase("irf@uni-corvinus.hu", "Abcd1234"),
    TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
]
        public void TestRegisterHappyPath(string email, string password)
        {
            
            var accountController = new AccountController();

            
            var actualResult = accountController.Register(email, password);

            
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [
    Test,
    TestCase("irf@uni-corvinus", "Abcd1234"),
    TestCase("irf.uni-corvinus.hu", "Abcd1234"),
    TestCase("irf@uni-corvinus.hu", "abcd1234"),
    TestCase("irf@uni-corvinus.hu", "ABCD1234"),
    TestCase("irf@uni-corvinus.hu", "abcdABCD"),
    TestCase("irf@uni-corvinus.hu", "Ab1234"),
]
        public void TestRegisterValidateException(string email, string password)
        {
            
            var accountController = new AccountController();

            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Returns<Account>(a => a);           
            accountController.AccountManager = accountServiceMock.Object;



           
                var actualResult = accountController.Register(email, password);
              
            

            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
            accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);

        }
    }

}
