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
            User? isValidUsername=await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (isValidUsername != null)
            {
                throw new Exception("User with this username already exist!");
            }
            User? isValidEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
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
            User? user = await _context.Users.Include(el=>el.Transactions).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            return user;
        }
        public async Task<User> AddTransactionToUser(int userId, string description, int price, CategoryType category, TypeEnum type) 
        {
            Transaction newTrasaction = new Transaction()
            {
                Description = description,
                Price = price,
                Category = category,
                Type = type
            };
            _context.Transactions.Add(newTrasaction);
            User? userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userToUpdate == null) 
            {
                throw new Exception("User not found!");
            }
            newTrasaction.UserId = userId;
            newTrasaction.User = userToUpdate;
            userToUpdate.Transactions.Add(newTrasaction);
            if (newTrasaction.Type == TypeEnum.Income)
            {
                userToUpdate.Balance += newTrasaction.Price;
            } else 
            {
                userToUpdate.Balance -= newTrasaction.Price;
            }
            await _context.SaveChangesAsync();
            return userToUpdate;
        }
    }
}
