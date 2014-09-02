
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
            //this.Address1 = 
            //this.Address2 = 
            //this.TownCity = 
            //this.Postcode = 
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
