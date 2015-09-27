using System;
using MaybeApp;
using NUnit.Framework;

namespace MaybeTests
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void Just()
        {
            var m = Maybe.Just(5);
            Assert.That(m.IsJust, Is.True);
            Assert.That(m.IsNothing, Is.False);
        }

        [Test]
        public void Nothing()
        {
            var m = Maybe.Nothing<int>();
            Assert.That(m.IsNothing, Is.True);
            Assert.That(m.IsJust, Is.False);
        }

        [Test]
        public void FromJustOfJustReturnsValue()
        {
            var m = Maybe.Just(5);
            Assert.That(m.FromJust, Is.EqualTo(5));
        }

        [Test]
        public void FromJustOfNothingThrowsException()
        {
            var m = Maybe.Nothing<int>();
            // ReSharper disable once UnusedVariable
            Assert.Throws<InvalidOperationException>(() => { var dummy = m.FromJust; });
        }

        [Test]
        public void OrElseOfJust()
        {
            var m = Maybe.Just(5);
            Assert.That(m.OrElse(10), Is.EqualTo(5));
        }

        [Test]
        public void OrElseOfNothing()
        {
            var m = Maybe.Nothing<int>();
            Assert.That(m.OrElse(10), Is.EqualTo(10));
        }

        [Test]
        public void Return()
        {
            var m = Maybe.Return(5);
            Assert.That(m.IsJust, Is.True);
            Assert.That(m.IsNothing, Is.False);
            Assert.That(m.FromJust, Is.EqualTo(5));
        }

        [Test]
        public void BindOfJust()
        {
            var ma = Maybe.Just(5);
            var mb = ma.Bind(MethodThatReturnsJust);
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo("5"));
        }

        [Test]
        public void BindOfNothing()
        {
            var ma = Maybe.Nothing<int>();
            var mb = ma.Bind(MethodThatReturnsJust);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void BindOfJustButMethodReturnsNothing()
        {
            var ma = Maybe.Just(5);
            var mb = ma.Bind(MethodThatReturnsNothing);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void SelectOfJust()
        {
            var ma = Maybe.Just(5);
            var mb = ma.Select(Convert.ToString);
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo("5"));
        }

        [Test]
        public void SelectOfNothing()
        {
            var ma = Maybe.Nothing<int>();
            var mb = ma.Select(Convert.ToString);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void SelectUsingLinqQuerySyntax()
        {
            var ma = Maybe.Just(5);
            var mb = from a in ma select Convert.ToString(a);
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo("5"));
        }

        [Test]
        public void SelectManyOfJust()
        {
            var ma = Maybe.Just(5);
            var mb = ma.SelectMany(MethodThatReturnsJust);
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo("5"));
        }

        [Test]
        public void SelectManyOfNothing()
        {
            var ma = Maybe.Nothing<int>();
            var mb = ma.SelectMany(MethodThatReturnsJust);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void SelectManyOfJustButMethodReturnsNothing()
        {
            var ma = Maybe.Just(5);
            var mb = ma.SelectMany(MethodThatReturnsNothing);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void SelectManyUsingLinqQuerySyntax()
        {
            var ma = Maybe.Just(5);
            var mb = from a in ma from b in MethodThatReturnsJust(a) select b;
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo("5"));
        }

        [Test]
        public void SelectManyOverloadOfJust()
        {
            var ma = Maybe.Just(5);
            var mb = ma.SelectMany(MethodThatReturnsJust, Tuple.Create);
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo(Tuple.Create(5, "5")));
        }

        [Test]
        public void SelectManyOverloadOfNothing()
        {
            var ma = Maybe.Nothing<int>();
            var mb = ma.SelectMany(MethodThatReturnsJust, Tuple.Create);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void SelectManyOverloadOfJustButMethodReturnsNothing()
        {
            var ma = Maybe.Just(5);
            var mb = ma.SelectMany(MethodThatReturnsNothing, Tuple.Create);
            Assert.That(mb.IsNothing, Is.True);
            Assert.That(mb.IsJust, Is.False);
        }

        [Test]
        public void SelectManyOverloadUsingLinqQuerySyntax()
        {
            var ma = Maybe.Just(5);
            var mb = from a in ma from b in MethodThatReturnsJust(a) select Tuple.Create(a, b);
            Assert.That(mb.IsJust, Is.True);
            Assert.That(mb.IsNothing, Is.False);
            Assert.That(mb.FromJust, Is.EqualTo(Tuple.Create(5, "5")));
        }

        private static Maybe<string> MethodThatReturnsJust(int a)
        {
            return Maybe.Just(Convert.ToString(a));
        }

        private static Maybe<string> MethodThatReturnsNothing(int _)
        {
            return Maybe.Nothing<string>();
        }
    }
}
