using InStudio.Data;
using InStudio.Data.Models;
using InStudio.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
