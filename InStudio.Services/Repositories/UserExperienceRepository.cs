using InStudio.Data;
using InStudio.Data.Models;
using InStudio.Services.Repositories.Interfaces;

namespace InStudio.Services.Repositories
{
    public class UserExperienceRepository : GenericRepository<UserExperience>, IUserExperienceRepository
    {
        public UserExperienceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
