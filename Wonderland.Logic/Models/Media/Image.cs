﻿
namespace Wonderland.Logic.Models.Media
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Umbraco.Core.Models.PublishedContent;

    public class Image : PublishedContentModel
    {
        public const string Alias = "Image";

        public Image(IPublishedContent content)
            : base(content)
        {
        }

        public string Url
        {
            get
            {
                return this.GetCropUrl(null);
            }
        }

        public string HtmlAltText
        {
            get
            {
                return this.GetPropertyValue<string>("htmlAltText");
            }
        }

    }
}
