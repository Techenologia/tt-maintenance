namespace TT.Backend.Core.Pipeline;

public class PipelineBuilder
{
    private readonly List<Func<Func<Task>, Func<Task>>> _components = new();

    public PipelineBuilder Use(Func<Func<Task>, Func<Task>> middleware)
    {
        _components.Add(middleware);
        return this;
    }

    public Func<Task> Build()
    {
        Func<Task> app = () => Task.CompletedTask;

        foreach (var component in _components.AsEnumerable().Reverse())
        {
            app = component(app);
        }

        return app;
    }
}