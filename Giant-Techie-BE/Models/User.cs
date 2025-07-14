using System.Net.Mail;

namespace Giant_Techie_BE.Models
{
    public sealed class User : EntityBase
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string CollegeName { get; private set; }
        public string Password { get; private set; }

        public User(string fullName, string email, string collegeName, string password)
        {

            FullName = fullName;
            Email = email;
            CollegeName = collegeName;
            Password = password;
        }


        public static User Create(string fullName, string email, string collegeName, string password)
        {
            ValidateInputs(fullName, email, collegeName, password);
            return new User(fullName, email, collegeName, password);
        }

        public void Update(string fullName, string email, string collegeName, string password)
        {
            ValidateInputs(fullName, email, collegeName, password);

            FullName = fullName;
            Email = email;
            CollegeName = collegeName;
            Password = password;

            UpdateLastModified();
        }
        private static void ValidateInputs(string fullName, string email, string collegeName, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Name cannot be null or empty.", nameof(fullName));
            
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            try
            {
                var addr = new MailAddress(email);
                if (addr.Address != email)
                    throw new ArgumentException("Invalid email format.", nameof(email));
            }
            catch
            {
                throw new ArgumentException("Invalid email format.", nameof(email));
            }


            if (string.IsNullOrWhiteSpace(collegeName))
                throw new ArgumentException("CollegeName is Required", nameof(collegeName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is Required", nameof(password));

            if (password.Length < 5)
                throw new ArgumentException("Password Lenght should be greater than or equal 5", nameof(password));

        }


    }
}
