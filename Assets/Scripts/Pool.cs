using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PoolableObj : MonoBehaviour
{
    public event System.Action<PoolableObj> onActive;
    public event System.Action<PoolableObj> onDisable; 

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        onActive.Invoke(this);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        onDisable.Invoke(this);
    }
}

public interface Ipoolable
{
    System.Action<PoolableObj> onActive { get; set; }
    System.Action<PoolableObj> onDisable { get; set; }

    Ipoolable Create();
    void Initialize();
    void Activate();
    void Deactivate();
}

[System.Serializable]
public class Pool<T> where T : PoolableObj
{
    List<T> _disabled, _enabled;
    T _PreFab;
    [Inject] DiContainer _DIContainer;

    public int Length => _disabled.Count + _enabled.Count;

    //TODO: add delayed spawning, circle injection otherwise!
    // Start is called before the first frame update
    //public Pool(T PF, int baseL)
    //{
    //    Initialize(PF, baseL);
    //}

    [Inject]
    public void Injection(DiContainer cont)
    {
        this._DIContainer = cont;
    }

    public Pool()
    {

    }

    public void Initialize(T PF, int baseL, DiContainer container)
    {
        _DIContainer = container;
        _disabled = new List<T>();
        _enabled = new List<T>();
        _PreFab = PF;
        T local;
        for (int i = 0; i < baseL; i++)
        {
            local = InstantiateNew();
        }
    }

    public T Get()
    {
        T output;
        for (int i = 0; i < _disabled.Count; i++)
        {
            output = _disabled[i];
            _disabled[i].Activate();
            return output;
        }
        output = InstantiateNew();
        output.Activate();
        return output;
    }

    public void Remove(T item)
    {
        item.Deactivate(); 
    }

    T InstantiateNew()
    {
        T local = GameObject.Instantiate(_PreFab);
        _DIContainer.Inject(local); 
        local.onActive += OnEnable;
        local.onDisable += OnDisable;
        local.Deactivate();
        return local;
    }

    void OnEnable(PoolableObj obj)
    {
        _enabled.Add((T)obj);
        _disabled.Remove((T)obj);
    }

    void OnDisable(PoolableObj obj)
    {
        _disabled.Add((T)obj);
        _enabled.Remove((T)obj);
    }

    void MoveToPool(bool state, T obj)
    {
        if (state)
        {
            _enabled.Add(obj);
            _disabled.Remove(obj);
        }
        else
        {
            _disabled.Add(obj);
            _enabled.Remove(obj);
        }
    }
}
