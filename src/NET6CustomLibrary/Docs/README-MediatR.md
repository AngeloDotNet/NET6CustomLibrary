# MediatR configuration

This README shows how to implement a Command (Insert) and a Query (GetAll), the implementations of the GetById, Update and Delete methods are missing.

A complete implementation example is available in this repository https://github.com/AngeloDotNet/Sample.MediatRV2

## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  // Replace the Program value, with a suitable handler (example: CreatePersonHandler),
  // if the MediatR implementations are not in the same assembly as the Program class
  services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
}
```

## Example of the PersonEntity class

```csharp
public class PersonEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}
```

## Example of the CreatePersonHandler class

```csharp
public class CreatePersonHandler : NET6CustomLibrary.MediatR.ICommandHandler<CreatePersonCommand, PersonEntity>
{
    private readonly ILogger<CreatePersonHandler> logger;
    private readonly IPeopleService peopleService;
    private readonly IMapper mapper;

    public CreatePersonHandler(ILogger<CreatePersonHandler> logger, IPeopleService peopleService, IMapper mapper)
    {
        this.logger = logger;
        this.peopleService = peopleService;
        this.mapper = mapper;
    }

    public async Task<PersonEntity> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var input = mapper.Map<PersonEntity>(command);

        await peopleService.CreatePersonAsync(input);

        var response = new PersonEntity()
        {
            Id = input.Id,
            UserId = input.UserId,
            Cognome = input.Cognome,
            Nome = input.Nome,
            Email = input.Email
        };

        return response;
    }
}
```

## Example of the CreatePersonCommand class

```csharp
public class CreatePersonCommand : NET6CustomLibrary.MediatR.ICommand<PersonEntity>
{
    public Guid UserId { get; set; }
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public CreatePersonCommand(PersonCreateInputModel inputModel)
    {
        UserId = inputModel.UserId;
        Cognome = inputModel.Cognome;
        Nome = inputModel.Nome;
        Email = inputModel.Email;
    }
}
```


## Example of the GetPeopleHandler class

```csharp
public class GetPeopleHandler : NET6CustomLibrary.MediatR.IQueryHandler<GetPeopleListQuery, List<PersonEntity>>
{
    private readonly ILogger<GetPeopleHandler> logger;
    private readonly IPeopleService peopleService;

    public GetPeopleHandler(ILogger<GetPeopleHandler> logger, IPeopleService peopleService)
    {
        this.logger = logger;
        this.peopleService = peopleService;
    }

    public async Task<List<PersonEntity>> Handle(GetPeopleListQuery request, CancellationToken cancellationToken)
    {
        var result = await peopleService.GetPeopleAsync();

        return result;
    }
}
```

## Example of the GetPeopleListQuery class

```csharp
public class GetPeopleListQuery : NET6CustomLibrary.MediatR.IQuery<List<PersonEntity>>
{
}
```