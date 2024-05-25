using System;

namespace Build_IT_CommonTools.Attributes
{
    /// <summary>
    /// Class which contain information about abbreviation of the property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AbbreviationAttribute : Attribute
    {
        private readonly string _abbreviation;

        /// <summary>
        /// Parameter which describes the abbreviation of the property or field.
        /// </summary>
        public string Abbreviation => _abbreviation;

        /// <summary>
        /// Constructor for abbreviation attribute.
        /// </summary>
        /// <param name="abbreviation">Abbreviation for the field or property.</param>
        public AbbreviationAttribute(string abbreviation)
        {
            _abbreviation = abbreviation;
        }
    }
}
