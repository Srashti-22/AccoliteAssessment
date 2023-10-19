using AccoliteAssessment.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AccoliteAssessment.Core.UsersAccount.UpdateAccountByUserId
{
    public class UpdateAccountByUserIdRequestModel : IRequest<UpdateAccountByUserIdResponseModel>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid AccountId { get; set; }
        
        public double Amount { get; set; }
        [Required]
        public string Action { get; set; }
    }

    public class UpdateAccountByUserIdResponseModel : UserAccountModel
    {
        public string? ErrorMessage { get; set; }
    }
    public class UpdateAccountByUserIdHandler : IRequestHandler<UpdateAccountByUserIdRequestModel, UpdateAccountByUserIdResponseModel>
    {
        private readonly IUOW _unitOfWork;
        public UpdateAccountByUserIdHandler(IUOW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateAccountByUserIdResponseModel> Handle(UpdateAccountByUserIdRequestModel request, CancellationToken cancellationToken)
        {
            if(request != null)
            {
                var userAccount =await _unitOfWork.userAccountRespository.GetAccountByUserId(request.UserId, request.AccountId);
                if(userAccount != null)
                {
                    if(!(request.Action == BankAction.Deposit || request.Action == BankAction.Withdrawl))
                    {
                        return new UpdateAccountByUserIdResponseModel() { ErrorMessage = "user can either Deposit or Withdraw amount from an account" };
                    }
                    if(request.Action == BankAction.Deposit && request.Amount > 10000)
                    {
                        return new UpdateAccountByUserIdResponseModel() { ErrorMessage = "user cannot deposit more than $10,000 in a single transaction" };
                    }
                    if(request.Action == BankAction.Withdrawl && request.Amount > userAccount.Balance * 0.90)
                    {
                        return new UpdateAccountByUserIdResponseModel() { ErrorMessage = "user cannot withdraw more than 90% of their total balance from an account in a single transaction" };
                    }

                    double leftBalance = userAccount.Balance - request.Amount;
                    if(request.Action == BankAction.Withdrawl && leftBalance < 100)
                    {
                        return new UpdateAccountByUserIdResponseModel() { ErrorMessage = "account cannot have less than $100 at any time" };
                    }
                    userAccount.Balance = request.Action == BankAction.Deposit ? userAccount.Balance + request.Amount : leftBalance;
                    userAccount.UpdatedAt = DateTime.UtcNow;

                    var result = await _unitOfWork.userAccountRespository.UpdateAccountByUserId(userAccount);
                    return new UpdateAccountByUserIdResponseModel()
                    {
                        Balance = result.Balance,
                        Currency = result.Currency,
                        Id = result.Id,
                        UserId = result.UserId,
                        ErrorMessage = string.Empty
                    };
                }
            }
            throw new NotImplementedException();
        }
    }
}
