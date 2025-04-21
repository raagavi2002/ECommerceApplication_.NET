using ECommerce.Application.Interfaces;
using ECommerceApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDBContext ApplicationDBContext { get; set; }

        public UserRepository(ApplicationDBContext applicationDBContext)
        {
            ApplicationDBContext = applicationDBContext;
        }

        public string GetUserNameFromUserId(string userId)
        {
            return this.ApplicationDBContext.Appusers.AsNoTracking().Where(i => i.Userid == userId).Select(i => i.Username).FirstOrDefault() ?? string.Empty;
        }
    }
}
