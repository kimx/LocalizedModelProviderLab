using LocalizedModelProviderLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalizedModelProviderLab
{
    public class LocalizedCompareAttributeAdapter : DataAnnotationsModelValidator<System.ComponentModel.DataAnnotations.CompareAttribute>
    {
        public LocalizedCompareAttributeAdapter(ModelMetadata metadata, ControllerContext context, System.ComponentModel.DataAnnotations.CompareAttribute attribute)
            : base(metadata, context, attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = null;
            if (this.Attribute.ErrorMessage == null && (this.Attribute.ErrorMessageResourceName == null || this.Attribute.ErrorMessageResourceType == null))
                errorMessage = MvcApplication.GlobalResourceStringProvider.GetModelString(Metadata.ContainerType, Metadata.PropertyName, "Compare");
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = ErrorMessage; // fallback to what ASP.NET MVC would normally display

            return new[] { new ModelClientValidationEqualToRule(errorMessage, string.Format("*.{0}", Attribute.OtherProperty)) };//*.{0} from MVC 5 SourceCode
        }
    }


}

