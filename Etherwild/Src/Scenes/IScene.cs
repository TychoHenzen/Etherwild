using EtherwildTransparencyTest.Controllers;
using EtherwildTransparencyTest.Core;

namespace EtherwildTransparencyTest.Scenes;

public interface IScene: IEventSensitive
{
  void InstantiateObjects(EntityController ctrl);
  void RemoveObjects(EntityController ctrl);
}