using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFService.Contracts;
using WCFServiceManager;

namespace WCFServiceObserver
{
    class Program
    {
        static object _lock = new object();
        public static void Main(string[] args)
        {
            var uri = new Uri("net.tcp://localhost");
            var binding = new NetTcpBinding();
            var host = new ServiceHost(typeof(InformationService), uri);
            host.AddServiceEndpoint(typeof(IInformationService), binding, "");
            host.Open();
           
            for (int i = 0; i < 2; i++)
            {
                   
                    new Thread((z) =>
                    {
                        var callback = new InformationServiceListener();
                        var channel = DuplexChannelFactory<IInformationService>.CreateChannel(callback, binding, new EndpointAddress(uri));

                        channel.Subscribe();

                    }).Start(i);
                
                
            }
            var _callback = new InformationServiceListener(2);
            var client = new SampleObservable(_callback, binding, new EndpointAddress(uri));
            var proxy = client.ChannelFactory.CreateChannel();
            proxy.Subscribe();
            new Thread(() => { while (true) { Thread.Sleep(1000); } });
            client.Close();
            Thread.CurrentThread.Join();
            // Printed in console:
            //  Hi from server!
            //  Hi from client!

            client.Close();
            host.Close();
        }
    }
}
