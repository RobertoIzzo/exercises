using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Money.Test
{

    public class Class1
    {
        [Fact]
        public void TestMultiplication()
        {
            Money dollar = Money.dollar(5);
            Assert.Equal(Money.dollar(10), dollar.times(2));
            Assert.Equal(Money.dollar(15), dollar.times(3));
        }

        [Fact]
        public void TestSampleAddiction()
        {
            Money five = Money.dollar(5);
            Expression sum = five.plus(Money.dollar(5));
            Bank bank = new Bank();
            Money reduced = bank.reduce(sum, "USD");
            Assert.Equal(Money.dollar(10), reduced);
        }

        [Fact]
        public void TestPlusReturnSum()
        {
            Money five = Money.dollar(5);
            Expression result = five.plus(Money.dollar(5));
            Sum sum = (Sum)result;
            Assert.Equal(five, sum.augend);
            Assert.Equal(five, sum.addend);
        }

        [Fact]
        public void TestEquality()
        {
            Assert.True(Money.dollar(5).Equals(Money.dollar(5)));
            Assert.False(Money.dollar(5).Equals(Money.dollar(6)));
            Assert.False(Money.franc(5).Equals(Money.dollar(5)));

        }

        [Fact]
        public void TestCurrency()
        {
            Assert.Equal("USD", Money.dollar(5).currency());
            Assert.Equal("CHF", Money.franc(5).currency());
        }

        [Fact]
        public void TestReduceSum()
        {
            Bank bank = new Bank();
            Money result = bank.reduce(Money.dollar(1), "USD");
            Assert.Equal(Money.dollar(1), result);
        }

        [Fact]
        public void TestReduceMoneyDifferentCountry()
        {
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Money result = bank.reduce(Money.franc(2), "USD");
            Assert.Equal(Money.dollar(1), result);

        }

        [Fact]
        public void TestArrayEquals()
        {
            Assert.Equal(new Object[] { "abc" }, new Object[] { "abc" });
        }

        [Fact]
        public void TestIdentityRAte()
        {
            Assert.Equal(1, new Bank().rate("USD", "USD"));
        }

        [Fact]
        public void TestMixedAddition()
        {
            Expression fiveBucks = Money.dollar(5);
            Expression tenFrancs = Money.franc(10);
            Bank bank = new Bank();
            bank.addRate("CHF","USD",2);
            Money result = bank.reduce(fiveBucks.plus(tenFrancs), "USD");
            Assert.Equal(Money.dollar(10), result);
        }

        [Fact]
        public void TestSumPlusMoney()
        {
            Expression fiveBucks = Money.dollar(5);
            Expression tenFrancs = Money.franc(10);
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Expression sum = new Sum(fiveBucks, tenFrancs).plus(fiveBucks);
            Money result = bank.reduce(fiveBucks.plus(tenFrancs), "USD");
            Assert.Equal(Money.dollar(10), result);
        }

        [Fact]
        public void TestSumTimes()
        {
            Expression fiveBucks = Money.dollar(5);
            Expression tenFrancs = Money.franc(10);
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Expression sum = new Sum(fiveBucks, tenFrancs).times(2);
            Money result = bank.reduce(fiveBucks.plus(tenFrancs), "USD");
            Assert.Equal(Money.dollar(10), result);
        }

        [Fact]
        public void testPlusSanCurrebùncyreturnsmoney()
        {
            Expression sum = Money.dollar(1).plus(Money.dollar(1));
            sum.
        }
    }
}
