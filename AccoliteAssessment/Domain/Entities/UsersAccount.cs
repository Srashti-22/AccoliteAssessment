using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AccoliteAssessment.Domain.Entities
{
    public class UserAccountModel : Account
    {

        private Guid _id;
        [Key]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private Guid _userId;
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        [JsonIgnore]
        public UserModel User { get; set; }
    }

    public class Account
    {
        private double _balance;
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        private string _currency;
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
        private DateTime _createdAt;
        [JsonIgnore]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = DateTime.UtcNow; }
        }

        private DateTime? _updatedAt;
        [JsonIgnore]
        public DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }
    }

}
