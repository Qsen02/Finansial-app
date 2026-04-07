using Bussines_Logic;
using Data.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finansal_app.ViewModels
{
        public class UserViewModel : INotifyPropertyChanged
        {
            private readonly UserService _userService;

            private int userId;
            public int UserId
            {
                get => userId;
                set
                {
                    if (userId != value)
                    {
                        userId = value;
                        OnPropertyChanged();
                    }
                }
            }

            private User? _user;
            public User? User
            {
                get => _user;
                set
                {
                    if (_user != value)
                    {
                        _user = value;
                        OnPropertyChanged();
                    }
                }
            }
            private decimal? _balance;
            public decimal? Balance
            {
                get => _balance;
                set
                {
                    if (_balance != value)
                    {
                        _balance = value;
                        OnPropertyChanged();
                    }
                }
            }

        public UserViewModel(UserService userService)
            {
                _userService = userService;
            }
            public async Task LoadUserAsync()
            {
                var userIdStr = await SecureStorage.GetAsync("userId");

                if (string.IsNullOrWhiteSpace(userIdStr))
                {
                    await Shell.Current.GoToAsync("//Register");
                    return;
                }

                UserId = int.Parse(userIdStr);
                User = await _userService.GetUserById(UserId);
                Balance = User.Balance;
            }

            public async Task<User> Register(string name, string email, string password) 
            {
                return await _userService.Register(name, email, password);
            }
            public async Task<User> Login(string name,string password)
            {
                return await _userService.Login(name,password);
            }
        public event Action? BalanceUpdated;
        public async Task<User> AddTransactionToUser(string description, decimal price, CategoryType category, TypeEnum type) 
            {
                User updatedUser= await _userService.AddTransactionToUser(this.userId, description, price, category, type);
                Balance = updatedUser.Balance;
                User = updatedUser;
                BalanceUpdated.Invoke();
                return updatedUser;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
                protected void OnPropertyChanged([CallerMemberName] string name = "")
                    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
    }
