using EtherwildTransparencyTest.Controllers;

namespace EtherwildTransparencyTest.Entities;

public interface IComponent
{
  void RegisterEvents(EventController events);
}