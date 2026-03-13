namespace object_pool_lazy;

public class ObjectPool<T> where T : class
{
    private readonly Queue<T> _availableObjects = new Queue<T>();
    private readonly Func<T> _objectGenerator;

    public ObjectPool(Func<T> objectGenerator, int initialCount)
    {
        _objectGenerator = objectGenerator;

        for (int i = 0; i < initialCount; i++)
        {
            _availableObjects.Enqueue(_objectGenerator());
        }
    }

    public T GetObject()
    {
        if (_availableObjects.Count > 0)
        {
            return _availableObjects.Dequeue();
        }
        return _objectGenerator();
    }

    public void ReturnObject(T obj)
    {
        _availableObjects.Enqueue(obj);
    }
}