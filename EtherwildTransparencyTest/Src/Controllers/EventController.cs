namespace EtherwildTransparencyTest.Controllers;

public class EventController
{
  private readonly ConcurrentDictionary<Type, List<Action<IComponent>>> _subscribers = new();

  public void Subscribe<T>(Action<T> handler) where T : IComponent
  {
    var key = typeof(T);
    if (!_subscribers.ContainsKey(key))
    {
      _subscribers[key] = new List<Action<IComponent>>();
    }
    _subscribers[key].Add((x) => handler((T)x));
  }

  public void Publish<T>(T component) where T : IComponent
  {
    var key = typeof(T);
    if (_subscribers.ContainsKey(key))
    {
      foreach (var handler in _subscribers[key])
      {
        // Consider dispatching on a separate thread or task for true async behavior
        Task.Run(() => handler(component));
      }
    }
  }
}
