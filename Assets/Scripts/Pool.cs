using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;  

public interface Ipoolable:IGameObject
{    
    void Initialize();
    void Activate();
    void Deactivate();
}

[System.Serializable]
public class Pool<T> where T : Ipoolable
{
    List<T> _disabled, _enabled;
    GameObject _PreFab;
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

    public void Initialize(GameObject PF, int baseL, DiContainer container)
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
        if(_disabled.Count>0)
            output = _disabled[0];
        else
            output = InstantiateNew();

        //for (int i = 0; i < _disabled.Count; i++)
        //{
        //    output = _disabled[i];
        //    _disabled[i].Activate();
        //    return output;
        //}
        //output = InstantiateNew();
        output.Activate();
        MoveToPool(true, output);
        return output;
    }

    public void Remove(T item)
    {
        item.Deactivate();
        MoveToPool(false, item);
    }

    T InstantiateNew()
    {
        T local;
        GameObject localGO;
        try
        {
            localGO = GameObject.Instantiate(_PreFab);
            local = localGO.GetComponent<T>();
            _DIContainer.Inject(localGO);
            MoveToPool(false, local);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Pool can't be initialized: {e.Message}");
            //No point in proceeding any further
            throw e;
        }
        return local;
    } 

    void MoveToPool(bool state, T obj)
    {
        obj.GetGameobject.SetActive(state);
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
