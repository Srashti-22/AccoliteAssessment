using AccoliteAssessment.Domain.Entities;

namespace AccoliteAssessment.Persistence.Repositories.UserAccount
{
    public interface IUserAccountRepository
    {
        public Task<UserAccountModel> GetAccountByUserId(Guid userId, Guid accountId);
        public Task<UserAccountModel> AddAccountByUserId(UserAccountModel entity);
        public void DeleteAccountByUserId(UserAccountModel entity);
        public Task<UserAccountModel> UpdateAccountByUserId(UserAccountModel entity);
    }
}
