using Bussines_Logic;
using Data.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finansal_app.ViewModels
{
        public class HomeViewModel : INotifyPropertyChanged
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

            public HomeViewModel(UserService userService)
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
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = "")
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
