using LocalizedModelProviderLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalizedModelProviderLab
{
    public class LocalizedStringLengthAttributeAdapter : StringLengthAttributeAdapter
    {


        public LocalizedStringLengthAttributeAdapter(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute)
            : base(metadata, context, attribute) { }


        //http://thatextramile.be/blog/2011/04/customizing-asp-net-mvcs-required-property-validation-messages/
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = null;
            if (this.Attribute.ErrorMessage == null && (this.Attribute.ErrorMessageResourceName == null || this.Attribute.ErrorMessageResourceType == null))
                errorMessage = MvcApplication.GlobalResourceStringProvider.GetModelString(Metadata.ContainerType, Metadata.PropertyName, "StringLength");
           
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = ErrorMessage; // fallback to what ASP.NET MVC would normally display

            return new[] { new ModelClientValidationStringLengthRule(errorMessage, this.Attribute.MinimumLength, this.Attribute.MaximumLength) };
        }
    }

}