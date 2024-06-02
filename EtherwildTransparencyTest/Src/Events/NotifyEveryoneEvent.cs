using System;
using System.Collections.Generic;
using System.Linq;

namespace EtherwildTransparencyTest.Events;

public class NotifyEveryoneEvent<TParams, TResponse> : IGeneralEvent<TParams, TResponse>, ILoopEvent
{
    private Func<TParams>? _parameterProvider;
    private Action<IEnumerable<TResponse>>? _responseHandler;
    private readonly Dictionary<IToken, Func<TParams, TResponse>> _listeners = [];

    public int Sequence { get; }
    public string Name { get; }
    public NotifyEveryoneEvent(int sequence, string name)
    {
        Sequence = sequence;
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
    
    public IToken Register(Func<TParams, TResponse> handler)
    {
        var token = new Token();    
        _listeners.Add(token, handler);
        return token;
    }
    public void Remove(IToken toRemove)
    {
        _listeners.Remove(toRemove);
    }
    
    public void Execute()
    {
        if (_parameterProvider == null) return;
        var parameters = _parameterProvider.Invoke();

        var responses = _listeners.Values.Select(listener => listener.Invoke(parameters)).ToArray();
        _responseHandler?.Invoke(responses);
    }
}

public class Token : IToken
{
    private static int index = 0;
    private int myIndex = index++;
}