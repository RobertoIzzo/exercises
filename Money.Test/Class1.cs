using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Assert.Equal(Money.dollar(10), dollar.Times(2));
            Assert.Equal(Money.dollar(15), dollar.Times(3));
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
            Assert.Equal("CHF",Money.franc(5).currency());
        }

      
    }
}
