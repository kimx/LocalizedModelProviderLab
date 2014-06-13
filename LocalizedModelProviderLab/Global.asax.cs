using LocalizedModelProviderLab.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace LocalizedModelProviderLab
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            this.BeginRequest += MvcApplication_BeginRequest;
        }

        void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(CurrentCultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }
        public static string CurrentCultureName
        {
            get
            {
                string cultureName = HttpContext.Current.Request.QueryString["cultureName"];
                if (string.IsNullOrEmpty(cultureName))
                    return "zh-TW";
                return cultureName;
            }
        }

        public static string CurrentCultureDisplay(string cultureName)
        {
            return System.Globalization.CultureInfo.CreateSpecificCulture(cultureName).DisplayName;
        }

        public static ResourceStringProvider GlobalResourceStringProvider { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ResourceStringProvider rp = new ResourceStringProvider(Local.ResourceManager);
            ModelMetadataProviders.Current = new LocalizedModelMetadataProvider(rp);
            GlobalResourceStringProvider = rp;

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredAttribute), typeof(LocalizedRequiredAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(StringLengthAttribute), typeof(LocalizedStringLengthAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RangeAttribute), typeof(LocalizedRangeAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(System.ComponentModel.DataAnnotations.CompareAttribute), typeof(LocalizedCompareAttributeAdapter));

        }

    }
}
