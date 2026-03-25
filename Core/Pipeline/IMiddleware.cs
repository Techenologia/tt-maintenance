namespace TT.Backend.Core.Pipeline;

public interface IMiddleware
{
    Task InvokeAsync(Func<Task> next);
}