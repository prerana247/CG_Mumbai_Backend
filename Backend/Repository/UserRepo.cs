using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly CGDbContext _DbContext;

        public UserRepo(CGDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _DbContext.User.ToListAsync();
        }

        public async Task<User> RegisterUser(User User)
        {
            var result = await _DbContext.User.AddAsync(User);
            await _DbContext.SaveChangesAsync();
            return result.Entity;
        }




    }
}
