using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Common.Services.Interfaces
{
    public interface IScopeContext
    {
        Guid UserId { get; }
        string UserName { get; }
        Guid RoleId { get; } 
        string RoleName { get; }
    }
}
