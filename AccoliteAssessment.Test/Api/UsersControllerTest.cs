using AccoliteAssessment.Api;
using AccoliteAssessment.Core.Users.Create;
using AccoliteAssessment.Core.Users.Delete;
using AccoliteAssessment.Core.Users.Get;
using AccoliteAssessment.Core.Users.GetById;
using AccoliteAssessment.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AccoliteAssessment.Test.Api
{
    public class UsersControllerTest
    {
        private readonly Mock<IMediator> _mediatR;
        private readonly UsersController _controller;
        private readonly UsersData _users;
        public UsersControllerTest()
        {
            _mediatR = new Mock<IMediator>();
            _controller = new UsersController(_mediatR.Object);
            _users = new UsersData();
        }

        #region Get
        [Fact]
        public async void GetAllUsers()
        {
            var data = _users.GetUsers();
            _mediatR.Setup(x => x.Send(It.IsAny<GetUserRequestModel>(), default)).ReturnsAsync(data);
            var result = await _controller.Get();
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, data);
        }

        [Fact]
        public async void GetWithNoUsersExists()
        {
            _mediatR.Setup(x => x.Send(It.IsAny<GetUserRequestModel>(), default)).ReturnsAsync(new List<UserModel>());
            var result = await _controller.Get();
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 404);
            Assert.Equal(((ObjectResult)result).Value, "No User exists");
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById()
        {
            var data = _users.GetUsers()[0];
            _mediatR.Setup(x => x.Send(It.IsAny<GetUserByIdRequestModel>(), default)).ReturnsAsync(data);
            var result = await _controller.Get(data.Id);
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, data);
        }

        [Fact]
        public async void GetByIdNotExists()
        {
            UserModel? user = null;
            _mediatR.Setup(x => x.Send(It.IsAny<GetUserByIdRequestModel>(), default)).ReturnsAsync(user);
            var result = await _controller.Get(Guid.NewGuid());
            Assert.Equal(((ObjectResult)result).StatusCode, 404);
            Assert.Equal(((ObjectResult)result).Value, "User Not Found");
        }
        #endregion

        #region Delete
        [Fact]
        public async void Delete()
        {
            var data = _users.GetUsers()[0];
            _mediatR.Setup(x => x.Send(It.IsAny<DeleteUserRequestModel>(), default)).ReturnsAsync("Successfully Deleted");
            var result = await _controller.Delete(data.Id);
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, "Successfully Deleted");
        }
        [Fact]
        public async void DeleteWithNotExistsId()
        {
            _mediatR.Setup(x => x.Send(It.IsAny<DeleteUserRequestModel>(), default)).ReturnsAsync(string.Empty);
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 404);
            Assert.Equal(((ObjectResult)result).Value, "User Not Found");
        }
        #endregion

        #region Post
        [Fact]
        public async void Post()
        {
            var data = _users.AddUser();
            _mediatR.Setup(x => x.Send(It.IsAny<AddUserRequestModel>(), default)).ReturnsAsync(data);
            var result = await _controller.Post(_users.AddUserRequestModel());
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 200);
            Assert.Equal(((ObjectResult)result).Value, data);
        }

        [Fact]
        public async void PostWithLessBalance()
        {
            UserModel? user = null;
            var data = _users.AddUserRequestModel();
            data.userAccounts[0].Balance = 50;
            _mediatR.Setup(x => x.Send(It.IsAny<AddUserRequestModel>(), default)).ReturnsAsync(user);
            var result = await _controller.Post(data);
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 400);
            Assert.Equal(((ObjectResult)result).Value, "Invalid data");
        }

        [Fact]
        public async void PostWithNullValue()
        {
            UserModel? user = null;
            _mediatR.Setup(x => x.Send(It.IsAny<AddUserRequestModel>(), default)).ReturnsAsync(user);
            var result = await _controller.Post(null);
            Assert.NotNull(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 400);
            Assert.Equal(((ObjectResult)result).Value, "Invalid data");
        }
        #endregion
    }
}
