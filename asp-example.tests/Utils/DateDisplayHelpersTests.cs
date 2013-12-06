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
        [TestCase(0, "today")]
        [TestCase(-1, "one-day-elapsed")]
        [TestCase(-2, "two-days-elapsed")]
        [TestCase(-3, "three-days-elapsed")]
        [TestCase(-4, "fully-elapsed")]
        public void Display_the_correct_elapsed_class(int daysPassed, string message)
        {
            DateTime? completedDate = DateTime.Now.AddDays(daysPassed);

            Assert.That(completedDate.GetElapsedDaysClass(), Is.EqualTo(message));
        }

        [Test]
        public void Return_empty_class_if_date_is_null()
        {
            var date = new DateTime?();

            Assert.That(date.GetElapsedDaysClass(), Is.EqualTo(string.Empty));
        }
    }
}
