namespace object_pool_lazy;

public class ObjectPool<T> where T : class
{
    private readonly Queue<T> _availableObjects = new Queue<T>();
    private readonly Func<T> _objectGenerator;

    private readonly int _maxSize;
    private int _currentCount;

    public ObjectPool(Func<T> objectGenerator, int initialCount, int maxSize)
    {
        _objectGenerator = objectGenerator;
        _maxSize = maxSize;

        for (int i = 0; i < initialCount; i++)
        {
            _availableObjects.Enqueue(_objectGenerator());
            _currentCount++;
        }
    }

    public T GetObject()
    {
        if (_availableObjects.Count > 0)
        {
            return _availableObjects.Dequeue();
        }
        
        _currentCount++;
        return _objectGenerator();
    }

    public void ReturnObject(T obj)
    {
        if (_availableObjects.Count < _maxSize)
        {
            _availableObjects.Enqueue(obj);
        }
        else
        {
            _currentCount--;
            Console.WriteLine("Pool full → object discarded");
        }
    }
}