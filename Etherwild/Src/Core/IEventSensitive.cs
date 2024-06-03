using Etherwild.Controllers;
using EtherwildTransparencyTest.Controllers;

namespace EtherwildTransparencyTest.Core;

public interface IEventSensitive
{
    void RegisterEvents(EventController events);
    void ClearEvents(EventController events);
}