using InStudio.Data;
using InStudio.Data.Models;
using InStudio.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InStudio.Services.Repositories
{
    public class UserSubscriptionTypeRepository : GenericRepository<UserSubscriptionType>, IUserSubscriptionTypeRepository
    {
        public UserSubscriptionTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
