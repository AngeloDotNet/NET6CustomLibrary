# MailKit configuration


## Configuration to add to the appsettings.json file

```json
"Smtp": {
  "Host": "example.org",
  "Port": 25,
  "Security": "StartTls",
  "Username": "Username SMTP",
  "Password": "Password SMTP",
  "Sender": "MyName <noreply@example.org>"
}
```


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  services.AddMailKitEmailSenderService(Configuration);
}
```


## Example of the InputMailSender class
```csharp
public class InputMailSender
{
  public string RecipientEmail { get; set; }
  public string ReplyToEmail { get; set; }
  public string Subject { get; set; }
  public string HtmlMessage { get; set; }
}
```


## Example of use in a web api controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
  private readonly IEmailClient emailClient;

  public EmailController(IEmailClient emailClient)
  {
    this.emailClient = emailClient;
  }
  
  [HttpPost("InvioEmail")]
  public async Task<IActionResult> InvioEmail([FromForm] InputMailSender model)
  {
      try
      {
        var result = await emailClient.SendEmailAsync(model.RecipientEmail, model.ReplyToEmail, model.Subject, model.HtmlMessage);
        
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
      }
      catch
      {
         //OMISSIS
      }
  }
}
```