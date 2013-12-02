using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asp_example.Utils;

namespace asp_example.tests
{
    [TestFixture]
    public class DateDisplayHelpersShould
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [TestCase(-4)]
        public void Display_the_correct_elapsed_class(int daysPassed)
        {
            var createdDate = DateTime.Now.AddDays(daysPassed);

            Assert.That(createdDate.GetElapsedDaysClass(), Is.EqualTo("dfs"));
        }
    }
}
