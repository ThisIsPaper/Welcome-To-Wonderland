
namespace Wonderland.Logic.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    public abstract class NonSerializablePublishedContentModel : PublishedContentModel
    {
        public NonSerializablePublishedContentModel(IPublishedContent content)
            : base(content)
        {
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override IEnumerable<IPublishedContent> Children
        {
            get
            {
                return base.Children;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override IEnumerable<IPublishedContent> ContentSet
        {
            get
            {
                return base.ContentSet;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override PublishedContentType ContentType
        {
            get
            {
                return base.ContentType;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override DateTime CreateDate
        {
            get
            {
                return base.CreateDate;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override int CreatorId
        {
            get
            {
                return base.CreatorId;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override string CreatorName
        {
            get
            {
                return base.CreatorName;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override string DocumentTypeAlias
        {
            get
            {
                return base.DocumentTypeAlias;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override int DocumentTypeId
        {
            get
            {
                return base.DocumentTypeId;
            }
        }

        //[ScriptIgnore]
        //[JsonIgnore]
        [JsonProperty("id")]
        public override int Id
        {
            get
            {
                return base.Id;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override bool IsDraft
        {
            get
            {
                return base.IsDraft;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override PublishedItemType ItemType
        {
            get
            {
                return base.ItemType;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override int Level
        {
            get
            {
                return base.Level;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override string Name
        {
            get
            {
                return base.Name;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override IPublishedContent Parent
        {
            get
            {
                return base.Parent;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override string Path
        {
            get
            {
                return base.Path;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override ICollection<IPublishedProperty> Properties
        {
            get
            {
                return base.Properties;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override int SortOrder
        {
            get
            {
                return base.SortOrder;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override int TemplateId
        {
            get
            {
                return base.TemplateId;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override object this[string alias]
        {
            get
            {
                return base[alias];
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override DateTime UpdateDate
        {
            get
            {
                return base.UpdateDate;
            }
        }

        //[ScriptIgnore]
        //[JsonIgnore]
        [JsonProperty("url")]
        public override string Url
        {
            get
            {
                return base.Url;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override string UrlName
        {
            get
            {
                return base.UrlName;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override Guid Version
        {
            get
            {
                return base.Version;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override int WriterId
        {
            get
            {
                return base.WriterId;
            }
        }

        [ScriptIgnore]
        [JsonIgnore]
        public override string WriterName
        {
            get
            {
                return base.WriterName;
            }
        }       
    }
}
