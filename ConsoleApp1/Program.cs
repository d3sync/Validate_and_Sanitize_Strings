using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("alex@idle.gr");
            list.Add("cosmote@otenet.gr");
            list.Add("kjh32423j4kh23kj4h23jdsf2323SDFADSF234");
            list.Add("kjh3$$$%24%23j4kh@23kj4h23jdsf23#23SDF!ADSF234");
            list.Add("kjh3$$$%24%23j4kh@2\r3kj4h23jds\r\nf23#23SD\0F!ADSF234");
            //list.Add("12345574567456435");
            //list.Add("6942907212");
            //list.Add("2106582121");
            //list.Add("+302106582121");
            //list.Add("+302101231234");
            //list.Add("+302241034443");
            //list.Add("+302810346587");
            list.Add("Hello\r\nWorld\r\n!!!");

            foreach (string item in list)
            {
                Console.WriteLine("============================");

                Console.Write(item + "\r\n");
                Console.WriteLine("============================");
                Console.Write("\nAlphanumeric:");
                Console.Write(SanitizeString(item, SanitizeBy.ALPHANUMERIC));
                //Console.Write("\nAlphanumeric with max 10 chars:");
                //Console.Write(SanitizeString(item, SanitizeBy.ALPHANUMERIC, maxLength: 10));
                //Console.Write("\nNumeric:");
                //Console.Write(SanitizeString(item, SanitizeBy.NUMERIC));
                //Console.Write("\nEmail:");
                //Console.Write(SanitizeString(item, SanitizeBy.EMAIL));
                //Console.Write("\nPhone Number:");
                //Console.Write(SanitizeString(item, @"(?:[^0-9]+|(^\+(30)|(30)|)|[^69|^2])"));
                //Console.Write("\nESCAPED CHARS:");
                //Console.Write(SanitizeString(item, SanitizeBy.ESCAPED));
                Console.WriteLine();

                Console.WriteLine("{0} - {1} {2}",item,nameof(Validate.ALPHANUMERIC),ValidateInput(item,Validate.ALPHANUMERIC));
                Console.WriteLine("{0} - {1} {2}", item, nameof(Validate.EMAIL), ValidateInput(item, Validate.EMAIL));
                Console.WriteLine("{0} - {1} {2}", item, nameof(Validate.NUMERIC), ValidateInput(item, Validate.NUMERIC));
                Console.WriteLine("{0} - {1} {2}", item, nameof(Validate.PHONE), ValidateInput(item, Validate.PHONE));
            }
            Console.ReadLine();
        }
        public static class SanitizeBy
        {
            public const string ALPHANUMERIC = @"[^a-zA-Z0-9\s]+";
            public const string NUMERIC = @"[^0-9]+";
            public const string EMAIL = @"[^a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            public const string PHONE = @"(?:[^0-9]+|(^\+(30)|(30)|)|[^69|^2])";
            public const string ESCAPED = @"[\n\r\t\f\v\0]";
        }
        public static class Validate
        {
            public const string ALPHANUMERIC = @"^[a-zA-Z0-9\s]+$";
            public const string NUMERIC = @"^[0-9]+$";
            public const string EMAIL = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            public const string PHONE = @"^(?:\+30\s(69\d{8}|2\d{8})|(69|2)\d{8}|(?:\d{10}))$";
        }
        public static bool ValidateInput(string input, string regexPattern)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            Regex regex = new Regex(regexPattern);
            return regex.IsMatch(input);
        }
        public static string SanitizeString(string input, string regexPattern, int? minLength = null, int? maxLength = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            Regex regex = new Regex(regexPattern);
            string sanitizedInput = regex.Replace(input, "");

            if (minLength.HasValue && sanitizedInput.Length < minLength.Value)
            {
                sanitizedInput = sanitizedInput.PadRight(minLength.Value, ' ');
            }

            if (maxLength.HasValue && sanitizedInput.Length > maxLength.Value)
            {
                sanitizedInput = sanitizedInput.Substring(0, maxLength.Value);
            }
            return sanitizedInput;
        }
    }
}
