# DIImplementation
Dependency Injection is a software design pattern, which is a technique for achieving inversion of control(IOC) between the classes and their dependencies
A dependency is an object that another object depends on
In this repository, I tried to explain the use of three service lifetime methods -> AddSingleton(), AddTransient() and AddScoped()

Create three lifetime classes 
public class Transient
{
private readonly Guid _guid;
public Transient(){
  _guid = Guid.NewGuid();
}
public string GetGuid()
{
    return _guid.ToString();
}
}

public class Scoped
{
private readonly Guid _guid;
public Scoped(){
  _guid = Guid.NewGuid();
}
public string GetGuid()
{
    return _guid.ToString();
}
}

public class Singleton
{
private readonly Guid _guid;
public Singleton(){
  _guid = Guid.NewGuid();
}
public string GetGuid()
{
    return _guid.ToString();
}
}

//custom middleware 


public class CustomMiddleware
{
    private readonly RequestDelegate next;

    public CustomMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context,TransientService transientService,ScopeService scopeService,SingletonService singletonService) { 
        context.Items.Add("MiddlewareTransientService",transientService.GetGuid());
        context.Items.Add("MiddlewareScopeService", scopeService.GetGuid());
        context.Items.Add("MiddlewareSingletonService", singletonService.GetGuid());
        await next(context);
    }
}


register three services in IOC container and add custom middleware to the pipeline 

builder.Services.AddTransient<TransientService>();
builder.Services.AddScoped<ScopeService>();
builder.Services.AddSingleton<SingletonService>();


app.UseMiddleware<CustomMiddleware>();


Injected the three services in the controller through constructor injection 

public class HomeController : Controller
{
private readonly TransientService transientService;
private readonly ScopeService scopeService;
private readonly SingletonService singletonService;

public HomeController(TransientService transientService, ScopeService scopeService, SingletonService singletonService)
{
    this.transientService = transientService;
    this.scopeService = scopeService;
    this.singletonService = singletonService;
}

public IActionResult Index()
{
    var messages = new List<string>()
    {
        HttpContext.Items["MiddlewareTransientService"].ToString(),
        $"Transient Controller - {transientService.GetGuid()}",
         HttpContext.Items["MiddlewareScopeService"].ToString(),
        $"Transient Controller - {scopeService.GetGuid()}",
         HttpContext.Items["MiddlewareSingletonService"].ToString(),
        $"Transient Controller - {singletonService.GetGuid()}"

    };
    
    return View(messages);
}
}


//display messages on UI
