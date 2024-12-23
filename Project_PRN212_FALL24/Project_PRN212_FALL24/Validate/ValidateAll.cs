using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_PRN212_FALL24.Validate
{
    internal class ValidateAll
    {
        // Kiểm tra nếu email không khớp với mẫu regex
        public bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-z.A-Z]{2,}$");
        }
        public bool ValidatePassWord(string input)
        {
            bool check = false;
            if (input.Length > 6)
            {
                check = true;
            }
            return check;
        }
    }
}
