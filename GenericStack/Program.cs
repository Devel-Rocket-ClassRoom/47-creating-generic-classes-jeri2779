using System;

// README.md를 읽고 아래에 코드를 작성하세요.

class SimpleQueue<T>
{
    private T[] items;
    private int head;
    private int tail;
    private int capacity;
    
    public SimpleQueue(int capacity)
    {
        this.capacity = capacity;
        items = new T[capacity];
        head = 0;
        tail = 0;
    }
    public void Enqueue(T item)
    {
        if ((tail + 1) % capacity == head)
            throw new InvalidOperationException("큐가 가득 찼습니다.");
        
        items[tail] = item;
        tail = (tail + 1) % capacity;
    }
    public T Dequeue()
    {
        if (head == tail)
            throw new InvalidOperationException("큐가 비어있습니다.");
        
        T item = items[head];
        head = (head + 1) % capacity;
        return default(T);
    }
    public T Peek()
    {
        if (head == tail)
            throw new InvalidOperationException("큐가 비어있습니다.");
        
        return default(T);
    }
    public bool IsEmpty()
    {
        return head == tail;
    }
    public bool IsFull()
    {
        return (tail + 1) % capacity == head;
    }

}

