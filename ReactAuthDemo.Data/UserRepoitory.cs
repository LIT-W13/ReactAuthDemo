using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAuthDemo.Data
{
    public class UserRepoitory
    {
        private readonly string _connectionString;

        public UserRepoitory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Signup(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new ReactAuthDemoDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if(user == null)
            {
                return null;
            };

            if(!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            using var context = new ReactAuthDemoDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
