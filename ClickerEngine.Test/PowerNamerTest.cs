using ClickerEngine.PowerNames;
using NUnit.Framework;

namespace ClickerEngine.Test
{
    [TestFixture]
    public class PowerNamerTest
    {
        [Test]
        [TestCase(0, "")]
        [TestCase(1, "")]
        [TestCase(2, "")]
        [TestCase(3, "Thousand")]
        [TestCase(4, "Thousand")]
        [TestCase(5, "Thousand")]
        [TestCase(6, "Million")]
        [TestCase(66, "Unvigintillion")]
        [TestCase(3081, "Milliasexvigintillion")]
        [TestCase(9996, "Tremilliatrecenuntrigintillion")]
        [TestCase(9999, "Tremilliatrecendotrigintillion")]
        public void CorrectNameReturned(int power, string name)
        {
            var powerName = PowerNamer.GetName(power);

            Assert.AreEqual(name, powerName);
        }
    }
}
