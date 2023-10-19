using AccoliteAssessment.Domain.Entities;

namespace AccoliteAssessment.Persistence.Repositories.User
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAll();
        public Task<UserModel> GetById(Guid id);
        public Task<UserModel> Add(UserModel entity);
        public void Delete(UserModel entity);
    }
}
