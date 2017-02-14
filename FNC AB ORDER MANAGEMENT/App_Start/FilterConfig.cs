using System.Web;
using System.Web.Mvc;

namespace FNC_AB_ORDER_MANAGEMENT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
