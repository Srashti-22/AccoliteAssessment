using AccoliteAssessment.Core;
using AccoliteAssessment.Core.UsersAccount.GetAccountByUserId;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.UsersAccount
{
    public class DeleteAccountByUserIdHandlerTest
    {
        private readonly DeleteAccountByUserIdHandler _deleteAccountByUserIdHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public DeleteAccountByUserIdHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _deleteAccountByUserIdHandler = new DeleteAccountByUserIdHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void DeleteUser()
        {
            var data = _users.GetUserAccountModel();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            _unitofWork.Setup(x => x.userAccountRespository.DeleteAccountByUserId(data));
            var result = await _deleteAccountByUserIdHandler.Handle(new DeleteAccountByUserIdRequestModel() { UserId = data.UserId, AccountId = data.Id }, default);
            Assert.NotNull(result);
            Assert.Equal("Successfully Deleted", result);
        }

        [Fact]
        public async void DeleteAccountByUserIdNotExists()
        {
            UserAccountModel? account = null;
            var data = _users.GetUsers().First();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(account);
            _unitofWork.Setup(x => x.userAccountRespository.DeleteAccountByUserId(account));
            var result = await _deleteAccountByUserIdHandler.Handle(new DeleteAccountByUserIdRequestModel() { UserId = Guid.NewGuid(), AccountId = Guid.NewGuid() }, default);
            Assert.NotNull(result);
            Assert.Equal("Failure", result);
        }
    }
}
