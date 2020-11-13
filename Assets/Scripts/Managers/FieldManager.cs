using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static SerializationManager;

public class FieldManager : MonoBehaviour
{
    [Inject] Prefab_Refs _PF_refs;
    [Inject] UImanager _UImanager;
    [Inject] InputManager _InputManager; 
    [Inject] DiContainer _Container;
    [Inject] Pool<FieldItem> _itemPool;
    [Inject] SerializationManager _SerializationManager;

    public List<IFieldable> Selected => new List<IFieldable>(selected);
    [SerializeField] List<IFieldable> selected = new List<IFieldable>();
    public List<IFieldable> AllFieldables => new List<IFieldable>(all);
    [SerializeField] List<IFieldable> all = new List<IFieldable>();

    public System.Action onSelect;

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

    public void ProcessSelection(IFieldable toSelect)
    { 
        Select(toSelect, !selected.Contains(toSelect), _InputManager.shiftActive);
        onSelect?.Invoke();
    } 

    public void Load(List<JSONfieldObject> objs)
    {
        ClearField();
        for (int i = 0; i < objs.Count; i++)
        {
            JSONfieldObject current = objs[i];
            Sprite sprite;
            _SerializationManager.allSprites.TryGetValue(current.spriteName, out sprite);
            if (sprite == null) {
                Debug.LogError($"Unknown sprite: {current.spriteName}");
                continue; 
            }
            CreateFieldItem(sprite, current.scale, current.position, current.pixelSize, current.color);
        }
    }

    void Select(IFieldable toSelect, bool state, bool additive)
    {
        //Clear by click on empty space
        if (toSelect == null)
        {
            DeselectAll();
        }
        else
        {
            if (additive)
            {
                if (state)
                    ConsumateSelection();
                else
                    ConsumateSelection();
            }
            else
            {
                if (selected.Count > 0 || !state)
                    DeselectAll();
                if (state)
                    ConsumateSelection();


            }
        }

        void ConsumateSelection()
        {
            if(state)
                selected.Add(toSelect);
            else
                selected.Remove(toSelect);
            toSelect.ShowSelection(state);
        }
    }

    public void DeselectAll()
    {
        for (int i = 0; i < selected.Count; i++)
        {
            selected[i].ShowSelection(false);
        }
        selected = new List<IFieldable>();
    }

    public void DeleteSelected()
    {
        for (int i = selected.Count-1; i >= 0; i--)
        {
            Destroy(selected[i]);
        }
        selected = new List<IFieldable>();
    }

    public void ClearField()
    {
        for (int i = all.Count - 1; i > -1; i--)
        {
            Destroy(all[i]);
        }
        all = new List<IFieldable>();
        selected = new List<IFieldable>();
    }

    public void ChangeColor(Color c)
    {
        for (int i = 0; i < selected.Count; i++)
        {
            selected[i].SetColor(c);
        }
    }

    public void ChangeScale(float value)
    {
        for (int i = 0; i < selected.Count; i++)
        {
            selected[i].SetScale(Vector3.one*value);
        }
    } 

    public void CreateFieldItem(Sprite sprite)
    {  
        CreateFieldItem(sprite, Vector3.one, _UImanager.dragManager.GetCenteredToolbarPosition(), _UImanager.dragManager.GetToolbarSize(), Color.white); 
    }

    public IFieldable CreateFieldItem(Sprite sprite, Vector3 scale, Vector3 position, Vector3 pixelSize, Color color)
    {
        IFieldable fieldable = _itemPool.Get();
        _Container.Inject(fieldable);
        fieldable.Initialize();
        fieldable.SetParent(_UImanager.fieldPanel);
        fieldable.SetScale(scale);
        fieldable.SetPosition(position);
        fieldable.SetSprite(sprite);
        fieldable.SetPixelSize(pixelSize);
        fieldable.SetColor(Color.white);
        all.Add(fieldable);
        return fieldable;
    }

    public void Destroy(IFieldable item)
    {
        item.ShowSelection(false);
        _itemPool.Remove((FieldItem)item); //:( TODO TODO TODO!!!
    }

    void CalculateFieldCoords() {
        (Vector2,Vector2) sizes = _UImanager.CalculateFieldSize(); 
    }

}
