using AccoliteAssessment.Api;
using AccoliteAssessment.Core;
using AccoliteAssessment.Core.UsersAccount.CreateAccountByUserId;
using AccoliteAssessment.Core.UsersAccount.GetAccountByUserId;
using AccoliteAssessment.Core.UsersAccount.UpdateAccountByUserId;
using AccoliteAssessment.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AccoliteAssessment.Test.Api
{
    public class UsersAccountControllerTest
    {
        private readonly Mock<IMediator> _mediatR;
        private readonly UsersAccountController _controller;
        private readonly UsersData _users;
        public UsersAccountControllerTest()
        {
            _mediatR = new Mock<IMediator>();
            _controller = new UsersAccountController(_mediatR.Object);
            _users = new UsersData();
        }

        #region GetAccountByUserId
        [Fact]
        public async void GetAccountByUserId()
        {
            var account = _users.GetUserAccountModel();
            _mediatR.Setup(x => x.Send(It.IsAny<GetAccountByUserIdRequestModel>(), default)).ReturnsAsync(account);
            var result = await _controller.Get(account.UserId,  account.Id);
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, account);
        }

        [Fact]
        public async void GetAccountByUserIdWithInvalidId()
        {
            UserAccountModel? account = null;
            _mediatR.Setup(x => x.Send(It.IsAny<GetAccountByUserIdRequestModel>(), default)).ReturnsAsync(account);
            var result = await _controller.Get(Guid.NewGuid(), Guid.NewGuid());
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 404);
            Assert.Equal(((ObjectResult)result).Value, "Account Not Found");
        }
        #endregion

        #region DeleteAccountByUserId
        [Fact]
        public async void DeleteAccountByUserId()
        {
            var account = _users.GetUserAccountModel();
            _mediatR.Setup(x => x.Send(It.IsAny<DeleteAccountByUserIdRequestModel>(), default)).ReturnsAsync("Successfully Deleted");
            var result = await _controller.Delete(account.UserId, account.Id);
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, "Successfully Deleted");
        }

        [Fact]
        public async void DeleteAccountByUserIdWithInvalidId()
        {
            _mediatR.Setup(x => x.Send(It.IsAny<DeleteAccountByUserIdRequestModel>(), default)).ReturnsAsync(string.Empty);
            var result = await _controller.Delete(Guid.NewGuid(), Guid.NewGuid());
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 404);
            Assert.Equal(((ObjectResult)result).Value, "Account Not Found");
        }
        #endregion

        #region AddAccountByUserId
        [Fact]
        public async void AddAccountByUserId()
        {
            var data = _users.GetUsers()[0];
            var account = _users.AddUserAccountModel();
            _mediatR.Setup(x => x.Send(It.IsAny<CreateAccountByUserIdRequestModel>(), default)).ReturnsAsync(account);
            var result = await _controller.Post(data.Id, new CreateAccountByUserIdRequestModel()
            {
                Balance = 1500,
                Currency = "USD",
            });
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, account);
        }

        [Fact]
        public async void AddAccountByUserIdWithInvalidId()
        {
            UserAccountModel? user = null;
            _mediatR.Setup(x => x.Send(It.IsAny<CreateAccountByUserIdRequestModel>(), default)).ReturnsAsync(user);
            var result = await _controller.Post(Guid.NewGuid(), new CreateAccountByUserIdRequestModel());
            Assert.NotNull(result);
            Assert.Equal(((BadRequestResult)result).StatusCode, 400);
        }
        #endregion

        #region UpdateAccountByUserId
        [Fact]
        public async void UpdateAccountByUserId()
        {
            var account = _users.GetUserAccountModel();
            var data = _users.UpdateUserAccountModel();
            _mediatR.Setup(x => x.Send(It.IsAny<UpdateAccountByUserIdRequestModel>(), default)).ReturnsAsync(data);
            var result = await _controller.Put(account.UserId, account.Id, new UpdateAccountByUserIdRequestModel()
            {
                Amount = 250,
                Action = BankAction.Deposit,
            });
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, data);
        }
        #endregion
    }
}
