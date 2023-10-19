using DIExample.Lifetimes;

namespace DIExample.Middlewares
{
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
}
