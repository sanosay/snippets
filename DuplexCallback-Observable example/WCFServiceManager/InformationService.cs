using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFService.Contracts;

namespace WCFServiceManager
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single,InstanceContextMode=InstanceContextMode.Single)]
    public class InformationService : IInformationService
    {
        private static List<IInformationServiceListener> _listeners = new List<IInformationServiceListener>();
        private static object _lock = new object();
        private static bool hasStarted = false;
        public InformationService() {
            var s = "";
        }
        public void Subscribe()
        {
            lock (_lock) {
                if (hasStarted == false) {
                    hasStarted = true;
                    new Thread(() => {
                        StartService();
                    }).Start();
                    
                }
            }
            var listener = OperationContext.Current.GetCallbackChannel<IInformationServiceListener>();
            lock (_lock) {
                if (!_listeners.Contains(listener)) {
                    _listeners.Add(listener);
                }
            }
        }

        public void Unsubscribe()
        {
            var listener = OperationContext.Current.GetCallbackChannel<IInformationServiceListener>();
            lock (_lock) {
                if (_listeners.Contains(listener)) {
                    _listeners.Remove(listener);
                }
            }
        }
        public void StartService() {
            int i = 0;
            while (true) {
                lock (_lock)
                {
                    var toLoop = _listeners.ToList();
                    toLoop.ForEach(foo =>
                    {
                        try
                        {
                            
                            foo.OnServiceStateChanged("State changed! " + i);
                        }
                        catch (Exception ex) {
                            _listeners.Remove(foo);
                        }
                    });
                }
                i++;
                Thread.Sleep(1000);
            }
        }
    }
}
