using System.Web;
using System.Web.Mvc;
using Inventory.CustomFilter;

namespace Inventory
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomFilters());
        }
    }
}
