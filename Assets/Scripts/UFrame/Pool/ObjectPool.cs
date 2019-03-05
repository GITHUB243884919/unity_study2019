using System.Collections.Generic;

public interface IObjectPoolable
{
    void OnDeathToPool();
}

public class ObjectPool<T> where T : class, IObjectPoolable, new()
{
    protected Queue<IObjectPoolable> mObjPools = new Queue<IObjectPoolable>();

    public T New()
    {
        T ret = null;
        if (mObjPools.Count == 0)
        {
            ret = new T();
        }
        else
        {
            ret = mObjPools.Dequeue() as T;
        }
        return ret;
    }

    public void Delete(T obj)
    {
        mObjPools.Enqueue(obj);
        obj.OnDeathToPool();
    }

    public void RecoveryAll()
    {
        mObjPools.Clear();
    }
}
