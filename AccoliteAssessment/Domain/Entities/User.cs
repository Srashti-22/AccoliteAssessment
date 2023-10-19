using System.ComponentModel.DataAnnotations;

namespace AccoliteAssessment.Domain.Entities
{
    public class UserModel : User
    {
        private Guid _id;
        [Key]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public virtual ICollection<UserAccountModel> UserAccounts { get; set; }
    }
    public class User
    {
        private string _firstName;
        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _lastName;
        [Required]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _email;
        [Required]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _sex;
        [Required]
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        private int _age;
        [Required]
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        private int _phone;
        [Required]
        public int Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
    }
}
