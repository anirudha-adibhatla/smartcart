using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MakeathonWCFServices
{
    interface IAddItemServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void IsItemAdded(string name);       
    }
}
