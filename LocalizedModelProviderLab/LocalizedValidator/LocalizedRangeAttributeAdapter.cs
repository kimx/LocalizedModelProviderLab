using LocalizedModelProviderLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalizedModelProviderLab
{
    public class LocalizedRangeAttributeAdapter : RangeAttributeAdapter
    {


        public LocalizedRangeAttributeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)
            : base(metadata, context, attribute) { }


        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = null;
            if (this.Attribute.ErrorMessage == null && (this.Attribute.ErrorMessageResourceName == null || this.Attribute.ErrorMessageResourceType == null))
                errorMessage = MvcApplication.GlobalResourceStringProvider.GetModelString(Metadata.ContainerType, Metadata.PropertyName, "Range");
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = ErrorMessage; // fallback to what ASP.NET MVC would normally display

            return new[] { new ModelClientValidationRangeRule(errorMessage, this.Attribute.Minimum, this.Attribute.Maximum) };
        }
    }

}