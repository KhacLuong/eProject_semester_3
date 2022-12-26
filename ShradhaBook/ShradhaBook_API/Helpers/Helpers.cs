﻿using AutoMapper;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ShradhaBook_API.Helpers
{
    public class Helpers
    {
        private readonly IMapper _mapper;
        public Helpers(DataContext context, IMapper mapper)
        {
        
            this._mapper = mapper;
        }

        public static string convertToSlug(string str)
        {
            var slug = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(str));
           
            return slug;
        }
        public static string RemoveAccents( string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            char[] chars = text
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }
        public static string Slugify( string phrase)
        {
            // Remove all accents and make the string lower case.  
            string output = RemoveAccents(phrase).ToLower();

            // Remove all special characters from the string.  
            output = Regex.Replace(output, @"[^A-Za-z0-9\s-]", "");

            // Remove all additional spaces in favour of just one.  
            output = Regex.Replace(output, @"\s+", " ").Trim();

            // Replace all spaces with the hyphen.  
            output = Regex.Replace(output, @"\s", "-");

            // Return the slug.  
            return output;
        }

        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidPhone(string phone)
        {
            string regex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            return Regex.IsMatch(phone, regex);
        }

        public static bool IsValidCode(string code)
        {
            string regex = @"^[a-zA-Z0-9]+$";
            return Regex.IsMatch(code, regex);
        }

    }
}
