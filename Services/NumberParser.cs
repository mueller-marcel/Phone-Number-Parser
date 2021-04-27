using System;
using System.Text;
using PhoneNumberParser.Models;
using PhoneNumbers;

namespace PhoneNumberParser.Services
{
    public class NumberParser
    {

        /// <summary>
        /// Parsed the number submitted as string
        /// </summary>
        /// <param name="number">The number to parse</param>
        /// <returns>An instance of type <see cref="NumberData"/> containing all the information about the number</returns>
        /// <exception cref="NumberParseException">Thrown when the number to be parsed is not a phone number</exception>
        public NumberData ParsePhoneNumber(string number)
        {
            // Get an instance of the parser and declare a PhoneNumber result variable
            PhoneNumberUtil numberParser = PhoneNumberUtil.GetInstance();
            PhoneNumber parsedNumber;

            // Try to parse the variable with default region Germany
            try
            {
                parsedNumber = numberParser.Parse(number, "DE");
            }
            catch (NumberParseException)
            {
                throw;
            }

            // Check if the number is valid
            if (!numberParser.IsValidNumber(parsedNumber))
            {
                throw new ArgumentException("Invalid number");
            }

            // Gets the region code for the number such as "DE" for germany
            string regionCode = numberParser.GetRegionCodeForNumber(parsedNumber);

            // Extract the area code from the number
            string areaCode = GetAreaCode(numberParser, parsedNumber, number);

            // Extract the subscriber number
            string subscriberNumber = GetSubscriberNumber(numberParser, parsedNumber);

            // Extract the extension if available
            string extension = parsedNumber.HasExtension ? parsedNumber.Extension : string.Empty;

            // Build the clear number
            string clearNumber = GetClearNumber(number);

            // Build the iso normed number format
            string countryCode = parsedNumber.CountryCode.ToString();
            string isoNormed = GetIsoNormedNumber(countryCode, areaCode, subscriberNumber, extension);

            // Fill the NumberData object to be returned
            NumberData numberData = new NumberData
            {
                CountryCode = regionCode,
                AreaCode = areaCode,
                SubscriberNumber = subscriberNumber,
                Extension = extension,
                DigitString = clearNumber,
                ISONormedNumber = isoNormed,
            };

            return numberData;
        }

        /// <summary>
        /// Extracts the subscriber number
        /// </summary>
        /// <param name="parser">An instance of type <see cref="PhoneNumberUtil"/> to parse</param>
        /// <param name="parsedNumber">An instance of type <see cref="PhoneNumber"/> representing the parsed number</param>
        /// <returns>The subscriber number as <see cref="string"/></returns>
        private string GetSubscriberNumber(PhoneNumberUtil parser, PhoneNumber parsedNumber)
        {
            // Get the national relevant number and the length of the area code
            string nationalNumber = parser.GetNationalSignificantNumber(parsedNumber);
            int areaCodeLength = parser.GetLengthOfGeographicalAreaCode(parsedNumber);

            // Get the substring of the national relevant number from the index of the length of the area code
            string subscriberNumber = nationalNumber.Substring(areaCodeLength);

            return subscriberNumber;
        }

        /// <summary>
        /// Retrieves the area code of parsed number
        /// </summary>
        /// <param name="parser">An instance of type <see cref="PhoneNumberUtil"/> representing the parser</param>
        /// <param name="parsedNumber">An instance of type <see cref="PhoneNumber"/> representing the parsed number</param>
        /// <param name="number">The raw number as <see cref="string"/></param>
        /// <returns></returns>
        private string GetAreaCode(PhoneNumberUtil parser, PhoneNumber parsedNumber, string number)
        {
            string areaCode;

            // Get length of area code and the national relevant part of the number
            int areaCodeLength = parser.GetLengthOfGeographicalAreaCode(parsedNumber);
            string nationalNumber = parser.GetNationalSignificantNumber(parsedNumber);

            // Check if the number starts with "0"
            if (number.StartsWith("0"))
            {
                // Get the area code which starts with a "0"
                areaCode = $"0{nationalNumber.Substring(0, areaCodeLength)}";
            }
            else
            {
                // Get the area code which does not start with a "0"
                areaCode = nationalNumber.Substring(0, areaCodeLength);
            }

            return areaCode;
        }

        /// <summary>
        /// Builds an iso-normed phone number string
        /// </summary>
        /// <param name="countryCode">The country code</param>
        /// <param name="areaCode">The area code</param>
        /// <param name="subscriberNumber">The subscriber code</param>
        /// <param name="extension">The extension</param>
        /// <returns>The phone number as iso-normed <see cref="string"/></returns>
        private string GetIsoNormedNumber(string countryCode, string areaCode, string subscriberNumber, string extension)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Append country code if available
            if (!string.IsNullOrEmpty(countryCode))
            {
                stringBuilder.Append($"+{countryCode} ");
            }

            // Append area code if available
            if (!string.IsNullOrEmpty(areaCode))
            {
                // Check, whether the area code starts with 0 and remove it
                if (areaCode.StartsWith("0"))
                {
                    areaCode = areaCode.Remove(0, 1);
                }
                stringBuilder.Append($"{areaCode} ");
            }

            // Append subscriber number if available
            if (!string.IsNullOrEmpty(subscriberNumber))
            {
                stringBuilder.Append(subscriberNumber);
            }

            // Append extension if available
            if (!string.IsNullOrEmpty(extension))
            {
                stringBuilder.Append($"-{extension}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Deletes the separation chars from the number
        /// </summary>
        /// <param name="number">The number to clear from separation chars</param>
        /// <returns>The separated <see cref="string"/></returns>
        private string GetClearNumber(string number)
        {
            // Define chars to be replaced
            string[] charsToReplace = new string[]
            {
                " ",
                "(",
                ")",
                "#"
            };

            // Iterate over the chars and delete them from the number
            foreach (string item in charsToReplace)
            {
                number = number.Replace(item, string.Empty);
            }

            return number;
        }
    }
}
