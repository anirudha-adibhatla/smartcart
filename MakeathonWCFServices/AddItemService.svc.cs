using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MakeathonWCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddItemService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AddItemService.svc or AddItemService.svc.cs at the Solution Explorer and start debugging.
    public class AddItemService : IAddItemService
    {
        static Action<string> itemAddedEvent = delegate { };
        public void DoWork()
        {
        }

        public void ItemAdded(string name)
        {
            IAddItemServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IAddItemServiceEvents>();
            itemAddedEvent += subscriber.IsItemAdded;
        }
    }
}
