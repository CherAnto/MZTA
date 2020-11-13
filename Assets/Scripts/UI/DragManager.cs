using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DragManager : MonoBehaviour
{
    [Inject] InputManager _InputManager;
    [Inject] FieldManager _FieldManager;
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

    public Vector3 GetCenteredToolbarPosition()
    {
        return _toolbar.GetCenterPosition();
    }

    public Vector3 GetToolbarSize()
    {
        return _toolbar.GetSize();
    } 

    public void StartDrag(Image caller)
    {
        //размер? 
        _currentCaller = caller;
        caller.color = caller.color.ChangeA(0);
        _toolbar.gameObject.SetActive(true);
        _toolbar.ChangeSprite(caller.sprite);
        _toolbar.ChangeSize(NormalizeSize(caller));

        Vector2 NormalizeSize(Image image)
        {
            RectTransform iRect = image.GetComponent<RectTransform>();
            if(iRect.rect.width - image.sprite.rect.width > iRect.rect.height - image.sprite.rect.height)
            {
                float coeff = iRect.rect.height / image.sprite.rect.height;
                return new Vector2(image.sprite.rect.width * coeff, iRect.rect.height);
            }
            else
            {
                float coeff = iRect.rect.width / image.sprite.rect.width;
                return new Vector2(iRect.rect.width, image.sprite.rect.height * coeff);
            } 
        }
    }

    public void Drag()
    {

        _toolbar.SetPosition(_InputManager.mousePosition);
    }
     
    public void FinishDrag()
    {
        _currentCaller.color = _currentCaller.color.ChangeA(1);
        _toolbar.gameObject.SetActive(false);
            if(!_InputManager.overUI)
        _FieldManager.CreateFieldItem(_currentCaller.sprite);
        _currentCaller = null;
    } 
}
