using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Common.Services.Interfaces
{
    public interface IScopeContextSetter
    {
        void SetUser(Guid userId, string userName);
    }
}
