using SmartKitchen3.Utils;
using System.Web;
using System.Web.Mvc;

namespace SmartKitchen3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SmartKitchenAuthorizeAttribute());

        }
    }
}
