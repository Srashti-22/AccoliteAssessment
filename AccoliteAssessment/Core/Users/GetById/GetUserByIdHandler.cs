using AccoliteAssessment.Domain.Entities;
using MediatR;

namespace AccoliteAssessment.Core.Users.GetById
{
    public class GetUserByIdRequestModel : IRequest<UserModel>
    {
        public Guid UserId { get; set; }
    }
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequestModel, UserModel>
    {
        private readonly IUOW _unitOfWork;
        public GetUserByIdHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserModel> Handle(GetUserByIdRequestModel request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.userRespository.GetById(request.UserId);
        }
    }
}
