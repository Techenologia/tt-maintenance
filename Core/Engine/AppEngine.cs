using TT.Backend.Core.Abstractions;

namespace TT.Backend.Core.Engine;

public class AppEngine
{
    private readonly List<IModule> _modules = new();

    public void RegisterModule(IModule module)
    {
        _modules.Add(module);
    }

    public void Start(IServiceCollection services)
    {
        foreach (var module in _modules)
        {
            module.Register(services);
        }
    }

    public IEnumerable<string> GetModules()
    {
        return _modules.Select(m => m.Name);
    }
}