using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.UI.Controls
{
    interface IControlFinder
    {
        IWebControl Find(string name, string type, string frame);
    }
}
