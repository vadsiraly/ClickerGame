using ClickerEngine.Enumerations;
using NUnit.Framework;

namespace ClickerEngine.Test
{
    [TestFixture]
    public class BonusTest
    {
        [Ignore("Obsolete")]
        [Test]
        public void BonusAddsCorrectValue()
        {
            var engine = new Engine();

            var oldVPS = engine.ValuePerSecond;
            if (oldVPS == new Value(0, 0))
            {
                oldVPS = new Value(1, 0);
            }

            engine.PurchaseBonus(new Bonus("SomeBonus", 6, new Value(0, 0), BonusType.Additive));

            var newVPS = engine.ValuePerSecond;

            Assert.AreEqual(newVPS, new Value(oldVPS.Gain * 6, oldVPS.Power).Normalize());
        }
    }
}
