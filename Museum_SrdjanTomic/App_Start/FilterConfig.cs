using System.Web;
using System.Web.Mvc;

namespace Museum_SrdjanTomic
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
