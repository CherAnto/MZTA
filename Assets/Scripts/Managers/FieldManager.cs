using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FieldManager : MonoBehaviour
{
    [Inject] Prefab_Refs _PF_refs;
    [Inject] UImanager _UImanager;


    [SerializeField] List<Imovable> selected = new List<Imovable>();
    [SerializeField] List<Imovable> all = new List<Imovable>();

    public void Initialize(bool to)
    {
        if (to)
        {
            CalculateFieldCoords();
        } 
    }

    public void Move(Vector3 moveBy)
    {
        if (moveBy == null) return;
        for (int i = 0; i < selected.Count; i++)
        {
            selected[i].Move(moveBy);
        }
    }

    public void CreateFieldItem(Vector3 position)
    {
        Imovable movable = Instantiate(_PF_refs.ItemPF).GetComponent<Imovable>();
        movable.SetPosition(position);
    }

    void CalculateFieldCoords() {
        (Vector2,Vector2) sizes = _UImanager.CalculateFieldSize();
        Debug.LogError(sizes.Item1);
        Debug.LogError(sizes.Item2);
    }

}
