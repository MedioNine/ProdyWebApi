using System;

namespace Prody.Rest.Interfaces
{
    public interface IRequestParameter
    {
        string Name { get; }

        Type ValueType { get; }

        string StringValue { get; }

        bool IsEncoded { get; }
    }

    public interface IRequestParameter<out T> : IRequestParameter
    {
        T Value { get; }
    }
}
