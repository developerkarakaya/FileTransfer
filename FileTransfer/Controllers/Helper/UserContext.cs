using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using DataEntites.FileTransfer;

    public class UserContext
    {
        public UserContext()
        {
            if (UserContext.Current!= null && UserContext.Current.User!=null)
            {
                var detail = UserContext.Current.User;
                HttpContext.Current.Session["UserSessionText"] = new UserContext(detail);
            }
        }

        public UserContext(Manager _User)
        { 
            this.User = _User;
        }
        public static UserContext Current
        {
            get
            {
                if (HttpContext.Current.Session == null)
                {
                    return null;
                }
                if (IsAvailable())
                    return (UserContext)HttpContext.Current.Session["UserSessionText"];
              
                FormsAuthentication.SignOut();

                return null;
            }
        }

        public Manager User { get; set; }
        public static bool IsAvailable()
        { 
            return HttpContext.Current.Session["UserSessionText"] != null;
        }


        public string MainMenu { get; set; }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session["UserSessionText"] = null;
            HttpContext.Current.Session.Abandon();
        }
    }
  
 

