using System;
using System.Collections.Generic;
using System.Linq;

namespace EtherwildTransparencyTest.Events;

public abstract class NotifyEveryoneEvent<TParams, TResponse> : IGeneralEvent<TParams, TResponse>, ILoopEvent
{
    private Func<TParams>? _parameterProvider;
    private Action<IEnumerable<TResponse>>? _responseHandler;
    private readonly Dictionary<ulong, Func<TParams, TResponse>> _listeners = [];

    public string Name { get; }
    public NotifyEveryoneEvent(string name)
    {
        Name = name;
    }

    public void RegisterParameterProvider(Func<TParams>? provideParameters)
    {
        _parameterProvider = provideParameters;
    }
    public void RegisterResponseHandler(Action<IEnumerable<TResponse>> handleResponses)
    {
        _responseHandler = handleResponses;
    }
    
    public void Register(ulong index, Func<TParams, TResponse> handler)
    {
        if (_listeners.ContainsKey(index))
            throw new ArgumentException("index already registered", nameof(index));
        
        _listeners.Add(index, handler);
    }
    public ulong Register(Func<TParams, TResponse> handler)
    {
        ulong index = 0;
        while (_listeners.ContainsKey(index))
        {
            index++;
        }
        Console.WriteLine("Registering handler for "+ Name + " at "+index);
        _listeners.Add(index, handler);
        return index;
    }
    public void Remove(ulong toRemove)
    {
        _listeners.Remove(toRemove);
    }
    
    public void Execute()
    {
        if (_parameterProvider == null) return;
        var parameters = _parameterProvider.Invoke();
        var responses = _listeners.OrderBy(pair => pair.Key).Select(listener => listener.Value.Invoke(parameters)).ToArray();
        _responseHandler?.Invoke(responses);
    }
    public void Execute(TParams arg)
    {
        var responses = _listeners.OrderBy(pair => pair.Key).Select(listener => listener.Value.Invoke(arg)).ToArray();
        _responseHandler?.Invoke(responses);
    }
}
