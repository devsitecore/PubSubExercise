using PubSubSample.Common.Unity;
using PubSubSample.Foundation.Base;
using PubSubSample.Foundation.Proxy;
using PubSubSample.Foundation.ServiceContracts;
using PubSubSample.Tests.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using PubSubSample.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PubSubSample.Tests.Base
{
    [TestClass]
    public class BaseTests
    {
        protected IProxyManager proxyManager;
        protected MockPubSubServerHost mockPubSubHost;
        protected IPubSubServicesHostingManager pubSubServicesHostingManager;
        protected UnityContainer container;

        protected void WaitFor(int millisecondsDelay)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(millisecondsDelay);
                return 0;
            });

            t.Wait();
        }

        public void Initialize()
        {
            this.container = DependencyInjection.Instance().Initialize();

            this.proxyManager = this.container.Resolve<IProxyManager>();
            this.pubSubServicesHostingManager = this.container.Resolve<IPubSubServicesHostingManager>();
            this.mockPubSubHost = this.container.Resolve<MockPubSubServerHost>();
        }

        public BaseTests()
        {
            Initialize();
        }
    }
}
