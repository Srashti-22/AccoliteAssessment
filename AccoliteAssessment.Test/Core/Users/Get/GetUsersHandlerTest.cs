using AccoliteAssessment.Core;
using AccoliteAssessment.Core.Users.Get;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.Users
{
    public class GetUsersHandlerTest
    {
        private readonly GetUsersHandler _getUsersHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public GetUsersHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _getUsersHandler = new GetUsersHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void GetAllUsers()
        {
            var data = _users.GetUsers();
            // _userRepo.Setup(x => x.GetAll()).ReturnsAsync(data);
            _unitofWork.Setup(x => x.userRespository.GetAll()).ReturnsAsync(data);
            var result = await _getUsersHandler.Handle(new GetUserRequestModel() { }, default);
            Assert.NotNull(result);
            Assert.Equal(data.Count, result.Count);
        }

        [Fact]
        public async void GetNoUsers()
        {
            var data = _users.GetUsers().First();
            // _userRepo.Setup(x => x.GetAll()).ReturnsAsync(new List<UserModel>() { });
            _unitofWork.Setup(x => x.userRespository.GetAll()).ReturnsAsync(new List<UserModel>() { });
            var result = await _getUsersHandler.Handle(new GetUserRequestModel() { }, default);
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
