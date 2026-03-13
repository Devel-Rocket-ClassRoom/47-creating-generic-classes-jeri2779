using System;
using System.Collections.Generic;

// README.md를 읽고 아래에 코드를 작성하세요.
 
Console.WriteLine("=== 총알 발사 ===");
ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>(3);
Bullet bullet1 = bulletPool.Get();
Bullet bullet2 = bulletPool.Get();
Bullet bullet3 = bulletPool.Get();

bullet1.Fire(10, 20);
bullet2.Fire(30, 40);
bullet3.Fire(50, 60);
Console.WriteLine($"활성: {bulletPool.ActiveCount}, " +
                  $"비활성: {bulletPool.AvailableCount}");

Console.WriteLine();

Console.WriteLine("=== 풀 초과 시도 ===");
Bullet bullet4 = bulletPool.Get();


Console.WriteLine();

Console.WriteLine("=== 반납 후 재사용 ===");
bulletPool.Return(bullet1);
Console.WriteLine($"활성: {bulletPool.ActiveCount}, " +
                  $"비활성: {bulletPool.AvailableCount}");
bulletPool.Return(bullet1);
Bullet reuse = bulletPool.Get();
reuse.Fire(100, 200);

Console.WriteLine($"활성: {bulletPool.ActiveCount}, " +
                  $"비활성: {bulletPool.AvailableCount}"); 

public interface IPoolable
{
    public void Reset();
}

class ObjectPool<T> where T : class, IPoolable, new()
{
    private readonly List<T> _available = new List<T>();//비활성 목록
    private readonly List<T> _active = new List<T>();//활성 목록
    private readonly int _maxSize;

    public int ActiveCount => _active.Count;
    public int AvailableCount => _available.Count;

    public ObjectPool(int initialSize)
    {

        _maxSize = initialSize;
    }

    //public T Get()
    //{
    //    int totalCount = _available.Count + _active.Count;
    //    if (totalCount >= _maxSize)
    //    {
    //        Console.WriteLine("풀이 가득 찼습니다!");
    //        return null;
    //    }
    //    if (_available.Count <= 0)//비활성 목록이 비어있으면
    //    {

    //        var newItem = new T();//새 객체 생성
    //        _active.Add(newItem);//새 객체를 활성 목록에 추가
    //        return newItem; 
    //    }

    //        T item = _available[0];//비활성 목록에서 첫 번째 객체를 가져옴
    //        _available.RemoveAt(0);//비활성 목록에서 제거
    //        _active.Add(item);//활성 목록에 추가
    //        return item;//객체 반환   
    //}

    public T Get()
    {
        // 1. 먼저 비활성(Available) 목록에 재사용 가능한 게 있는지 확인
        if (_available.Count > 0)
        {
            var item = _available[0];
            _available.RemoveAt(0);
            _active.Add(item);
            return item;
        }

        // 2. 재사용할 게 없다면, 새로 만들 수 있는지 확인 (최대치 체크)
        if (_active.Count + _available.Count >= _maxSize)
        {
            Console.WriteLine("풀이 가득 찼습니다! 더 이상 생성할 수 없습니다.");
            return null; // 호출하는 곳에서 반드시 null 체크를 해야 합니다!
        }

        // 3. 새로 생성하여 반환
        var newItem = new T();
        _active.Add(newItem);
        return newItem;
    }

    public void Return(T item)
    {
        if (_active.Remove(item))
        {
            item.Reset();
            _available.Add(item);
            Console.WriteLine("총알 반납됨");
        }
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
