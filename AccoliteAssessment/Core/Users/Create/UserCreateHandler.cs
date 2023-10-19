using AccoliteAssessment.Domain.Entities;
using MediatR;
using System.Runtime.Serialization;

namespace AccoliteAssessment.Core.Users.Create
{
    public class AddUser : User
    {
        public List<Account> userAccounts { get; set; }
    }
    public class AddUserRequestModel : AddUser, IRequest<UserModel>
    {
    }
    public class UserCreateHandler : IRequestHandler<AddUserRequestModel, UserModel>
    {
        private readonly IUOW _unitOfWork;
        public UserCreateHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel> Handle(AddUserRequestModel request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                if(request.userAccounts.Any(x=> x.Balance < 100))
                {
                    return null;
                }
                var data = new UserModel()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Age = request.Age,
                    Sex = request.Sex,
                    Phone = request.Phone,
                    UserAccounts = request.userAccounts.Select(x => new UserAccountModel()
                    {
                        Balance = x.Balance,
                        Currency = x.Currency,
                        CreatedAt = DateTime.UtcNow,
                    }).ToList()
                };
                var result = await _unitOfWork.userRespository.Add(data);
                return result;
            }
            return null;
        }
    }
}
