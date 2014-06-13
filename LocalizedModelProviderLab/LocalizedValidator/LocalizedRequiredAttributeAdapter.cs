using LocalizedModelProviderLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalizedModelProviderLab
{
    public class LocalizedRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public LocalizedRequiredAttributeAdapter( ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute) { }

        //http://thatextramile.be/blog/2011/04/customizing-asp-net-mvcs-required-property-validation-messages/
        //http://social.msdn.microsoft.com/Forums/en-US/77514514-8375-480a-a905-e636eed20064/aspnet-mvc-validate-?forum=236
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = null;
            if (this.Attribute.ErrorMessage == null && (this.Attribute.ErrorMessageResourceName == null || this.Attribute.ErrorMessageResourceType == null))
                errorMessage = MvcApplication.GlobalResourceStringProvider.GetModelString(Metadata.ContainerType, Metadata.PropertyName, "Required");

            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = ErrorMessage; //取不到則還原成預設值
            return new[] { new ModelClientValidationRequiredRule(errorMessage) };
        }
    }

}