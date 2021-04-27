namespace PhoneNumberParser.Models
{
    public class NumberData
    {
        /// <summary>
        /// Holds the region code for the parsed number (e.g. "DE" for Germany)
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Holds the area code for the parsed number
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// Holds the subscriber number for the parsed number
        /// </summary>
        public string SubscriberNumber { get; set; }

        /// <summary>
        /// Holds the extension for the parsed number
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Holds the number without the whitespaces and the braces or hyphens
        /// </summary>
        public string DigitString { get; set; }

        /// <summary>
        /// The number in ISO-Format
        /// </summary>
        public string ISONormedNumber { get; set; }
    }
}
