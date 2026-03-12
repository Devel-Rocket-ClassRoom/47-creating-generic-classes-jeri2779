using System;
using System.Collections.Generic;

// README.md를 읽고 아래에 코드를 작성하세요.
Console.WriteLine("코드를 작성하세요.");

public interface IPoolable
{
    void Reset();
}

class ObjectPool<T> where T : class, IPoolable, new()
{
    private readonly List<T> _available = new List<T>();
    private readonly List<T> _active = new List<T>();
    private readonly int _maxSize;

    public int ActiveCount => _active.Count;
    public int AvailableCount => _available.Count;

    public ObjectPool(int initialSize)
    {
        
        _maxSize = initialSize * 2;
    }
}
