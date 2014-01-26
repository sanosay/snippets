using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Contracts
{
    [ServiceContract(CallbackContract=typeof(IInformationServiceListener))]
    public interface IInformationService
    {
        [OperationContract]
        void Subscribe();

        [OperationContract]
        void Unsubscribe();
       
    }
}
