using Etherwild.Controllers;
using EtherwildTransparencyTest.Controllers;

namespace EtherwildTransparencyTest.Entities;

public abstract class BaseComponent : IComponent
{
  protected BaseComponent(Entity owner)
  {
    Owner = owner;
  }

  public Entity Owner { get; }
  public abstract void RegisterEvents(EventController events);
  public abstract void ClearEvents(EventController events);
}
