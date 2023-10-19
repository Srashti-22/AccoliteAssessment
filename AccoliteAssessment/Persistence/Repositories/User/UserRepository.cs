using AccoliteAssessment.Domain.Entities;
using AccoliteAssessment.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccoliteAssessment.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly BankingContext _bankingContext;
        public UserRepository(BankingContext bankingContext)
        {
            _bankingContext = bankingContext;
        }
        public async Task<UserModel> Add(UserModel entity)
        {
            var result = await _bankingContext.User.AddAsync(entity);
            await _bankingContext.SaveChangesAsync();
            return result.Entity;
        }

        public void Delete(UserModel entity)
        {
            _bankingContext.User.Remove(entity);
            _bankingContext.SaveChanges();
            return;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _bankingContext.User.Include(x => x.UserAccounts).AsNoTracking().ToListAsync();
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var result = await _bankingContext.User.Where(x => x.Id == id).Include(x => x.UserAccounts).AsNoTracking().FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }
    }
}
