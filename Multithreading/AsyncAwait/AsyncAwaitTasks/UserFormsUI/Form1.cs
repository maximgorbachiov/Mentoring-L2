using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncCRUDLibrary;
using AsyncCRUDLibrary.Interfaces;

namespace UserFormsUI
{
    public partial class Form1 : Form
    {
        private IAsyncRepository asyncRepository = new AsyncRepository();

        public Form1()
        {
            InitializeComponent();
        }

        private async void FindUsersButton_Click(object sender, EventArgs e)
        {
            IEnumerable<User> users;

            string name = NameField.Text;
            if (!string.IsNullOrEmpty(name))
            {
                users = await this.asyncRepository.ReadUserBySelectorAsync((user) => user.Name == name);

                if (users == null)
                {
                    MessageBox.Show($"No such user with name {name} was founded");
                }
                else
                {
                    FoundedUsersList.DataSource = users.ToList();
                }
                return;
            }

            string surname = SurnameField.Text;
            if (!string.IsNullOrEmpty(surname))
            {
                users = await this.asyncRepository.ReadUserBySelectorAsync((user) => user.Surname == surname);

                if (users == null)
                {
                    MessageBox.Show($"No such user with surname {surname} was founded");
                }
                else
                {
                    FoundedUsersList.DataSource = users.ToList();
                }
                return;
            }

            int age, toAge;
            string ageText = AgeField.Text;
            string toAgeText = ToAgeField.Text;
            if (!string.IsNullOrEmpty(ageText))
            {
                if (int.TryParse(ageText, out age) && age >= 0)
                {
                    if (!string.IsNullOrEmpty(toAgeText))
                    {
                        if (int.TryParse(toAgeText, out toAge) && toAge > age)
                        {
                            users = await this.asyncRepository.ReadUserBySelectorAsync((user) => user.Age >= age && user.Age <= toAge);

                            if (users == null)
                            {
                                MessageBox.Show($"No such user with age between {age} and {toAge} was founded");
                            }
                            else
                            {
                                FoundedUsersList.DataSource = users.ToList();
                            }
                            return;
                        }
                    }
                    else
                    {
                        users = await this.asyncRepository.ReadUserBySelectorAsync((user) => user.Age == age);

                        if (users == null)
                        {
                            MessageBox.Show($"No such user with age {age} was founded");
                        }
                        else
                        {
                            FoundedUsersList.DataSource = users.ToList();
                        }
                        return;
                    }
                }
            }
            
        }

        private async void CreateUserButton_Click(object sender, EventArgs e)
        {
            User user = GetUser(NameField, SurnameField, AgeField);

            if (user == null)
            {
                return;
            }

            bool result = await this.asyncRepository.CreateUserAsync(user);
            if (result)
            {
                CreatedUserLabel.Text = $"User {user} was added successfully";
            }
            else
            {
                CreatedUserLabel.Text = $"User {user} was also added before";
            }
        }

        private async void DeleteUserButton_Click(object sender, EventArgs e)
        {
            User userToDelete = GetUser(NameField, SurnameField, AgeField);

            if (userToDelete == null)
            {
                return;
            }

            bool result = await this.asyncRepository.DeleteUserAsync((user) =>
            {
                return user.Name == userToDelete.Name
                    && user.Surname == userToDelete.Surname
                    && user.Age == userToDelete.Age;
            });

            if (result)
            {
                DeletedUserLabel.Text = $"User {userToDelete} was deleted successfully";
            }
            else
            {
                DeletedUserLabel.Text = $"User {userToDelete} was not deleted";
            }
        }

        private async void UpdateUserButton_Click(object sender, EventArgs e)
        {
            User userToUpdate = GetUser(NameField, SurnameField, AgeField);
            User newUser = GetUser(UpdatedUserNameField, UpdatedUserSurnameField, UpdatedUserAgeField);

            bool result = await this.asyncRepository.UpdateUserAsync((user) =>
            {
                return user.Name == userToUpdate.Name
                    && user.Surname == userToUpdate.Surname
                    && user.Age == userToUpdate.Age;
            }, newUser);

            if (result)
            {
                UpdatedUserLabel.Text = $"User {userToUpdate} was successfully updated to the {newUser}";
            }
            else
            {
                UpdatedUserLabel.Text = $"User {userToUpdate} was not updated to the {newUser}";
            }
        }

        private User GetUser(TextBox nameTextBox, TextBox surnameTextBox, TextBox ageTextBox)
        {
            string name = nameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(nameof(name));
                return null;
            }

            string surname = surnameTextBox.Text;
            if (string.IsNullOrEmpty(surname))
            {
                MessageBox.Show(nameof(surname));
                return null;
            }

            int age;
            string ageText = ageTextBox.Text;
            if (string.IsNullOrEmpty(ageText))
            {
                MessageBox.Show(nameof(ageText));
                return null;
            }
            if (!int.TryParse(ageText, out age) || age < 0)
            {
                MessageBox.Show($"Value {ageText} is not valid for user's age");
                return null;
            }

            return new User { Name = name, Surname = surname, Age = age };
        }
    }
}
