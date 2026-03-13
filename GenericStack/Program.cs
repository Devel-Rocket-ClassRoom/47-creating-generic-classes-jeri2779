using System;
using System.Collections;
using System.Collections.Generic;


SimpleQueue<int> queue = new SimpleQueue<int>(3);
SimpleQueue<string> stringQueue = new SimpleQueue<string>(2);
Console.WriteLine("=== int 큐 (용량: 3) ===");
queue.Enqueue(10);
queue.Enqueue(20);
queue.Enqueue(30);

Console.WriteLine($"Count: {queue.Count}, IsEmpty: {queue.IsEmpty}");

queue.Enqueue(40); // 큐가 가득 찼습니다. 출력

Console.Write("Enqueue: ");
Console.WriteLine(string.Join(", ", queue));

Console.WriteLine($"Peek: {queue.Peek()}");

queue.Dequeue();
queue.Dequeue();

Console.WriteLine($"Count: {queue.Count}, IsEmpty: {queue.IsEmpty}");
Console.WriteLine();

Console.WriteLine("=== string 큐 (용량: 2) ===");
stringQueue.Enqueue("Hello");
stringQueue.Enqueue("World");

Console.WriteLine(string.Join(", ", stringQueue));

Console.WriteLine($"Dequeue: {stringQueue.Dequeue()}");
Console.WriteLine($"Dequeue: {stringQueue.Dequeue()}");
Console.Write($"{stringQueue.Dequeue()}");//WriteLine으로 하면 자동 한줄 이 줄바꿈 출력됨 원인???
Console.WriteLine($"Dequeue:");
Console.WriteLine($"IsEmpty: {stringQueue.IsEmpty}");

 
 
class SimpleQueue<T> : IEnumerable<T>
{
    private T[] _items; // 큐의 요소를 저장하는 배열
    private int _head; // 큐의 요소 out
    private int _tail; // 큐의 요소 in
    private int _count; // 큐에 저장된 요소의 개수
    private int _capacity; // 큐의 최대 용량

    public int Count => _count;// 큐에 저장된 요소의 개수 읽기 전용.
    public bool IsFull => _count == _capacity; // 큐가 가득 찼는지 여부 읽기 전용.
    public bool IsEmpty => _count == 0; // 큐가 비어 있는지 여부 읽기 전용. 

    public SimpleQueue(int capacity)
    {
        _capacity = capacity;
        _items = new T[capacity];// 참조타입이므로 new T[capacity]로 초기화.
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public void Enqueue(T item)
    {
        if(IsFull)
        {
            Console.WriteLine("큐가 가득 찼습니다.");
            return;
        }
        _items[_tail] = item;
        _tail = (_tail + 1) % _capacity; // 원형 큐이므로 인덱스를 순환시킴.
        _count++;
    }
    public T Dequeue()
    {
        if(IsEmpty)
        {
            Console.WriteLine("큐가 비어있습니다.");
            return default(T); // 큐가 비어있을 때 기본값 반환. 
        }
         T item = _items[_head];// 큐의 시작 인덱스에 있는 요소를 반환하기 위해 저장.
        _head = (_head + 1) % _capacity; // 원형 큐이므로 인덱스를 순환시킴.
        _count--;
        return item;// 큐에서 제거된 요소 반환.
    }

    public T Peek()
    {
        if(IsEmpty)
        {
            Console.WriteLine("큐가 비어있습니다.");
            return default(T); // 큐가 비어있을 때 기본값 반환. 
        }
        return _items[_head];// 큐의 시작 인덱스에 있는 요소를 반환.
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[(_head + i) % _capacity];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


}

