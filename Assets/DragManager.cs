using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DragManager : MonoBehaviour
{
    [Inject] InputManager _InputManager;
    [SerializeField] ToolBar _toolbar;
    public Image currentCaller => _currentCaller;
    Image _currentCaller;

    public void Initialize()
    {
        if (_toolbar == null)
            Debug.LogError("Toolbar is missing!");
        _toolbar.Initialize();
        _toolbar.gameObject.SetActive(false);
    }

    public void StartDrag(Image caller)
    {
        //размер? 
        _currentCaller = caller;
        caller.color = caller.color.ChangeA(0);
        _toolbar.gameObject.SetActive(true);
        _toolbar.ChangeSprite(caller.sprite);
    }

    public void Drag()
    {

        _toolbar.SetPosition(_InputManager.mousePosition);
    }
     
    public void FinishDrag()
    {
        _currentCaller.color = _currentCaller.color.ChangeA(1);
    }

    public void Select()
    {
        Debug.LogError("AAAAAAAAAAAA");
    }

    public void Click()
    {
        Debug.LogError("SSSSSSSSSSSSS");
    }
}
