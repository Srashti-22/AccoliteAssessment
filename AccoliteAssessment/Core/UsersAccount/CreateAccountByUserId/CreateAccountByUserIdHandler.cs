using AccoliteAssessment.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace AccoliteAssessment.Core.UsersAccount.CreateAccountByUserId
{
    public class CreateAccountByUserIdRequestModel : Account, IRequest<UserAccountModel>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
    public class CreateAccountByUserIdHandler : IRequestHandler<CreateAccountByUserIdRequestModel, UserAccountModel>
    {
        private readonly IUOW _unitOfWork;
        public CreateAccountByUserIdHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserAccountModel> Handle(CreateAccountByUserIdRequestModel request, CancellationToken cancellationToken)
        {
            if (request != null && request.Balance >= 100)
            {
                var user = await _unitOfWork.userRespository.GetById(request.UserId);
                if (user != null)
                {
                    var data = new UserAccountModel()
                    {
                        Balance = request.Balance,
                        CreatedAt = DateTime.UtcNow,
                        Currency = request.Currency,
                        UserId = request.UserId,
                        Id = Guid.NewGuid()
                    };
                    var result = await _unitOfWork.userAccountRespository.AddAccountByUserId(data);
                    return result;
                }
            }
            return null;
        }
    }
}
