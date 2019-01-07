﻿using System;
using Newtonsoft.Json;
using NServiceBus.Testing;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

class TimeoutMessageConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        var at = OutgoingMessageHelper.GetAt(value);
        if (at != null)
        {
            writer.WritePropertyName("At");
            serializer.Serialize(writer, at);
        }

        var within = OutgoingMessageHelper.GetWithin(value);
        if (within != null)
        {
            writer.WritePropertyName("Within");
            serializer.Serialize(writer, within);
        }

        OutgoingMessageConverter.WriteBaseMembers(writer, value, serializer);

        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type type, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanConvert(Type type)
    {
        if (type.IsGenericType)
        {
            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(TimeoutMessage<>);
        }
        return false;
    }
}