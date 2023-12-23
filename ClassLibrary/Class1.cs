using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Testing
    {
        public static bool ValidateBack(bool click)
        {
            if (click == true)
            {
                return true;
            }
            return false;
        }

        public static bool ValidatePrice(string coust)
        {
            if (!int.TryParse(coust, out var result))
            {
                return false;
            }
            return true;
        }
        public static bool ValidateUser(string password, string login)
        {
            if (password == null || login == null)
            {
                return false;
            }
            else if (login == "loginDErvb2018" && password == "ceAf&R")
            {
                return true;
            }
            return false;
        }
    }
}
