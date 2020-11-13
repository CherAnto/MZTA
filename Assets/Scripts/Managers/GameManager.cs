using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Asyncoroutine;
using Zenject;


public class GameManager : MonoBehaviour
{
    [Inject] InputManager _InputManager;
    [Inject] UImanager _UImanager;
    [Inject] FieldManager _FieldManager; 
    [Inject] Prefab_Refs _Prefab_Refs;
    [Inject] SerializationManager _SerializationManager;
    [Inject] WebManager _WebManager;

    public System.Action onUpdate; 
     
    void Start()
    {
        //Room for manuevers: perhaps scene-based initialization, etc
        _InputManager.Initialize(true);
        _FieldManager.Initialize(true);
        _UImanager.Initialize(true);
        _SerializationManager.Initialize();
        _WebManager.Initialize(); 
    }

    
     
    void Update()
    {
        onUpdate.Invoke();
    }
}
