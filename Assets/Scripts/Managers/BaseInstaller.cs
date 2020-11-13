using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseInstaller : MonoInstaller
{
    [SerializeField] Prefab_Refs ui_refs;
    [SerializeField] GameManager _gameManager;
    [SerializeField] InputManager _inputManager;
    [SerializeField] FieldManager _fieldManager;
    [SerializeField] UImanager _uimanager;
    [SerializeField] SerializationManager _SerializationManager;
    [SerializeField] WebManager _WebManager;

    public override void InstallBindings()
    {
        Container.Bind<Prefab_Refs>().FromInstance(ui_refs).AsSingle().NonLazy();

        Pool<FieldItem> pool = new Pool<FieldItem>(); 
        pool.Initialize(ui_refs.ItemPF.GetComponent<FieldItem>(),0, Container);

        Container.Bind<Pool<FieldItem>>().FromInstance(pool).AsSingle().NonLazy();


        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle().NonLazy();
        Container.Bind<FieldManager>().FromInstance(_fieldManager).AsSingle().NonLazy();
        Container.Bind<UImanager>().FromInstance(_uimanager).AsSingle().NonLazy();
        Container.Bind<SerializationManager>().FromInstance(_SerializationManager).AsSingle().NonLazy();
        Container.Bind<WebManager>().FromInstance(_WebManager).AsSingle().NonLazy();


    } 
}
