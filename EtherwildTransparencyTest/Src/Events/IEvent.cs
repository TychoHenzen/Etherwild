using System;
using System.Collections.Generic;

namespace EtherwildTransparencyTest.Events;
// Interface for events as seen by the publisher
public interface IPublisherEvent<TParams, TResponse>
{
    void RegisterParameterProvider(Func<TParams>? provideParameters);
    void RegisterResponseHandler(Action<IEnumerable<TResponse>> handleResponses);
}

// Interface for events as seen by the listeners
public interface IGeneralListenerEvent<TParams, TResponse>
{
    IToken Register(Func<TParams,TResponse> handler);
    void Remove(IToken toRemove);
}
// Interface for events as seen by the listeners
public interface ITargetedListenerEvent<TResponse>
{
    void Register(ulong target, Func<ulong,TResponse> handler);
    void Remove(ulong toRemove);
}
public interface IToken
{}

// Base event interface combining both aspects
public interface IGeneralEvent<TParams, TResponse> : 
    IPublisherEvent<TParams, TResponse>, IGeneralListenerEvent<TParams, TResponse>
{
}
public interface ITargetedEvent<TResponse> : 
    IPublisherEvent<ulong, TResponse>, ITargetedListenerEvent<TResponse>
{
}

public interface ILoopEvent
{
    int Sequence { get; }
    string Name { get; }
    void Execute();
}