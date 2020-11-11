using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldItem : MonoBehaviour, Imovable
{
    [SerializeField] EventTrigger _evtrigger;

    public void Move(Vector3 moveBy)
    {
        transform.position += moveBy;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    } 

    public void Initialize()
    {
        _evtrigger.OnPointerClick
    }

}
