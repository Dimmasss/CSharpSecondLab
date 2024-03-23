using System;
using System.Threading.Tasks;
using static SukailoCSharpLab2.ViewModels.Exeptions;
using static System.Net.Mime.MediaTypeNames;

namespace SukailoCSharpLab2.Models
{
    internal class Person
    {
        #region Fields
        private string _firstName;
        private string _lastNname; 
        private DateTime _dateOfBirth;
        private string _emailAddress;
        private  bool _isAdult;
        private  string _sunSign;
        private  string _chineseSign;
        private  bool _isBirthday;
        #endregion

        #region Properties
        // Властивості
        public string FirstName { get; private set; } 
        public string LastName { get; private set; } 
        public string EmailAddress { get; private set; } 
        public DateTime DateOfBirth { get; private set; }
        public bool IsAdult { get; private set; }
        public string SunSign { get; private set; }
        public string ChineseSign { get; private set; }
        public bool IsBirthday { get; private set; }
        #endregion

        #region Constructors
        // Конструктор, що приймає всі чотири параметри
        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            DateOfBirth = dateOfBirth;

            // Validate email address
            if (!IsValidEmail(email))
            {
                throw new InvalidEmailAddressException("Invalid email address format.");
            }
        }

        // Конструктор, що приймає ім'я, прізвище та адресу електронної пошти
        public Person(string firstName, string lastName, string emailAddress) : this(firstName, lastName, emailAddress, DateTime.MinValue)
        { }

        // Конструктор, що приймає ім'я, прізвище та дату народження
        public Person(string firstName, string lastName, DateTime dateOfBirth) : this(firstName, lastName, "", dateOfBirth)
        { }
        #endregion

        #region Calculations
        public async Task AsynchronInit()
        {
            await Task.Run(() =>
            {
                IsAdult = CalcIsAdult();
                SunSign = CalcSunSign();
                ChineseSign = CalcChineseSign();
                IsBirthday = CalcIsBirthday();
            });
        }

        private bool CalcIsAdult() {
            DateTime now = DateTime.Now;
            int userAge = now.Year - DateOfBirth.Year;

            if (now.DayOfYear < DateOfBirth.DayOfYear)
            {
                userAge -= 1;
            }

            return userAge >= 18;
        }

        private string CalcSunSign()
        {
            int month = DateOfBirth.Month;
            int day = DateOfBirth.Day;

            switch (month)
            {
                case 1:
                    return (day <= 19) ? "Capricorn" : "Aquarius";
                case 2:
                    return (day <= 18) ? "Aquarius" : "Pisces";
                case 3:
                    return (day <= 20) ? "Pisces" : "Aries";
                case 4:
                    return (day <= 20) ? "Aries" : "Taurus";
                case 5:
                    return (day <= 20) ? "Taurus" : "Gemini";
                case 6:
                    return (day <= 20) ? "Gemini" : "Cancer";
                case 7:
                    return (day <= 22) ? "Cancer" : "Leo";
                case 8:
                    return (day <= 22) ? "Leo" : "Virgo";
                case 9:
                    return (day <= 22) ? "Virgo" : "Libra";
                case 10:
                    return (day <= 22) ? "Libra" : "Scorpio";
                case 11:
                    return (day <= 21) ? "Scorpio" : "Sagittarius";
                default:
                    return (day <= 21) ? "Sagittarius" : "Capricorn";
            }
        }

        private string CalcChineseSign()
        {
            int year = DateOfBirth.Year;
            string[] chineseHoroscop = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox","Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

            return chineseHoroscop[year % 12];
        }

        private bool CalcIsBirthday() { return DateTime.Now.Month == DateOfBirth.Month && DateTime.Now.Day == DateOfBirth.Day; }

        #endregion

        #region Validation

        internal static bool IsValidEmail(string emailAddress)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains("@") || 
                    !emailAddress.Contains(".") || emailAddress.IndexOf("@") > emailAddress.LastIndexOf("."))
                    throw new InvalidEmailAddressException("WrongEmailException");
            }
            catch
            {
                return false;
            }
            return true;
        }

    #endregion
    }
}
