using asp_example.Utils;
using Microsoft.WindowsAzure.Storage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp_example.tests.Utils
{
    [TestFixture]
    public class TableStorageHelpersShould
    {
        [Test]
        public void Get_table()
        {
            var storageConnectionString = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            var table = TableStorageHelpers.GetTable(storageConnectionString, "Todos");

            Assert.That(table, Is.Not.Null);
        }
    }
}
