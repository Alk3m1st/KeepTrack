using asp_example.Migrations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity.Migrations;

namespace asp_example.tests.Migrations
{
    [TestFixture]
    class MigrationsShould
    {
        private DbMigrator _migrator;

        [SetUp]
        public void SetUpFixture()
        {
            string connectionString = string.Empty;
            connectionString = System.Configuration.ConfigurationManager
                                    .ConnectionStrings["DefaultConnection"].ConnectionString;

            var configuration = new asp_example.Migrations.Configuration();
            configuration.TargetDatabase =
                new System.Data.Entity.Infrastructure.DbConnectionInfo(connectionString, "System.Data.SqlClient");

            _migrator = new DbMigrator(configuration);
        }
        
        [Test]
        public void Go_down_and_back_up()
        {
            // Go down to first migration
            _migrator.Update("0");

            // Go back up
            _migrator.Update();
        }
    }
}
