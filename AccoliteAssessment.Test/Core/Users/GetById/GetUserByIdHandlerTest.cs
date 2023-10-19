using AccoliteAssessment.Core;
using AccoliteAssessment.Core.Users.GetById;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.Users
{
    public class GetUserByIdHandlerTest
    {
        private readonly GetUserByIdHandler _getUserByIdHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        // private readonly Mock<IUserRepository> _userRepo;
        public GetUserByIdHandlerTest()
        {
            // _userRepo = new Mock<IUserRepository>();
            _unitofWork = new Mock<IUOW>();
            _getUserByIdHandler = new GetUserByIdHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void GetUserById()
        {
            var data = _users.GetUsers().First();
            //  _userRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(data);
            _unitofWork.Setup(x => x.userRespository.GetById(It.IsAny<Guid>())).ReturnsAsync(data);
            var result = await _getUserByIdHandler.Handle(new GetUserByIdRequestModel() { UserId = data.Id }, default);
            Assert.NotNull(result);
            Assert.Equal(data, result);
        }

        [Fact]
        public async void GetUserByIdNotExists()
        {
            UserModel? user = null;
            //  _userRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(emp);
            _unitofWork.Setup(x => x.userRespository.GetById(It.IsAny<Guid>())).ReturnsAsync(user);
            var result = await _getUserByIdHandler.Handle(new GetUserByIdRequestModel() { UserId = Guid.NewGuid() }, default);
            Assert.Null(result);
        }
    }
}
