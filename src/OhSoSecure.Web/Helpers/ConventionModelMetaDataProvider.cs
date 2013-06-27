using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FluentNHibernate.Utils;
using OhSoSecure.Core.Web;

namespace OhSoSecure.Web.Helpers
{
    public class ConventionModelMetaDataProvider: DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes, Type containerType,
            Func<object> modelAccessor, Type modelType, string propertyName)
        {
            ModelMetadata metadata = base.CreateMetadata(attributes,
                                                         containerType,
                                                         modelAccessor,
                                                         modelType,
                                                         propertyName);

            if (metadata.DisplayName == null)
                metadata.DisplayName =
                    metadata.PropertyName.ToSeparatedWords();
            
            attributes.OfType<RequiredAttribute>().Each(a => a.ErrorMessage = "{0} is required.");
//            attributes.OfType<RegexAttribute>().Each(a => metadata.Description = a.Description);
//            attributes.OfType<UrlAttribute>().Each(a => metadata.AdditionalValues["prepend_http"] = true);
            return metadata;
        }
    }
}
