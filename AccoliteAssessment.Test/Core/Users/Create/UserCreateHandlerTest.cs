using AccoliteAssessment.Core;
using AccoliteAssessment.Core.Users.Create;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.Users
{
    public class UserCreateHandlerTest
    {
        private readonly UserCreateHandler _createUserHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public UserCreateHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _createUserHandler = new UserCreateHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void CreateUser()
        {
            var data = _users.AddUser();
            var create = _users.AddUserRequestModel();
            _unitofWork.Setup(x => x.userRespository.Add(It.IsAny<UserModel>())).ReturnsAsync(data);
            var result = await _createUserHandler.Handle(create, default);
            Assert.NotNull(result);
            Assert.Equal(data.FirstName, create.FirstName);
            Assert.Equal(data.LastName, create.LastName);
            Assert.Equal(data.Email, create.Email);
        }

        [Fact]
        public async void CreateUserWithNullPayload()
        {
            var result = await _createUserHandler.Handle(null, default);
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUserWithLessBalance()
        {
            var create = _users.AddUserRequestModel();
            create.userAccounts[0].Balance = 50;
            var result = await _createUserHandler.Handle(create, default);
            Assert.Null(result);
        }
    }
}
