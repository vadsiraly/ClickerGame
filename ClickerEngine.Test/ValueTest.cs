using NUnit.Framework;

namespace ClickerEngine.Test
{
    [TestFixture]
    public class ValueTest
    {
        [Test]
        [TestCaseSource("SubstractionCases")]
        public void SubstractionTest(Value v1, Value v2, Value expected)
        {
            var sub = v1 - v2;
            Assert.AreEqual(expected, sub);
        }

        [Test]
        [TestCaseSource("SummationCases")]
        public void SummationTest(Value v1, Value v2, Value expected)
        {
            var sub = v1 + v2;
            Assert.AreEqual(expected, sub);
        }

        public static object[] SummationCases =
        {
           new object[] { new Value(100, 0), new Value(50, 0), new Value(150, 0) },
           new object[] { new Value(100, 1), new Value(100, 0), new Value(110, 1) },
           new object[] { new Value(100, 0), new Value(-120, 0), new Value(-20, 0) },
           new object[] { new Value(100, 10), new Value(120, 0), new Value(100, 10) },
           new object[] { new Value(50, 1), new Value(-50, 1), new Value(0, 0) },
           new object[] { new Value(0, 0), new Value(145, 1), new Value(145, 1) },
           new object[] { new Value(141, 0), new Value(0, 0), new Value(141, 0) },
           new object[] { new Value(15, 0), new Value(145, 4), new Value(145.0015, 4) },
           new object[] { new Value(173, 4), new Value(863, 8), new Value(863.0173, 8) },
           new object[] { new Value(123, 10), new Value(962, 33), new Value(962, 33) },
           new object[] { new Value(123, 33), new Value(962, 10), new Value(123, 33) },
           new object[] { new Value(0, 0), new Value(0, 0), new Value(0, 0) },
        };

        public static object[] SubstractionCases = 
        {
           new object[] { new Value(100, 0), new Value(50, 0), new Value(50, 0) },
           new object[] { new Value(100, 1), new Value(100, 0), new Value(90, 1) },
           new object[] { new Value(100, 0), new Value(120, 0), new Value(-20, 0) },
           new object[] { new Value(100, 10), new Value(120, 0), new Value(100, 10) },
           new object[] { new Value(50, 1), new Value(50, 1), new Value(0, 0) },
           new object[] { new Value(0, 0), new Value(145, 1), new Value(-145, 1) },
           new object[] { new Value(15, 0), new Value(145, 4), new Value(-144.9985, 4) },
           new object[] { new Value(173, 4), new Value(863, 8), new Value(-862.9827, 8) },
           new object[] { new Value(123, 10), new Value(962, 33), new Value(-962, 33) },
           new object[] { new Value(123, 33), new Value(962, 10), new Value(123, 33) },
           new object[] { new Value(0, 0), new Value(0, 0), new Value(0, 0) },
        };
    }
}
