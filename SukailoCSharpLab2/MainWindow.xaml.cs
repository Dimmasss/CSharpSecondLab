using SukailoCSharpLab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SukailoCSharpLab2.ViewModels.Exeptions;

namespace SukailoCSharpLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstName.Text.Length > 0 && LastName.Text.Length > 0 && Email.Text.Length > 0 && DatePicker.SelectedDate != null)
            {
                ProceedButton.IsEnabled = true;
            }
            else
            {
                ProceedButton.IsEnabled = false;
            }
        }

        private async void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CalculateAge() || !Person.IsValidEmail(Email.Text))
            {
                FirsNameAnswer.Content = "Name : ";
                LastNameAnswer.Content = "Surname : " ;
                EmailAddressAnswer.Content = "E-mail address: " ;
                DateOfBirthAnswer.Content = "Date of birth: " ;
                WhetherIsAdult.Text = "Is adult?: " ;
                SunSign.Text = "Sun sign: " ;
                ChineeseSign.Text = "Chinese sign: ";
                WhetherIsBirthday.Text = "Is birthday: " ;
                return;
            }
            Person persn = new Person(FirstName.Text, LastName.Text, Email.Text, DatePicker.SelectedDate.Value);
            await persn.AsynchronInit();
            FirsNameAnswer.Content = "Name : " + persn.FirstName;
            LastNameAnswer.Content = "Surname : " + persn.LastName;
            EmailAddressAnswer.Content = "E-mail address: " + persn.EmailAddress;
            DateOfBirthAnswer.Content = "Date of birth: " + persn.DateOfBirth.ToShortDateString();
            WhetherIsAdult.Text = "Is adult?: " + persn.IsAdult;
            SunSign.Text = "Sun sign: " + persn.SunSign;
            ChineeseSign.Text = "Chinese sign: " + persn.ChineseSign;
            WhetherIsBirthday.Text = "Is birthday: " + persn.IsBirthday;


        }
        private bool CalculateAge()
        {
            DateTime? dateOfBirth = DatePicker.SelectedDate;
            if (dateOfBirth != null)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - dateOfBirth.Value.Year;
                if (dateOfBirth > today.AddYears(-age)) age--;
                try
                {
                    if (age < 0)
                    {
                        throw new DateOfBirthInTheFutureException("NotBornException");
                    }
                    else if (age > 135)
                    {
                        throw new DateOfBirthTooFarInThePastException("TooOldException");
                    }
                    else
                    {
                        if (dateOfBirth.Value.Month == today.Month && dateOfBirth.Value.Day == today.Day)
                            MessageBox.Show("Happy Birthday!");
                        return true;
                    }
                } 
                catch (DateOfBirthInTheFutureException)
                { }
                catch (DateOfBirthTooFarInThePastException) 
                { }

            }
            return false;
        }
        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FirstName.Text.Length > 0 && LastName.Text.Length > 0 && Email.Text.Length > 0 && DatePicker.SelectedDate != null)
            {
                ProceedButton.IsEnabled = true;
            }
            else
            {
                ProceedButton.IsEnabled = false;
            }
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FirstName.Text.Length > 0 && LastName.Text.Length > 0 && Email.Text.Length > 0 && DatePicker.SelectedDate != null)
            {
                ProceedButton.IsEnabled = true;
            }
            else
            {
                ProceedButton.IsEnabled = false;
            }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FirstName.Text.Length > 0 && LastName.Text.Length > 0 && Email.Text.Length > 0 && DatePicker.SelectedDate != null)
            {
                ProceedButton.IsEnabled = true;
            }
            else
            {
                ProceedButton.IsEnabled = false;
            }
        }
    }
}
