using AccoliteAssessment.Persistence.Context;
using AccoliteAssessment.Persistence.Repositories.User;
using AccoliteAssessment.Persistence.Repositories.UserAccount;

namespace AccoliteAssessment.Core
{
    public class UnitOfWork : IUOW
    {
        private readonly BankingContext _bankingContext;

        public UnitOfWork(BankingContext bankingContext)
        {
            _bankingContext = bankingContext;
        }

        public IUserRepository userRespository { get { return new UserRepository(_bankingContext); } }

        public IUserAccountRepository userAccountRespository
        {
            get
            {
                return new UserAccountRepository(_bankingContext);
            }
        }
    }
}
