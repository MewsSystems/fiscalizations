# Advanced examples

### Verification mode
```csharp
var response = await client.SendRevenueAsync(record, EetMode.Verification);
```

### Using playground
```csharp
var client = new EetClient(certificate, EetEnvironment.Playground);
```

### Timeouts

The default HTTP request timeout is set to 2 seconds as per the Czech law. If you want to change it, you need to pass a parameter to the EetClient constructor:

```csharp
var client = new EetClient(certificate, EetEnvironment.Playground, httpTimeout: TimeSpan.FromSeconds(20));
```

### Logging
- Catchall logger:
```csharp
Action<string, object> logHandler = (message, detailsObject) => { ... };
var client = new EetClient(
    certificate,
    logger: logHandler
);
```

- Selective logger:
```csharp
Action<string, object> logHandler = (message, detailsObject) => { ... };
var client = new EetClient(
    certificate,
    logger: new EetLogger(onError: logHandler, onInfo: logHandler, onDebug: null)
);
```

### Events
- HTTP request duration
```csharp
var client = new EetClient(certificate);
client.HttpRequestFinished += (sender, args) =>
{
    var duration = args.Duration;
};
```

- Catching XML message sent to EET
```csharp
var client = new EetClient(certificate);
client.XmlMessageSerialized += (sender, args) =>
{
    var xmlString = args.XmlElement.OuterXml;
};
```
