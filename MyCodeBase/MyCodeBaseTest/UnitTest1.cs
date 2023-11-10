using NUnit.Framework;

namespace MyCodeBaseTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var a = 123;
            var b = 3;

            var result = Add(a, b);
            var expeced = a / b;
            Assert.AreEqual(result, expeced);
        }

        private int Add(int a, int b)
        {
            return a / b;
        }
    }
}