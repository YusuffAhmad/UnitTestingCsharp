using Stack = SoftwareTesting.Fundamentals.Stack<object>;

namespace SoftwareTesting.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Fundamentals.Stack<object> _stack;
        [SetUp]
        public void SetUp()
        {
            _stack = new Fundamentals.Stack<object>();
        }
        [Test]
        public void Push_WhenInputObjectIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_WhenInputObjectIsValid_AddObject()
        {
            int initialStackCount = _stack.Count;

            _stack.Push(1);

            Assert.That(_stack.Count, Is.GreaterThan(initialStackCount));
            Assert.That(_stack.Count, Is.Not.EqualTo(0));
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_WhenTheListIsEmpty_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_WhenTheListContainsMoreThanOneElement_RemoveTheLastTheElement()
        {
            _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);

            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(2));
        }
        [Test]
        public void Pop_WhenTheListContainsMoreThanOneElement_ReturnTheLastTheElement()
        {
            _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(result, Is.Not.EqualTo(null));
            Assert.That(_stack.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public void Peek_WhenTheListIsEmpty_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_WhenTheListContainsMoreThanOneElement_RemoveTheLastTheElement()
        {
             _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo(3));
        }
    }
}

