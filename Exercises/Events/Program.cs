using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    /*
    The observer design pattern enables a subscriber to register with and receive notifications from a provider. 
    It is suitable for any scenario that requires push-based notification.
    The pattern defines a provider (also known as a subject or an observable) and zero, one, or more observers.
    Observers register with the provider, and whenever a predefined condition, event, or state change occurs,
    the provider automatically notifies all observers by calling one of their methods. 
    In this method call, the provider can also provide current state information to observers. 

    An event is a message sent by an object to signal the occurrence of an action.
    The action could be caused by user interaction, such as a button click,
    or it could be raised by some other program logic, such as changing a property’s value.
    The object that raises the event is called the event sender.
    The event sender doesn't know which object or method will receive (handle) the events it raises. 
    The event is typically a member of the event sender; for example, 
    the Click event is a member of the Button class, and the PropertyChanged event
    is a member of the class that implements the INotifyPropertyChanged interface.
    */

    class Program
    {
        static void Main(string[] args)
        {
            Provider m = new Provider();
            //observer si registra al provider
            Listener l = new Listener();
            l.Subscribe(m);
            //observable start to notify if something appen 
            m.Start();
        }
    }

    //observer
    public class Listener
    {
        public void Subscribe(Provider provider)
        {
          
            provider.Tick += HeardIt;
        }
        private void HeardIt(object sender, MyArgs e)
        {
            System.Console.WriteLine("HEARD IT : "+ e.Value);
        }
    }

    //observable
    public class Provider
    {
        public event EventHandler<MyArgs> Tick =  delegate { };
       
        public void Start()
        {
            int count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                if (Tick != null)
                {
                    count++;
                    MyArgs myArgs = new MyArgs(count);
                    Tick(this, myArgs);
                }
            }
        }
    }

    public class MyArgs : EventArgs
    {
        public MyArgs(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
    }

}
