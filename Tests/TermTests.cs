using MiniMaple;

namespace Tests;


public class TermTests
{
    [Test]
    public void TestConstructor()
    {
        var x = new Term("x", 1, 2);
        Assert.That(x, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(x.Symbol, Is.EqualTo("x"));
            Assert.That(x.Coefficient, Is.EqualTo(1));
            Assert.That(x.Power, Is.EqualTo(2));
        });
    }

    [Test]
    public void TestEq()
    {
        var x1 = new Term("x", 1, 2);
        var x2 = new Term("x", 1, 2);
        var x3 = new Term("x", 1, 3);
        var y = new Term("y", 1, 2);
        Assert.Multiple(() =>
        {
            Assert.That(x1.Eq(x2), Is.True);
            Assert.That(x1.Eq(x3), Is.False);
            Assert.That(x2.Eq(x3), Is.False);
            Assert.That(x1.Eq(y), Is.False);
        });
    }

    [Test]
    public void TestNeg()
    {
        var x = new Term("x");
        Assert.That(((Term)x.Neg()).Coefficient, Is.EqualTo(-1));
    }

    [Test]
    public void TestNegative()
    {
        var x = new Term("x");
        var x2 = new Term("x", -1);
        Assert.Multiple(() =>
        {
            Assert.That(x.Negative, Is.False);
            Assert.That(x2.Negative, Is.True);
        });
    }

    [Test]
    public void TestAdd()
    {
        var x = new Term("x");
        var n = new Number(1);
        var s = (Sum)x.Add(n);
        Assert.Multiple(() =>
        {
            Assert.That(s.Lhs.Eq(x), Is.True);
            Assert.That(s.Rhs.Eq(n), Is.True);
        });
    }

    [Test]
    public void TestSub()
    {
        var x = new Term("x");
        var n = new Number(1);
        var s = (Sum)x.Sub(n);
        Assert.Multiple(() =>
        {
            Assert.That(s.Lhs.Eq(x), Is.True);
            Assert.That(s.Rhs.Eq(n.Neg()), Is.True);
        });
    }

    [Test]
    public void TestDiff()
    {
        var x = new Term("x");
        Assert.That(x.Diff().Eq(new Number(1)), Is.True);
        x = x.Pow(3);
        Assert.That(x.Diff().Eq(new Term("x", 3, 2)), Is.True);
        x = x.Mul(2);
        Assert.That(x.Diff().Eq(new Term("x", 6, 2)), Is.True);
    }

    [Test]
    public void TestToString()
    {
        var x = new Term("x");
        Assert.That(x.ToString(), Is.EqualTo("x"));
        x = x.Pow(2);
        Assert.That(x.ToString(), Is.EqualTo("x^2"));
        x = x.Mul(3);
        Assert.That(x.ToString(), Is.EqualTo("3*x^2"));
    }
}
