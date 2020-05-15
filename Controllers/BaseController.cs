using Microsoft.AspNetCore.Mvc;

namespace VMS.Web.Controllers
{
    public class BaseController : Controller
    {
        //Set up alerts of differnt types to display messages to the user with different color backgrounds
        //based on type of alert
        public enum AlertType {success,danger,warning,info }
        
        public void Alert(string message,AlertType type = AlertType.info)
        {
            TempData["Alert.Message"] = message;
            TempData["Alert.Type"] = type.ToString();
            
        }
    }
}