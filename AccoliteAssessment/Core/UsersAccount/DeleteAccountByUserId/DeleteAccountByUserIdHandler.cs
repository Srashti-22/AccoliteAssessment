using MediatR;

namespace AccoliteAssessment.Core.UsersAccount.GetAccountByUserId
{
    public class DeleteAccountByUserIdRequestModel : IRequest<string>
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
    }
    public class DeleteAccountByUserIdHandler : IRequestHandler<DeleteAccountByUserIdRequestModel, string>
    {
        private readonly IUOW _unitOfWork;
        public DeleteAccountByUserIdHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<string> Handle(DeleteAccountByUserIdRequestModel request, CancellationToken cancellationToken)
        {
            if(request != null)
            {
                var user = await _unitOfWork.userAccountRespository.GetAccountByUserId(request.UserId, request.AccountId);
                if (user != null)
                {
                    _unitOfWork.userAccountRespository.DeleteAccountByUserId(user);
                    return "Successfully Deleted";
                }
            }
            return "Failure";
        }
    }
}
