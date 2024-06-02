using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EtherwildTransparencyTest.Events;

namespace EtherwildTransparencyTest.Controllers;

public class EventController
{
  private Dictionary<int, ILoopEvent> _events = [];

  public void CreateEvent<TParams, TResponse>(int sequence, string name)
  {
    if (sequence > 10000)
      throw new ArgumentException("Sequence must be under 10,000");
    if (_events.Values.Any(evt => evt.Name == name))
      throw new DuplicateNameException($"Event with name {name} already exists");
    if (_events.Keys.Any(evt => evt == sequence))
      throw new DuplicateNameException($"{_events.First(evt => evt.Key == sequence).Value} has same priority as {name}");
    
    var newEvent = new NotifyEveryoneEvent<TParams, TResponse>(sequence, name);

    _events.Add(newEvent.Sequence, newEvent);
  }
  public void CreateEvent<TResponse>(int sequence, string name)
  {
    if (sequence > 10000)
      throw new ArgumentException("Sequence must be under 10,000");
    if (_events.Values.Any(evt => evt.Name == name))
      throw new DuplicateNameException($"Event with name {name} already exists");
    if (_events.Keys.Any(evt => evt == sequence))
      throw new DuplicateNameException($"{_events.First(evt => evt.Key == sequence).Value} has same priority as {name}");
    
    var newEvent = new TargetedEvent<TResponse>(sequence, name);
    _events.Add(newEvent.Sequence, newEvent);
  }
  
  public IPublisherEvent<TParams,TResponse> Publish<TParams, TResponse>(string name)
  {
    return (IPublisherEvent<TParams, TResponse>)_events.Values.First(evt => evt.Name == name);
  }
  public IGeneralListenerEvent<TParams,TResponse> Listen<TParams, TResponse>(string name)
  {
    return (IGeneralListenerEvent<TParams, TResponse>)_events.Values.First(evt => evt.Name == name);
  }
  public ITargetedListenerEvent<TResponse> Listen<TResponse>(string name)
  {
    return (ITargetedListenerEvent<TResponse>)_events.Values.First(evt => evt.Name == name);
  }
  public IPublisherEvent<TParams,TResponse> Publish<TParams, TResponse>(int sequence)
  {
    return (IPublisherEvent<TParams, TResponse>)_events[sequence];
  }
  public IGeneralListenerEvent<TParams,TResponse> Listen<TParams, TResponse>(int sequence)
  {
    return (IGeneralListenerEvent<TParams, TResponse>)_events[sequence];
  }
  public ITargetedListenerEvent<TResponse> Listen<TResponse>(int sequence)
  {
    return (ITargetedListenerEvent<TResponse>)_events[sequence];
  }
  public void ExecuteEvents(int minSequence, int count = 1000)
  {
    int startIndex = minSequence;
    int endIndex = minSequence+count;

    for (int i = startIndex; i <= endIndex; i++)
    {
      if(_events.TryGetValue(i, out ILoopEvent? value))
                value.Execute();
    }
  }
}