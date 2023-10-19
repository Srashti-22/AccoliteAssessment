using AccoliteAssessment.Core;
using AccoliteAssessment.Core.UsersAccount.GetAccountByUserId;
using AccoliteAssessment.Domain.Entities;
using Moq;

namespace AccoliteAssessment.Test.Core.UsersAccount
{
    public class GetAccountByUserIdHandlerTest
    {
        private readonly GetAccountByUserIdHandler _getAccountUserByIdHandler;
        private readonly UsersData _users;
        private readonly Mock<IUOW> _unitofWork;
        public GetAccountByUserIdHandlerTest()
        {
            _unitofWork = new Mock<IUOW>();
            _getAccountUserByIdHandler = new GetAccountByUserIdHandler(_unitofWork.Object);
            _users = new UsersData();
        }

        [Fact]
        public async void GetAccountByUserId()
        {
            var data = _users.GetUserAccountModel();
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(data);
            var result = await _getAccountUserByIdHandler.Handle(new GetAccountByUserIdRequestModel() { UserId = data.UserId, AccountId = data.Id }, default);
            Assert.NotNull(result);
            Assert.Equal(data, result);
        }

        [Fact]
        public async void GetAccountByUserIdNotExists()
        {
            UserAccountModel? userAccount = null;
            _unitofWork.Setup(x => x.userAccountRespository.GetAccountByUserId(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(userAccount);
            var result = await _getAccountUserByIdHandler.Handle(new GetAccountByUserIdRequestModel() { UserId = Guid.NewGuid(), AccountId = Guid.NewGuid() }, default);
            Assert.Null(result);
        }
    }
}
