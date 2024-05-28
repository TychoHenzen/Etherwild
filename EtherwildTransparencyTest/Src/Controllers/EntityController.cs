using System;
using System.Collections.Generic;
using System.Linq;
using EtherwildTransparencyTest.Entities;
using EtherwildTransparencyTest.Interfaces;

namespace EtherwildTransparencyTest.Controllers;

public class EntityController
{
  private int _nextId;
  private readonly Dictionary<int,Entity> _entities = new();

  public Entity CreateEntity(string name)
  {
    var id = _nextId++;
    _entities.Add(id, new Entity(id, name));
    return _entities[id];
  }
  public IEnumerable<Entity> this[string name] => _entities.Values.Where(entity => entity.Name == name);
  public Entity this[int index] => _entities[index];
  public void DestroyEntity(Entity entity)
  {
    _entities.Remove(entity.Id);
  }
}