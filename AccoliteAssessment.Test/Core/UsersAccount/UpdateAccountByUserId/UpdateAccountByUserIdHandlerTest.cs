using AccoliteAssessment.Core;
using AccoliteAssessment.Core.UsersAccount.UpdateAccountByUserId;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.UsersAccount
{
    public class UpdateAccountByUserIdHandlerTest
    {
        private readonly UpdateAccountByUserIdHandler _updateAccountUserByIdHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public UpdateAccountByUserIdHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _updateAccountUserByIdHandler = new UpdateAccountByUserIdHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void UpdateAccountByUserId()
        {
            var data = _users.GetUserAccountModel();
            var userAccount = _users.UpdateUserAccountModel();
            userAccount.UserId = data.UserId;
            userAccount.Id = data.Id;
            userAccount.Balance = 450;

            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            _unitofWork.Setup(x => x.userAccountRespository.UpdateAccountByUserId(It.IsAny<UserAccountModel>())).ReturnsAsync(userAccount);
            var result = await _updateAccountUserByIdHandler.Handle(new UpdateAccountByUserIdRequestModel()
            {
                Amount = 250,
                Action = BankAction.Deposit,
                UserId = data.UserId,
                AccountId = data.Id,
            }, default);

            Assert.NotNull(result);
            Assert.Equal(result.Balance, 450);
            Assert.Equal(result.UserId, userAccount.UserId);
            Assert.Equal(result.Id, userAccount.Id);
        }

        [Fact]
        public async void UpdateAccountByUserId_Deposit_GreaterThan10000()
        {
            var data = _users.GetUserAccountModel();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            var result = await _updateAccountUserByIdHandler.Handle(new UpdateAccountByUserIdRequestModel()
            {
                Amount = 11000,
                Action = BankAction.Deposit,
                UserId = data.UserId,
                AccountId = data.Id,
            }, default);

            Assert.NotNull(result);
            Assert.Equal(result.ErrorMessage, "user cannot deposit more than $10,000 in a single transaction");
        }

        [Fact]
        public async void UpdateAccountByUserId_Withdrawl_GreaterThan90Percent()
        {
            var data = _users.GetUserAccountModel();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            var result = await _updateAccountUserByIdHandler.Handle(new UpdateAccountByUserIdRequestModel()
            {
                Amount = 190,
                Action = BankAction.Withdrawl,
                UserId = data.UserId,
                AccountId = data.Id,
            }, default);

            Assert.NotNull(result);
            Assert.Equal(result.ErrorMessage, "user cannot withdraw more than 90% of their total balance from an account in a single transaction");
        }

        [Fact]
        public async void UpdateAccountByUserId_Balance_LessThan100()
        {
            var data = _users.GetUserAccountModel();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            var result = await _updateAccountUserByIdHandler.Handle(new UpdateAccountByUserIdRequestModel()
            {
                Amount = 120,
                Action = BankAction.Withdrawl,
                UserId = data.UserId,
                AccountId = data.Id,
            }, default);

            Assert.NotNull(result);
            Assert.Equal(result.ErrorMessage, "account cannot have less than $100 at any time");
        }
        [Fact]
        public async void UpdateAccountByUserId_InvalidAction()
        {
            var data = _users.GetUserAccountModel();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            var result = await _updateAccountUserByIdHandler.Handle(new UpdateAccountByUserIdRequestModel()
            {
                Amount = 120,
                Action = "Test",
                UserId = data.UserId,
                AccountId = data.Id,
            }, default);

            Assert.NotNull(result);
            Assert.Equal(result.ErrorMessage, "user can either Deposit or Withdraw amount from an account");
        }
    }
}
