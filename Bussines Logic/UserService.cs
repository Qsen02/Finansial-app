using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic
{
    public class UserService
    {
        private readonly FinansalContext _context;
        public UserService(FinansalContext context) 
        {
            _context = context;
        }
        public async Task<User> Register(string username,string email, string password) 
        {
            User isValidUsername=await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (isValidUsername != null)
            {
                throw new Exception("User with this username already exist!");
            }
            User isValidEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (isValidEmail != null)
            {
                throw new Exception("User with this email already exist!");
            }
            User newUser=new User() 
            {
                Username = username,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
        public async Task<User> Login(string username, string password) 
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new Exception("Username or password don't match!");
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid) 
            {
                throw new Exception("Username or password don't match!");
            }
            return user;
        }
        public async Task<User> GetUserById(int userId) 
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            return user;
        }
        public async Task<User> AddTransactionToUser(int userId,Transaction transaction) 
        {
            User userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userToUpdate == null) 
            {
                throw new Exception("User not found!");
            }
            userToUpdate.Transactions.Add(transaction);
            if (transaction.Type == TypeEnum.Income)
            {
                userToUpdate.Balance += transaction.Price;
            } else 
            {
                userToUpdate.Balance -= transaction.Price;
            }
            await _context.SaveChangesAsync();
            return userToUpdate;
        }
    }
}
