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
            var sum = v1 + v2;
            Assert.AreEqual(expected, sum);
        }

        public static object[] SummationCases =
        {
           new object[] { new Value(100, 0), new Value(50, 0), new Value(150, 0) },
           new object[] { new Value(100, 1), new Value(100, 0), new Value(1.1, 3) },
           new object[] { new Value(100, 0), new Value(-120, 0), new Value(-20, 0) },
           new object[] { new Value(100, 10), new Value(120, 0), new Value(100, 10) },
           new object[] { new Value(50, 1), new Value(-50, 1), new Value(0, 0) },
           new object[] { new Value(0, 0), new Value(145, 1), new Value(145, 1) },
           new object[] { new Value(141, 0), new Value(0, 0), new Value(141, 0) },
           new object[] { new Value(15, 0), new Value(145, 4), new Value(1.45, 6) },
           new object[] { new Value(173, 4), new Value(863, 8), new Value(86.3017, 9) },
           new object[] { new Value(123, 10), new Value(962, 33), new Value(962, 33) },
           new object[] { new Value(123, 33), new Value(962, 10), new Value(123, 33) },
           new object[] { new Value(1.4, 3), new Value(1, 3), new Value(2.4, 3) },
           new object[] { new Value(1.04, 3), new Value(1, 3), new Value(2.04, 3) },
           new object[] { new Value(1.004, 3), new Value(1, 3), new Value(2.004, 3) },
           new object[] { new Value(1.0004, 3), new Value(1, 3), new Value(2.0004, 3) },
           new object[] { new Value(1, 3), new Value(10, 0), new Value(1.010, 3) },
           new object[] { new Value(0, 0), new Value(0, 0), new Value(0, 0) },
        };

        public static object[] SubstractionCases = 
        {
           new object[] { new Value(100, 0), new Value(50, 0), new Value(50, 0) },
           new object[] { new Value(100, 1), new Value(100, 0), new Value(900, 0) },
           new object[] { new Value(100, 0), new Value(120, 0), new Value(-20, 0) },
           new object[] { new Value(100, 10), new Value(120, 0), new Value(1, 12) },
           new object[] { new Value(50, 1), new Value(50, 1), new Value(0, 0) },
           new object[] { new Value(0, 0), new Value(145, 1), new Value(-145, 1) },
           new object[] { new Value(15, 0), new Value(145, 4), new Value(-1.45, 6) },
           new object[] { new Value(173, 4), new Value(863, 8), new Value(-86.2983, 9) },
           new object[] { new Value(123, 10), new Value(962, 33), new Value(-962, 33) },
           new object[] { new Value(123, 33), new Value(962, 10), new Value(123, 33) },
           new object[] { new Value(1.4, 3), new Value(1, 3), new Value(400, 0) },
           new object[] { new Value(1.04, 3), new Value(1, 3), new Value(40, 0) },
           new object[] { new Value(1.004, 3), new Value(1, 3), new Value(4, 0) },
           new object[] { new Value(0, 0), new Value(0, 0), new Value(0, 0) },
        };
    }
}
