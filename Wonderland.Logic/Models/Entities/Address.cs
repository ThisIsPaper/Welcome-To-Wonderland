
namespace Wonderland.Logic.Models.Entities
{
    using System;

    public class Address
    {
        internal Address()
        {
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
