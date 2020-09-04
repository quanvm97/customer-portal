using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Constants
{
    public class SdkKeyConstants
    {
        #region AppCenter

        public static string AppCenterAndroid = "--";
        public static string AppCenteriOS = "--";

        #endregion        

        #region Regex emal

        public const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        #endregion

    }
}
