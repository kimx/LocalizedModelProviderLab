using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;

namespace LocalizedModelProviderLab
{
    /// <summary>
    /// Used to return strings from one or more StringTables.
    /// </summary>
    /// <example>
    /// <code>
    /// var provider = new ResourceStringProvider(MyLocalizedStrings.ResourceProvider);
    /// </code>
    /// </example>
    /// <remarks>
    /// <para>Model translations should have the following format: "ClassName_PropertyName", for example: "User_FirstName". All
    /// extra metadata should have the following format: "ClassName_PropertyName_MetadataName".</para>
    /// <para>
    /// Validation error messages should just be named as the attributes, but without the "Attribute" suffix. Example: "Required".
    /// </para>
    /// </remarks>
    public class ResourceStringProvider : ILocalizedStringProvider
    {
        private readonly List<ResourceManager> _resourceManagers = new List<ResourceManager>();


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceStringProvider"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <example>
        /// </example>
        public ResourceStringProvider(params ResourceManager[] resourceManager)
        {
            _resourceManagers.AddRange(resourceManager);
        }

        #region ILocalizedStringProvider Members

        /// <summary>
        /// Get a localized string for a model property
        /// </summary>
        /// <param name="model">Model being localized</param>
        /// <param name="propertyName">Property to get string for</param>
        /// <returns>Translated string</returns>
        public string GetModelString(Type model, string propertyName)
        {
            return GetString(Format(model, propertyName));
            //return GetString(Format(model, propertyName)) ?? GetString(Format(typeof(CommonPrompts), propertyName));
        }

        /// <summary>
        /// Get a localized metadata for a model property
        /// </summary>
        /// <param name="model">Model being localized</param>
        /// <param name="propertyName">Property to get string for</param>
        /// <param name="metadataName">Valid names are: Watermark, Description, NullDisplayText, ShortDisplayText.</param>
        /// <returns>Translated string</returns>
        /// <remarks>
        /// Look at <see cref="ModelMetadata"/> to know more about the meta data
        /// </remarks>
        public string GetModelString(Type model, string propertyName, string metadataName)
        {
            return GetString(Format(model, propertyName, metadataName));
           // return GetString(Format(model, propertyName, metadataName)) ?? GetString(Format(typeof(CommonPrompts), propertyName, metadataName));
        }
      
        #endregion

        /// <summary>
        /// Format the model informaiton into a StringTable key.
        /// </summary>
        /// <param name="type">Model type</param>
        /// <param name="propertyName">Name of the property in the model</param>
        /// <param name="extras">Extras used during formatting</param>
        /// <returns>String Table key</returns>
        protected virtual string Format(Type type, string propertyName, params string[] extras)
        {
            var baseStr = string.Format("{0}_{1}", type.Name, propertyName);
            return extras.Aggregate(baseStr, (current, extra) => current + ("_" + extra));
        }

        /// <summary>
        /// Format the attribute type information into a StringTable key
        /// </summary>
        /// <param name="attributeType">Attribute type</param>
        /// <returns>String Table key</returns>
        protected virtual string Format(Type attributeType)
        {
            return attributeType.Name.Replace("Attribute", "");
        }

        /// <summary>
        /// Get a string from one of the string tables.
        /// </summary>
        /// <param name="name">String table item key</param>
        /// <returns>string if found; otherwise null.</returns>
        protected virtual string GetString(string name)
        {
            var result = _resourceManagers.Select(resourceManager => resourceManager.GetString(name)).FirstOrDefault(value => value != null);
            return result;
        }
    }
}