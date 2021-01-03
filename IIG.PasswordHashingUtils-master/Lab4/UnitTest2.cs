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
    public class UnitTest2
    {

        private const string Server = @"DESKTOP-E4A2HRP";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"L}EjpfCgru9X@GLj";
        private const int ConnectionTime = 75;

        readonly StorageDatabaseUtils storageDatкabaseUtils = new StorageDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);

        static readonly string workingDirectory = Environment.CurrentDirectory;
        private readonly string testsDirFullPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\test-dir";

        [TestMethod]
        public void PositiveAddFile()
        {
            string filepath = testsDirFullPath + "\\" +  "file.txt";
            string lines = "HELLO!";
            BaseFileWorker.Write(lines, filepath);

            byte[] byteArr = File.ReadAllBytes(filepath);
            
            storageDatкabaseUtils.AddFile(filepath, byteArr);
            
            byte[] returnedArr;
            string returnedFileName;

            int returned = int.Parse(storageDatкabaseUtils.GetFiles(filepath).Rows[0]["FileID"].ToString());
            
            storageDatкabaseUtils.GetFile(returned, out returnedFileName, out returnedArr);


            Assert.AreEqual(byteArr, returnedArr);
            Assert.AreEqual(filepath, returnedFileName);
        }

        [TestMethod]
        public void GetFilesEmptyCheck()
        {
            Assert.IsTrue(storageDatкabaseUtils.GetFiles("no_such_filename.dot").Rows.Count == 0);
        }
    }
}
