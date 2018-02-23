using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progetto1;
using Xunit;

namespace TestMyCode
{
    [Category("Unit")]
    public class Class1
    {
        [Fact]
        public void TestMe()
        {
            //ARRANGE
            Pippo sut = new Pippo(new AccountService());
            //act
            Action act = () => sut.AddNewUser();
            //assert
            Assert.Throws<NotImplementedException>(act);
        }

    }
}
