using EasyNetQ;
using System.Collections.Concurrent;

namespace Security.Infrastructure.MessageBus.Configurations
{
    public sealed class EventBusTypeNameSerializer : ITypeNameSerializer
    {
        private readonly ConcurrentDictionary<Type, string> serializedTypes = new();
        private readonly ConcurrentDictionary<string, Type> deSerializedTypes = new();

        public Type DeSerialize(string typeName)
        {
            return deSerializedTypes.GetOrAdd(typeName, t =>
            {
                var type = Type.GetType(typeName);
                if (type == null)
                {
                    throw new EasyNetQException("Cannot find type {0}", t);
                }
                return type;
            });
        }

        public string Serialize(Type type)
        {
            return serializedTypes.GetOrAdd(type, t =>
            {
                var typeName = type.FullName;
                if (typeName.Length > 255)
                {
                    throw new EasyNetQException("The serialized name of type '{0}' exceeds the AMQP " +
                                                  "maximum short string length of 255 characters.", t.Name);
                }
                return typeName;
            });
        }
    }
}
