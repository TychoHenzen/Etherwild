using System;
using System.Collections.Generic;
using System.Linq;
using EtherwildTransparencyTest.Entities;
using EtherwildTransparencyTest.Interfaces;

namespace EtherwildTransparencyTest.Controllers;

public class EntityController
{
  private readonly Dictionary<ulong,Entity> _entities = new();

  public Entity CreateEntity(string name)
  {
    var ret = new Entity(name);
    _entities.Add(ret.Id, ret);
    return ret;
  }
  public IEnumerable<Entity> this[string name] => _entities.Values.Where(entity => entity.Name == name);
  public Entity this[ulong index] => _entities[index];
  public void DestroyEntity(Entity entity)
  {
    _entities.Remove(entity.Id);
  }
}