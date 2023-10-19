using AccoliteAssessment.Persistence.Repositories.User;
using AccoliteAssessment.Persistence.Repositories.UserAccount;

namespace AccoliteAssessment.Core
{
    public interface IUOW
    {
        IUserRepository userRespository { get; }
        IUserAccountRepository userAccountRespository { get; }
    }
}
