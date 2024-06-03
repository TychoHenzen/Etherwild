using System;
using System.Collections.Generic;
using System.Linq;

namespace EtherwildTransparencyTest.Events;

public abstract class TargetedEvent<TResponse> : ITargetedEvent<TResponse>, ILoopEvent
{
    private Func<ulong>? _parameterProvider;
    private Action<IEnumerable<TResponse>>? _responseHandler;
    private readonly Dictionary<ulong, Func<TResponse>> _listeners = [];

    public int Sequence { get; }
    public string Name { get; }
    public TargetedEvent(int sequence, string name)
    {
        Sequence = sequence;
        Name = name;
    }

    public void RegisterParameterProvider(Func<ulong>? provideParameters)
    {
        _parameterProvider = provideParameters;
    }
    public void RegisterResponseHandler(Action<IEnumerable<TResponse>> handleResponses)
    {
        _responseHandler = handleResponses;
    }
    
    public void Register(ulong entity, Func<TResponse> handler)
    {
        _listeners.Add(entity, handler);
    }
    public void Remove(ulong toRemove)
    {
        _listeners.Remove(toRemove);
    }
    
    public void Execute()
    {
        if (_parameterProvider == null) return;
        var parameters = _parameterProvider.Invoke();
        var responses = _listeners[parameters].Invoke();
        _responseHandler?.Invoke([responses]);
    }
    public void Execute(ulong target)
    {
        if (_parameterProvider == null) return;
        var responses = _listeners[target].Invoke();
        _responseHandler?.Invoke([responses]);
    }
}