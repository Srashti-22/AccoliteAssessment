using AccoliteAssessment.Core;
using AccoliteAssessment.Core.UsersAccount.CreateAccountByUserId;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.UsersAccount
{
    public class CreateAccountByUserIdHandlerTest
    {
        private readonly CreateAccountByUserIdHandler _createAccountUserByIdHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public CreateAccountByUserIdHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _createAccountUserByIdHandler = new CreateAccountByUserIdHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void CreateAccountByUserId()
        {
            var data = _users.GetUsers()[0];
            var userAccount = _users.AddUserAccountModel();
            _unitofWork.Setup(x => x.userRespository.GetById(It.IsAny<Guid>())).ReturnsAsync(data);
            _unitofWork.Setup(x => x.userAccountRespository.AddAccountByUserId(It.IsAny<UserAccountModel>())).ReturnsAsync(userAccount);
            var result = await _createAccountUserByIdHandler.Handle(new CreateAccountByUserIdRequestModel()
            {
                Balance = userAccount.Balance,
                Currency = userAccount.Currency,
                UserId = userAccount.UserId
            }, default);

            Assert.NotNull(result);
            Assert.Equal(result, userAccount);

        }

        [Fact]
        public async void CreateAccountByUserIdWithNullPayload()
        {
            var result = await _createAccountUserByIdHandler.Handle(null, default);
            Assert.Null(result);
        }

        [Fact]
        public async void CreateAccountByUserIdWithLessBalance()
        {
            var result = await _createAccountUserByIdHandler.Handle(new CreateAccountByUserIdRequestModel()
            {
                Balance = 50,
                Currency = "USD",
                UserId = _users.GetUsers().First().Id
            }, default);
            Assert.Null(result);
        }
    }
}
