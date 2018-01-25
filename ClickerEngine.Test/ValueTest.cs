using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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

        static object[] SubstractionCases =
        {
            new object[] { new Value(100, 0), new Value(50, 0), new Value(50, 0) },
            new object[] { new Value(100, 1), new Value(100, 0), new Value(90, 1) },
            new object[] { new Value(100, 0), new Value(120, 0), new Value(-20, 0) },
            new object[] { new Value(100, 10), new Value(120, 0), new Value(100, 10) },
            new object[] { new Value(50, 1), new Value(50, 1), new Value(0, 0) },
            new object[] { new Value(0, 0), new Value(145, 1), new Value(-145, 1) },
            new object[] { new Value(15, 0), new Value(145, 4), new Value(-145, 4) },
        };
    }
}
