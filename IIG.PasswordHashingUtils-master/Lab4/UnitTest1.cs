using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

using IIG.FileWorker;
using System.Text;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;


namespace Lab4
{
    [TestClass]
    public class UnitTest1
    {
        private const string Server = @"DESKTOP-E4A2HRP";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"L}EjpfCgru9X@GLj";
        private const int ConnectionTime = 75;
        
        readonly AuthDatabaseUtils storageDatкabaseUtils = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);
      
        static readonly string workingDirectory = Environment.CurrentDirectory;
        private readonly string testsDirFullPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\tests-dir";

        [TestMethod]
        public void PositiveAddCredentials()
        {
            string hash = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.AddCredentials("login", hash);
            
            Assert.IsTrue(result);
            

        }

        [TestMethod]
        public void NegativeAddcredentials()
        {
            string hash = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.AddCredentials(null, hash);

            Assert.IsFalse(result);


        }
        [TestMethod]
        public void PositiveUpdateCredentials() 
        {
            string oldLogin = "login";
            string oldPass = PasswordHasher.GetHash("test");

            string newLogin = "login2";
            string newPass = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.UpdateCredentials(oldLogin, oldPass, newLogin, newPass);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void NegativeUpdateCredentials()
        {
            string oldLogin = "WRONG";
            string oldPass = PasswordHasher.GetHash("WRONG");

            string newLogin = "login2";
            string newPass = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.UpdateCredentials(oldLogin, oldPass, newLogin, newPass);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void PositiveCheckCredentials()
        {
            string oldLogin = "login2";
            string oldPass = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.CheckCredentials(oldLogin, oldPass);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void NegativeCheckCredentials()
        {
            string oldLogin = "WRONG";
            string oldPass = PasswordHasher.GetHash("WRONG");

            bool result = storageDatкabaseUtils.CheckCredentials(oldLogin, oldPass);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PositivedDeleteCredentials()
        {
            string oldLogin = "login2";
            string oldPass = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.DeleteCredentials(oldLogin, oldPass);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PositiveLoginAfterDelete()
        {
            string oldLogin = "login2";
            string oldPass = PasswordHasher.GetHash("test");

            bool result = storageDatкabaseUtils.CheckCredentials(oldLogin, oldPass);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NegativeDeleteCredentials()
        {
            string oldLogin = "WRONG";
            string oldPass = PasswordHasher.GetHash("WRONG");

            bool result = storageDatкabaseUtils.DeleteCredentials(oldLogin, oldPass);

            Assert.IsFalse(result);
        }

    }
}
