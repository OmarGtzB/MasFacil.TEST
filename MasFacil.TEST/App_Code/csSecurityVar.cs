using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csSecurityVar
/// </summary>
namespace MGMSecurityVar
{

    public enum LoginResult
    {
        Succeed = 1,
        PasswordError = 2,
        UsrNoExist = 3,
        OtherError = 4
    };

    public class Session
    {
        public const string LoginInfo = "LoginInfo";
       
    }

    public class SessionUser {
        public const string maUsuSess = "maUsuSess";
    }

}