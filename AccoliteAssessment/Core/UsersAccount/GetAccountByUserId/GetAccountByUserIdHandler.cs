using AccoliteAssessment.Domain.Entities;
using MediatR;

namespace AccoliteAssessment.Core.UsersAccount.GetAccountByUserId
{
    public class GetAccountByUserIdRequestModel : IRequest<UserAccountModel>
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
    }
    public class GetAccountByUserIdHandler : IRequestHandler<GetAccountByUserIdRequestModel, UserAccountModel>
    {
        private readonly IUOW _unitOfWork;
        public GetAccountByUserIdHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserAccountModel> Handle(GetAccountByUserIdRequestModel request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.userAccountRespository.GetAccountByUserId(request.UserId, request.AccountId);
        }
    }
}
