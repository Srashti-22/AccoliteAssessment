using AccoliteAssessment.Core.Users.Create;
using AccoliteAssessment.Core.UsersAccount.UpdateAccountByUserId;
using AccoliteAssessment.Domain.Entities;

namespace AccoliteAssessment.Test
{
    public class UsersData
    {
        Guid user1 = Guid.NewGuid();
        Guid user2 = Guid.NewGuid();
        Guid acc1 = Guid.NewGuid();
        public UserAccountModel GetUserAccountModel()
        {
            return new UserAccountModel()
            {
                Balance = 200,
                Currency = "USD",
                Id = acc1,
                UserId = user1
            };
        }

        public UpdateAccountByUserIdResponseModel UpdateUserAccountModel()
        {
            return new UpdateAccountByUserIdResponseModel()
            {
                Balance = 250,
                Currency = "USD",
                Id = acc1,
                UserId = user1,
                ErrorMessage = string.Empty
            };
        }

        public UserAccountModel AddUserAccountModel()
        {
            return new UserAccountModel()
            {
                Balance = 1500,
                Currency = "USD",
                Id = Guid.NewGuid(),
                UserId = user1
            };
        }
        public List<UserModel> GetUsers()
        {
            return new List<UserModel>()
            {
                new UserModel()
                {
                    FirstName = "Test1",
                    LastName = "Test1Last",
                    Age= 1,
                    Sex = "F",
                    Email = "test@gmail.com",
                    Phone = 123456,
                    Id = user1,
                    UserAccounts = new List<UserAccountModel>(){ GetUserAccountModel()
                    }
                },
                new UserModel()
                {
                    FirstName = "Test2",
                    LastName = "Test2Last",
                    Age= 1,
                    Sex = "F",
                    Email = "test2@gmail.com",
                    Phone = 123456,
                    Id = user2,
                    UserAccounts = new List<UserAccountModel>(){ new UserAccountModel()
                    {
                        Balance = 2000,
                        Currency = "USD",
                        Id = Guid.NewGuid(),
                        UserId = user2
                    }
                    }
                }
            };
        }

        public UserModel AddUser()
        {
            Guid userId = Guid.NewGuid();
            return new UserModel()
            {
                FirstName = "Test3",
                LastName = "Test3Last",
                Age = 1,
                Sex = "F",
                Email = "test3@gmail.com",
                Phone = 123456,
                Id = userId,
                UserAccounts = new List<UserAccountModel>() { new UserAccountModel()
                {
                    Balance = 2000,
                    Currency = "USD",
                    Id = Guid.NewGuid(),
                    UserId = userId
                }
                }
            };

        }
        public AddUserRequestModel AddUserRequestModel()
        {
            Guid userId = Guid.NewGuid();
            return new AddUserRequestModel()
            {
                FirstName = "Test3",
                LastName = "Test3Last",
                Age = 1,
                Sex = "F",
                Email = "test3@gmail.com",
                Phone = 123456,
                userAccounts = new List<Account>() { new Account()
                {
                    Balance = 2000,
                    Currency = "USD",
                   
                }
                }
            };

        }
    }
}
