using System;
using System.Collections.Generic;

// README.md를 읽고 아래에 코드를 작성하세요.
Console.WriteLine("코드를 작성하세요.");

public interface IPoolable
{
    public void Reset();
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

        _maxSize = initialSize;
    }

    public T Get()
    {
        int totalCount = _available.Count + _active.Count;
        if (_available.Count <= 0)
        {
            if (totalCount < _maxSize)
            {
                var newItem = new T();
                _available.Add(newItem);
                return newItem;
            }
            return null;
        }
        if (totalCount >= _maxSize)
        {
            Console.WriteLine("풀이 가득 찼습니다!");
        }
        return null;
    }

}

class Bullet : IPoolable
{
    public bool IsActive;
    public int X, Y;
    public int Damage { get; set; }

    public void Fire(int x, int y)
    {
        X = x;
        Y = y;
        IsActive = true;
        Console.WriteLine($"총알 발사! 위치: ({X}, {Y})");
    }
    public void Reset()
    {
        IsActive = false;
        X = 0;
        Y = 0;
    }
}   
