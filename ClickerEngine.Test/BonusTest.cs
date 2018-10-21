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

            var oldVPT = engine.ValuePerTick;
            if (oldVPT == new Value(0, 0))
            {
                oldVPT = new Value(1, 0);
            }

            engine.PurchaseBonus(new Bonus("SomeBonus", 6, new Value(0, 0), BonusType.Additive));

            var newVPT = engine.ValuePerTick;

            Assert.AreEqual(newVPT, new Value(oldVPT.Gain * 6, oldVPT.Power).Normalize());
        }
    }
}
