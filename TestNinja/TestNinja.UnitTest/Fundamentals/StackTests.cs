using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest.Fundamentlas
{
    [TestFixture]
    class StackTests
    {
        private Stack<object> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<object>();
        }

        [Test]
        public void Push_NullObject_ArgumentNullEsxecption()
        {
            Assert.That(() => _stack.Push(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Push_Element_SizeIncreasedAndElementOnPeek()
        {
            object expectedObj = new object();
            int expectedSize = _stack.Count + 1;



            _stack.Push(expectedObj);
            int resultSize = _stack.Count;

            Assert.That(_stack.Peek(), Is.EqualTo(expectedObj));
            Assert.That(resultSize, Is.EqualTo(expectedSize));
        }

        [Test]
        public void Pop_EmptyStack_InvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Pop_Element_ReturnsElement()
        {
            object expectedObj = new object();
            int expectedSize = _stack.Count;
            _stack.Push(expectedObj);
            

            object returnedObject = _stack.Pop();
            int resultSize = _stack.Count;

            Assert.That(returnedObject, Is.EqualTo(expectedObj));
            Assert.That(resultSize, Is.EqualTo(expectedSize));
        }

        [Test]
        public void Peek_EmptyStack_InvalidOperationException()
        {
            Assert.That(() => { _stack.Peek(); }, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Peek_Element_ObtainElement()
        {
            object expectedObj = new object();
            _stack.Push(expectedObj);


            object returnedObject = _stack.Peek();
            int resultSize = _stack.Count;

            Assert.That(returnedObject, Is.EqualTo(expectedObj));
        }
    }
}
