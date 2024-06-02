using System;
using System.Collections.Generic;
using System.Linq;

namespace EtherwildTransparencyTest.Events;

public class TargetedEvent<TResponse> : ITargetedEvent<TResponse>, ILoopEvent
{
    private Func<ulong>? _parameterProvider;
    private Action<IEnumerable<TResponse>>? _responseHandler;
    private readonly Dictionary<ulong, Func<ulong, TResponse>> _listeners = [];

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
    
    public void Register(ulong entity, Func<ulong, TResponse> handler)
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
        var responses = _listeners[parameters].Invoke(parameters);
        _responseHandler?.Invoke([responses]);
    }
}