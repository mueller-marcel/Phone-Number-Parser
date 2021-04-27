using System;
using System.Windows;
using PhoneNumberParser.Helpers;
using PhoneNumberParser.Models;
using PhoneNumberParser.Services;

namespace PhoneNumberParser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Field and property to hold the date for the input field
        /// </summary>
        private string _inputField;

        public string InputField
        {
            get { return _inputField; }
            set
            {
                _inputField = value;
                OnPropertyChanged(nameof(InputField));
            }
        }

        /// <summary>
        /// Field and property to hold the data for the country code field
        /// </summary>
        private string _countryCode;

        public string CountryCode
        {
            get { return _countryCode; }
            set
            {
                _countryCode = value;
                OnPropertyChanged(nameof(CountryCode));
            }
        }

        /// <summary>
        /// Field and property to hold the data for the area code field
        /// </summary>
        private string _areaCode;

        public string AreaCode
        {
            get { return _areaCode; }
            set
            {
                _areaCode = value;
                OnPropertyChanged(nameof(AreaCode));
            }
        }

        /// <summary>
        /// Field and property to hold the data for the number field
        /// </summary>
        private string _subscriberNumber;

        public string SubscriberNumber
        {
            get { return _subscriberNumber; }
            set
            {
                _subscriberNumber = value;
                OnPropertyChanged(nameof(SubscriberNumber));
            }
        }

        /// <summary>
        /// Field and property to hold the data for the extension field
        /// </summary>
        private string _extension;

        public string Extension
        {
            get { return _extension; }
            set
            {
                _extension = value;
                OnPropertyChanged(nameof(Extension));
            }
        }

        /// <summary>
        /// Holds the given number without braces, whitespaces or hyphens
        /// </summary>
        private string _digitString;

        public string DigitString
        {
            get { return _digitString; }
            set
            {
                _digitString = value;
                OnPropertyChanged(nameof(DigitString));
            }
        }

        /// <summary>
        /// Holds the number in the iso-format
        /// </summary>
        private string _isoNumber;

        public string ISONumber
        {
            get { return _isoNumber; }
            set
            {
                _isoNumber = value;
                OnPropertyChanged(nameof(ISONumber));
            }
        }


        /// <summary>
        /// Holds the handlers for the parse button
        /// </summary>
        public RelayCommand ParseCommand { get; set; }

        /// <summary>
        /// Holds the handlers for the reset button
        /// </summary>
        public RelayCommand ResetCommand { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Set up the command properties
        /// </summary>
        public MainWindowViewModel()
        {
            ParseCommand = new RelayCommand(ExecuteParse, CanExecuteParse);
            ResetCommand = new RelayCommand(ExecuteReset, CanExecuteReset);
        }
        #endregion

        /// <summary>
        /// Checks if the parse button is enabled
        /// </summary>
        /// <param name="parameter">Parameter for some data</param>
        /// <returns>True if the input field is filled, otherwise false</returns>
        public bool CanExecuteParse(object parameter) => string.IsNullOrEmpty(InputField) ? false : true;

        /// <summary>
        /// Executed when the parse button is clicked
        /// </summary>
        /// <param name="parameter">Parameter for some data</param>
        public void ExecuteParse(object parameter)
        {
            // Parse number
            NumberParser parser = new NumberParser();
            NumberData parsedNumber = new NumberData();
            try
            {
                parsedNumber = parser.ParsePhoneNumber(InputField);
            }
            catch (Exception)
            {
                MessageBox.Show("Fehler beim Parsen");
                ResetTextboxes();
            }

            // Fill textboxes
            CountryCode = parsedNumber.CountryCode;
            AreaCode = parsedNumber.AreaCode;
            SubscriberNumber = parsedNumber.SubscriberNumber;
            Extension = parsedNumber.Extension;
            DigitString = parsedNumber.DigitString;
            ISONumber = parsedNumber.ISONormedNumber;
        }

        /// <summary>
        /// Checks if the reset button is enabled
        /// </summary>
        /// <param name="parameter">Parameter for some data</param>
        /// <returns>True</returns>
        public bool CanExecuteReset(object parameter) => true;

        /// <summary>
        /// Executed when the reset button is clicked
        /// </summary>
        /// <param name="parameter">parameter for some data</param>
        public void ExecuteReset(object parameter)
        {
            ResetTextboxes();
        }

        /// <summary>
        /// Clears all textboxes in the main view
        /// </summary>
        public void ResetTextboxes()
        {
            InputField = string.Empty;
            CountryCode = string.Empty;
            AreaCode = string.Empty;
            SubscriberNumber = string.Empty;
            Extension = string.Empty;
            DigitString = string.Empty;
            ISONumber = string.Empty;
        }
    }
}
