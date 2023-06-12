# Custom response result configuration


## Example of custom message for a 200 Ok status

```csharp
  //with result
  return Ok(new DefaultResponse(true, result));

  //without result
  return Ok(new DefaultResponse(HttpStatusCode.Ok, true));
```

<b>Note:</b> result can be a message in string format or a response class that returns a DTO

## Example of custom message for a 404 NotFound status

```csharp
  //with TypeCode, indicated in second position by the value zero
  throw new ExceptionResponse(HttpStatusCode.NotFound, 0, "NotFound", "Insert a custom object not found message here");

  //without TypeCode
  throw new ExceptionResponse(HttpStatusCode.NotFound, "NotFound", "Insert a custom object not found message here");
```

:alarm_clock: More status codes will be added soon
