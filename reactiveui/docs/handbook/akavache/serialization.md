# Serialization

Choose a serializer package and configure via builder.

System.Text.Json (recommended):
```csharp
var serializer = new SystemJsonSerializer
{
    Options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    }
};

AppBuilder.CreateSplatBuilder()
    .WithAkavache<SystemJsonSerializer>(() => serializer,
        b => b.WithSqliteDefaults());
```

Cross-serializer compatibility allows reading legacy JSON/BSON. Test migrations and back up caches before switching serializers.
