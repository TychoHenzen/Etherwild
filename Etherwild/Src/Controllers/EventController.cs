using System;
using System.Collections.Generic;
using System.Linq;
using EtherwildTransparencyTest.Events;

namespace Etherwild.Controllers;

public sealed class EventController
{
  private Dictionary<string, ILoopEvent> _events = [];

  public EventController()
  {
    Console.WriteLine("Starting evt ctrl");
    var loopEventTypes = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(assembly => assembly.GetTypes())
      .Where(type => typeof(ILoopEvent).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
      .Where(type => type.GetConstructor([typeof(string)]) != null).ToArray();
    
    Console.WriteLine("creating "+loopEventTypes.Count() +" handlers");
    foreach (Type type in loopEventTypes)
    {
      var eventInstance = (ILoopEvent?)Activator.CreateInstance(type, type.Name);
      if (eventInstance == null)
        throw new ArgumentNullException(nameof(eventInstance), $"failed to instantiate {type.Name} event");

      Console.WriteLine("Created handler for "+type.Name);
      _events.Add(type.Name, eventInstance);
    }
  }

  public IPublisherEvent<TParams, TResponse> Publish<TEvent, TParams, TResponse>() where TEvent : ILoopEvent
  {
    return (IPublisherEvent<TParams, TResponse>)_events[typeof(TEvent).Name];
  }

  public IGeneralListenerEvent<TParams, TResponse> Listen<TEvent, TParams, TResponse>() where TEvent : ILoopEvent
  {
    return (IGeneralListenerEvent<TParams, TResponse>)_events[typeof(TEvent).Name];
  }

  public ITargetedListenerEvent<TResponse> ListenTargeted<TEvent, TResponse>() where TEvent : ILoopEvent
  {
    return (ITargetedListenerEvent<TResponse>)_events[typeof(TEvent).Name];
  }

  public void ExecuteEvents<TEvent>()
  {
    var evtName = typeof(TEvent).Name;
    if(_events.ContainsKey(evtName))
    _events[evtName].Execute();
  }
}
