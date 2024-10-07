using InStudio.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Common.Services
{
    public class ScopeContext : IScopeContext, IScopeContextSetter
    {
        public Guid UserId { get; private set; }
        public string? UserName { get; private set; }
        public Guid RoleId { get; private set; }  
        public string? RoleName { get; private set; } 

        public void SetUser(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
