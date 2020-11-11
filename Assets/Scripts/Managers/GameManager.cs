using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] InputManager _InputManager;
    [Inject] UImanager _UImanager;
    [Inject] FieldManager _FieldManager;

    public System.Action onUpdate;
     
    void Start()
    {
        //Room for manuevers: perhaps scene-based initialization, etc
        _InputManager.Initialize(true);
        _FieldManager.Initialize(true);
        _UImanager.Initialize(true);
    }
     
    void Update()
    {
        onUpdate.Invoke();
    }
}
