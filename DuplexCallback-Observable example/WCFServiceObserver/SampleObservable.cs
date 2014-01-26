using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCFService.Contracts;

namespace WCFServiceObserver
{
    public class SampleObservable : DuplexClientBase<IInformationService>
    {
        public SampleObservable(object callbackInstance, Binding binding, EndpointAddress remoteAddress)
            : base(callbackInstance, binding, remoteAddress) { }
    }
    
}
