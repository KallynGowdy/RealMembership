using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Data.Common;
using RealMembership.Implementation.EF;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace RealMembership.Implementation.EF.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void TestCreateGoodAccount()
        {
            // Setup
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            TestDbContext context = new TestDbContext(connection);
            ILoginRepository<UserAccount> repository = new LoginRepository<UserAccount>(context);
            IUserService<UserAccount> service = new UserService<UserAccount>(repository)
            {
                EmailService = new DefaultEmailService(new SmtpClient("smtp.mandrillapp.com", 587)
                {
                    Credentials = new NetworkCredential("kallyngowdy@gmail.com", "3xgf2AIaRN45LM_VbuvQgg"),
                    EnableSsl = true
                }, "admin@classroomaid2.com")
            };

            var result = service.CreateAccountAsync(new EmailAccountCreationRequest
            {
                Email = "2risker@gmail.com",
                Password = "NotASecret1@"
            }).Result;

            Assert.IsTrue(result.Successful);
            Assert.IsNotNull(result.CreatedAccount);
            Assert.AreEqual(result.Result, AccountCreationResultType.CreatedAndSentCode);
            Assert.IsTrue(result.CreatedAccount.Logins.All(l => l.VerificationCode != null));
        }
    }
}
