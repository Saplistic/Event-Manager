using Administration;
using EventManager.Data;
using EventManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EventManager.Models.RequestModels;
using Microsoft.IdentityModel.Tokens;

namespace EventManager.Services
{
    internal class UserManager
    {

        public bool Create(UserRequest userRequest)
        {
            using (var context = new MyDBContext())
            {
                if (context.Users.Any(user => user.EmailAddress == userRequest.EmailAddress))
                {
                    MessageBox.Show("Email address already in use");
                    return false;
                }

                if (!Validate(userRequest))
                {
                    return false;
                }

                User newUser = new User()
                {
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    EmailAddress = userRequest.EmailAddress,
                    Password = userRequest.Password,
                    PhoneNumber = userRequest.PhoneNumber
                };

                context.Users.Add(newUser);
                context.SaveChanges();
            }
            return true;
        } 

        public bool Update(UserRequest userRequest, int userId)
        {
            if (UserService.Instance.User == null || userId != UserService.Instance.User.Id)
            {
                MessageBox.Show("You have insufficient rights to perform this action");
                return false;
            }

            using (var context = new MyDBContext())
            {
                User selectedUser = context.Users.Find(userId);

                if (selectedUser == null)
                {
                    MessageBox.Show("User to update not found");
                    return false;
                }

                if (string.IsNullOrEmpty(userRequest.Password) && string.IsNullOrEmpty(userRequest.ConfirmPassword)) // Laat de wachtwoorden ongewijzigd als ze leeg zijn
                {
                    userRequest.Password = selectedUser.Password;
                    userRequest.ConfirmPassword = selectedUser.Password;
                    MessageBox.Show("Password unchanged");
                }

                if (!Validate(userRequest))
                {
                    return false;
                }
                    
                {
                    selectedUser.FirstName = userRequest.FirstName;
                    selectedUser.LastName = userRequest.LastName;
                    selectedUser.EmailAddress = userRequest.EmailAddress;
                    selectedUser.Password = userRequest.Password;
                    selectedUser.PhoneNumber = userRequest.PhoneNumber;
                }

                context.Users.Update(selectedUser);
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int userId)
        {
            if (UserService.Instance.User == null || userId != UserService.Instance.User.Id)
            {
                MessageBox.Show("You have insufficient rights to perform this action");
                return false;
            }

            using (var context = new MyDBContext())
            {
                User selectedUser = context.Users.Find(userId);

                if (selectedUser == null)
                {
                    MessageBox.Show("User to delete not found");
                    return false;
                }

                context.Users.Remove(selectedUser);
                context.SaveChanges();
            }
            return true;
        }

        public bool Validate(UserRequest userRequest)
        {
            if (string.IsNullOrEmpty(userRequest.FirstName))
            {
                MessageBox.Show("Please enter your first name");
                return false;
            }
            else if (userRequest.FirstName.Count() < 2 || userRequest.FirstName.Count() > 40)
            {
                MessageBox.Show("First name must contain between 2 to 40 characters");
                return false;
            }

            else if (string.IsNullOrEmpty(userRequest.LastName))
            {
                MessageBox.Show("Please enter your last name");
                return false;
            }
            else if (userRequest.LastName.Count() < 2 || userRequest.LastName.Count() > 40)
            {
                MessageBox.Show("Last name may only contain between 2 to 40 characters");
                return false;
            }

            else if (string.IsNullOrEmpty(userRequest.EmailAddress))
            {
                MessageBox.Show("Please enter your email address");
                return false;
            }

            else if (userRequest.EmailAddress.Count() < 6 || userRequest.EmailAddress.Count() > 50)
            {
                MessageBox.Show("Email address may only contain between 6 to 50 characters");
                return false;
            }

            else if (string.IsNullOrEmpty(userRequest.Password))
            {
                MessageBox.Show("Please enter your password");
                return false;
            }
            else if (userRequest.Password.Count() < 5 || userRequest.Password.Count() > 40)
            {
                MessageBox.Show("Password may only contain between 5 to 40 characters");
                return false;
            }

            else if (string.IsNullOrEmpty(userRequest.ConfirmPassword))
            {
                MessageBox.Show("Please confirm your password");
                return false;
            }
            else if (!string.Equals(userRequest.Password, userRequest.ConfirmPassword, StringComparison.Ordinal))
            {
                MessageBox.Show("Passwords do not match");
                return false;
            }

            else if (!string.IsNullOrEmpty(userRequest.PhoneNumber) && userRequest.PhoneNumber.Count() > 16)
            {
                return false;
            }
            return true;
        }

    }
}
