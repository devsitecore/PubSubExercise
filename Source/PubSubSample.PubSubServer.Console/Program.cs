using PubSubSample.Common.Unity;
using PubSubSample.Foundation.ServiceContracts;
using PubSubSample.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Launcher : IPubSubServerHost
    {
        public Launcher()
        {
            var manager = DependencyInjection.Instance().Initialize().Resolve<IPubSubServicesHostingManager>();
            manager.InitializeServicesHosting(this);
        }
        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }
    class Program
    {
        public Program()
        {
        }

        static void Main(string[] args)
        {
            Console.Write("Hello");
            new Launcher();
            Console.ReadLine();
        }
    }
}
