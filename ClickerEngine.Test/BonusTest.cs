using ClickerEngine.Enumerations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClickerEngine.Test
{
    [TestFixture]
    public class BonusTest
    {
        [Test]
        public void BonusAddsCorrectValue()
        {
            var engine = new Engine();

            var oldVPS = engine.ValuePerSecond;

            engine.PurchaseBonus(new Bonus("SomeBonus", 6, new Value(0, 0), BonusType.Additive));

            Thread.Sleep(10000);

            var newVPS = engine.ValuePerSecond;

            Assert.AreEqual(newVPS, new Value(oldVPS.Gain * 6, oldVPS.Power).Normalize());
        }
    }
}
