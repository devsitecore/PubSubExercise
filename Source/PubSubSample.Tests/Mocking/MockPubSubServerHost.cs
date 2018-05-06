using PubSubSample.Foundation.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Tests.Mocking
{
    public sealed class MockPubSubServerHost : IPubSubServerHost
    {
        public List<string> Messages { get; private set; }

        public MockPubSubServerHost()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            if (this.Messages == null)
            {
                this.Messages = new List<string>();
            }
            else
            {
                this.Messages.Clear();
            }
        }

        public void Notify(string message)
        {
            this.Messages.Add(message);
        }
    }
}
