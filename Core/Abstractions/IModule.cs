namespace TT.Backend.Core.Abstractions;

public interface IModule
{
    string Name { get; }
    void Register(IServiceCollection services);
}