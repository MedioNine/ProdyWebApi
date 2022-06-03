using Newtonsoft.Json;
using Prody.Rest.Interfaces;
using System;

namespace Prody.Rest
{
    public class RequestParameter<T> : IRequestParameter<T>
    {
        public T Value { get; set; }

        public string Name { get; set; }

        public Type ValueType => typeof(T);

        public string StringValue => JsonConvert.SerializeObject(Value);

        public bool IsEncoded { get; set; }

        public RequestParameter(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}
