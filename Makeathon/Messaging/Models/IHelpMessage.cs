using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makeathon.Messaging.Models
{
    interface IHelpMessage
    {
        string HelpMessage { get; set; }
        string HelpImageURL { get; set; }

        void ShowHelp();
    }
}
