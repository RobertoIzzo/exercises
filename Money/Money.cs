using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Money : Expression
    {
        public int _amount;
        protected string _currency;

        public Money(int amount, string currency)
        {
            _amount = amount;
            _currency = currency;
        }


        public override bool Equals(object obj)
        {
            Money money = (Money)obj;
            return _amount == money._amount
                && currency().Equals(money.currency());
        }


        public static Money dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money franc(int amount)
        {
            return new Money(amount,"CHF");
        }
     
        public  string currency()
        {
            return _currency;
        }

        public String toString()
        {
            return _amount + " " + _currency;
        }

        public Expression plus(Expression addend)
        {
            return new Sum(this, addend);
        }

        public Expression times(int multiplier)
        {
            return new Money(_amount * multiplier, _currency);
        }


        public Money reduce(Bank bank, string to)
        {
            int rate = bank.rate(_currency, to);
            return new Money(_amount /rate,to);
        }

       
    }

    public class Sum : Expression
    {
        public Expression augend;
        public Expression addend;

        public Sum(Expression augend, Expression addend)
        {
            this.augend = augend;
            this.addend = addend;
        }

        public Money reduce(Bank bank, string to)
        {
            int amount = augend.reduce(bank, to)._amount
                + addend.reduce(bank,to)._amount;
            return new Money(amount,to);
        }

        public Expression plus(Expression addend)
        {
            return new Sum(this,addend);
        }

        public Expression times(int multiplier)
        {
            return new Sum(augend.times(multiplier),addend.times(multiplier));
        }
    }

    public class Bank
    {
        private Hashtable rates = new Hashtable();

        public int rate(string from , string to)
        {
            if (from.Equals(to)) return 1;
            try
            {
                //var ret =  rates[new Pair(from, to)];
                int ret = 2;
                return (int)ret;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
        public Money reduce(Expression source, string to)
        {
            return source.reduce(this,to);
        }

        public void addRate(string from, string to, int rate)
        {
            rates.Add(new Pair(from,to),rate );
        }
    }

    public class Pair
    {
        private string from;
        private string to;

        public Pair(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public bool equals(Object obj)
        {
            Pair pair = (Pair)obj;
            return from.Equals(pair.from) && to.Equals(pair.to);
        }

        public int hashCode()
        {
            return 0;
        }
    }

    public interface Expression
    {
        Money reduce(Bank bank, string to);
        Expression plus(Expression addend);
        Expression times(int multiplier);
    }
}
