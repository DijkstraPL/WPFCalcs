using Build_IT_Data.Calculators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_Data.Calculators
{
    public class Result : IResult
    {
        public object this[string name]
        {
            get => _properties[name];
            set
            {
                _properties[name] = value;
            }
        }

        public IDictionary<string, string> Descriptions { get; }

        private readonly IDictionary<string, object> _properties;

        public Result(IDictionary<string,string> properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            _properties = new Dictionary<string, object>();
            foreach (var property in properties)
                _properties.Add(property.Key, default);

            Descriptions = properties;
        }

        public IDictionary<string, object> GetProperties() => _properties;
    }
}
