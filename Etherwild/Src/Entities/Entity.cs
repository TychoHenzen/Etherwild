using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Etherwild.Controllers;
using EtherwildTransparencyTest.Controllers;
using EtherwildTransparencyTest.Core;
using IComponent = EtherwildTransparencyTest.Entities.IComponent;

namespace EtherwildTransparencyTest.Entities;

public sealed class Entity : IEventSensitive
{
  private static ulong index = 0;
  public ulong Id { get; } = index++;
  public string Name { get; }
  private readonly List<IComponent> _components = [];

  internal Entity(string name)
  {
    Name = name;
  }

  internal void AddComponent<T>(params object[] args) where T : IComponent
  {
    var constructParams = new List<object>() { this };
    constructParams.AddRange(args);
    var component = (T?)Activator.CreateInstance(typeof(T), constructParams.ToArray());
    if (component is not null)
      _components.Add(component);
  }

  public IEnumerable<T> GetComponent<T>() where T : IComponent =>
    _components.OfType<T>();

  public bool HasComponent<T>() where T : IComponent
  {
    return _components.Exists(component => component is T);
  }

  public void RegisterEvents(EventController events)
  {
    foreach (var component in _components)
    {
      component.RegisterEvents(events);
    }
  }
  public void ClearEvents(EventController events)
  {
    foreach (var component in _components)
    {
      component.ClearEvents(events);
    }
  }
}
