using AccoliteAssessment.Domain.Entities;
using MediatR;

namespace AccoliteAssessment.Core.Users.Get
{
    public class GetUserRequestModel : IRequest<List<UserModel>>
    {
    }

    public class GetUsersHandler : IRequestHandler<GetUserRequestModel, List<UserModel>>
    {
        private readonly IUOW _unitOfWork;
        public GetUsersHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<UserModel>> Handle(GetUserRequestModel request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.userRespository.GetAll();
        }
    }
}
