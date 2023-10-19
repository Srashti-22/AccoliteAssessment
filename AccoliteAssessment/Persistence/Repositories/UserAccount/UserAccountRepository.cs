using AccoliteAssessment.Domain.Entities;
using AccoliteAssessment.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccoliteAssessment.Persistence.Repositories.UserAccount
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly BankingContext _bankingContext;
        public UserAccountRepository(BankingContext bankingContext)
        {
            _bankingContext = bankingContext;
        }

        public async Task<UserAccountModel> AddAccountByUserId(UserAccountModel entity)
        {
            var result = await _bankingContext.UserAccount.AddAsync(entity);
            await _bankingContext.SaveChangesAsync();
            return result.Entity;

        }

        public void DeleteAccountByUserId(UserAccountModel entity)
        {
            _bankingContext.UserAccount.Remove(entity);
            _bankingContext.SaveChanges();
            return;
        }

        public async Task<UserAccountModel> GetAccountByUserId(Guid userId, Guid accountId)
        {
            var result =  await _bankingContext.UserAccount.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId && x.Id == accountId);
            return result;
        }

        public async Task<UserAccountModel> UpdateAccountByUserId(UserAccountModel entity)
        {
            var result =  _bankingContext.UserAccount.Update(entity);
            _bankingContext.SaveChanges();
            return result.Entity;

        }
    }
}
