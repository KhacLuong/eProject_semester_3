using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;

namespace ShradhaBook_API.Helpers;

public class Helpers
{
    private readonly IMapper _mapper;

    public Helpers(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
    }

    public static string convertToSlug(string str)
    {
        var slug = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(str));

        return slug;
    }

    public static string RemoveAccents(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        text = text.Normalize(NormalizationForm.FormD);
        var chars = text
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                        != UnicodeCategory.NonSpacingMark).ToArray();

        return new string(chars).Normalize(NormalizationForm.FormC);
    }

    public static string Slugify(string phrase)
    {
        // Remove all accents and make the string lower case.  
        var output = RemoveAccents(phrase).ToLower();

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
            var m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool IsValidPhone(string phone)
    {
        var regex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        return Regex.IsMatch(phone, regex);
    }

    public static bool IsValidCode(string code)
    {
        var regex = @"^[a-zA-Z0-9]+$";
        return Regex.IsMatch(code, regex);
    }
}
//    public class Recucive
//    {
//        public List<Category> result;
//        public string text;
//        public int index;

//        public Recucive()
//        {
//            this.result = null;
//            this.text = "";
//            index=0;
//        }

//        public  List<Category> categoryRecusive(List<Category> categories, int? id = 0)
//        {
//            if (result == null || result.Count == 0)
//            {
//                for(int i = 0; i < categories.Count; i++)
//                {
//                    if (categories[i].Id == id)
//                    {
//                        result.Add(categories[i]);
//                        break;
//                    }
//                }
//                this.categoryRecusive(result);
//            }
//            else
//            {

//                int i = index;
//                for(i = 0; i < result.Count; i++)
//                {
//                   bool  flag = false;
//                    for(int j = 0; j < categories.Count; j++)
//                    {
//                        if (categories[j].Id == result[i].Id)
//                        {
//                            result.Add(categories[i]);
//                            categories.RemoveAt(i);
//                            index++;
//                            flag = true;
//                            break;
//                        }
//                    }
//                    if (flag)
//                    {
//                        index--;
//                        break;
//                    }

//                }
//                categoryRecusive(result);

//            }

//        }
//    //}

//}