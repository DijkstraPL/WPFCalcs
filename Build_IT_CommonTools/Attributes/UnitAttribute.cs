using System;

namespace Build_IT_CommonTools.Attributes
{
    /// <summary>
    /// Class which contain information about unit of the property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnitAttribute : Attribute
    {
        private readonly string _unit;

        /// <value>
        /// Parameter which describes the unit of the property or field.
        /// </value>
        public string Unit => _unit;
        
        /// <summary>
        /// Constructor for unit attribute.
        /// </summary>
        /// <param name="unit">Unit for the field or property.</param>
        public UnitAttribute(string unit)
        {
            _unit = unit;
        }
    }
}
