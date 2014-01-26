using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFService.Contracts;

namespace WCFServiceObserver
{
    public class InformationServiceListener : IInformationServiceListener
    {
        int i = 0;
        public InformationServiceListener() {
            i = int.MaxValue;
        }
        public InformationServiceListener(int z) {
            i = z;
        }
        public void OnServiceStateChanged(string state)
        {
            if (i < 0) {
                Thread.CurrentThread.Abort();
            }
            Console.WriteLine(state);
            i--;
        }
    }
}
