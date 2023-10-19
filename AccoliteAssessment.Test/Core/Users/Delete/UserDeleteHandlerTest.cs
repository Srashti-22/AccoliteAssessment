using AccoliteAssessment.Core;
using AccoliteAssessment.Core.Users.Delete;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.Users
{
    public class UserDeleteHandlerTest
    {
        private readonly UserDeleteHandler _deleteUserHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public UserDeleteHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _deleteUserHandler = new UserDeleteHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void DeleteUser()
        {
            var data = _users.GetUsers().First();
            _unitofWork.Setup(x => x.userRespository.GetById(It.IsAny<Guid>())).ReturnsAsync(data);
            _unitofWork.Setup(x => x.userRespository.Delete(It.IsAny<UserModel>()));
            var result = await _deleteUserHandler.Handle(new DeleteUserRequestModel() { UserId = data.Id }, default);
            Assert.NotNull(result);
            Assert.Equal("Successfully Deleted", result);
        }


        [Fact]
        public async void DeleteUserByIdNotExists()
        {
            UserModel? user = null;
            var data = _users.GetUsers().First();
            _unitofWork.Setup(x => x.userRespository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            _unitofWork.Setup(x => x.userRespository.Delete(It.IsAny<UserModel>()));
            var result = await _deleteUserHandler.Handle(new DeleteUserRequestModel() { UserId = Guid.NewGuid() }, default);
            Assert.Null(result);
        }
    }
}
