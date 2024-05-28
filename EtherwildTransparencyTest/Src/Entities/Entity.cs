using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IComponent = EtherwildTransparencyTest.Entities.IComponent;

namespace EtherwildTransparencyTest.Entities;

public sealed class Entity
{
  public int Id { get; }
  public string Name { get; }
  private readonly List<IComponent> _components = [];

  internal Entity(int id, string name)
  {
    Id = id;
    Name = name;
  }

  internal void AddComponent<T>(params object[] args) where T : IComponent
  {
    var component = (T?)Activator.CreateInstance(typeof(T), args);
    if (component is not null)
      _components.Add(component);
  }

  public IEnumerable<T> GetComponent<T>() where T : IComponent =>
    _components.OfType<T>();

  public bool HasComponent<T>() where T : IComponent
  {
    return _components.Exists(component => component is T);
  }
}
