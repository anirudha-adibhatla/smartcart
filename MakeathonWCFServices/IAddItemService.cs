using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MakeathonWCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAddItemService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IAddItemServiceEvents))]
    public interface IAddItemService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        void ItemAdded(string name);

    }
}
