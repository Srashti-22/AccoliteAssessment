using MediatR;

namespace AccoliteAssessment.Core.Users.Delete
{
    public class DeleteUserRequestModel : IRequest<string>
    {
        public Guid UserId { get; set; }
    }
    public class UserDeleteHandler : IRequestHandler<DeleteUserRequestModel, string>
    {
        private readonly IUOW _unitOfWork;
        public UserDeleteHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteUserRequestModel request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                var user = await _unitOfWork.userRespository.GetById(request.UserId);
                if (user != null)
                {
                    _unitOfWork.userRespository.Delete(user);
                    return "Successfully Deleted";
                }
            }
            return null;
        }
    }
}
