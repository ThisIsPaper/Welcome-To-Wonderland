
namespace Wonderland.Logic.Models.Entities
{
    using System;
    using System.ComponentModel;

    public class Address
    {
        //internal Address()
        //{
        //}

        internal Address(string address1, string address2, string townCity, string postCode)
        {
            this.Address1 = address1.Replace(Environment.NewLine, string.Empty);
            if (!string.IsNullOrWhiteSpace(address2))
            {
                this.Address2 = address2.Replace(Environment.NewLine, string.Empty);
            }
            this.TownCity = townCity.Replace(Environment.NewLine, string.Empty);
            this.Postcode = postCode.Replace(Environment.NewLine, string.Empty);
        }

        internal Address(string address)
        {
            try
            {
                string[] addressParts = address.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                this.Address1 = addressParts[0];
                this.Address2 = addressParts[1];
                this.TownCity = addressParts[2];
                this.Postcode = addressParts[3];
            }
            catch
            {
                this.Address1 = string.Empty;
                this.Address2 = string.Empty;
                this.TownCity = string.Empty;
                this.Postcode = string.Empty;
            }
        }

        public string Address1 { get; internal set; }

        public string Address2 { get; internal set; }

        public string TownCity { get; internal set; }

        public string Postcode { get; internal set; }

        /// <summary>
        /// converts this address to a carrige return delimited string (for cms presentation)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(
                            Environment.NewLine, 
                            new string[] 
                            {
                                this.Address1,
                                this.Address2,
                                this.TownCity,
                                this.Postcode
                            });
        }
    }
}
