using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class FieldItem : PoolableObj, IFieldable
{
    [Inject] FieldManager _FieldManager;
    [Inject] InputManager _InputManager;
    [Inject] Pool<FieldItem> localPool;

    [SerializeField] EventTrigger _evtrigger;
    RectTransform transform;
    Image image;
    ParticleSystem partS;

    bool initialized;

    public Sprite sprite => image.sprite;

    public Vector3 scale => transform.localScale;

    public Vector3 position => transform.anchoredPosition;

    public Vector2 pixelSize => new Vector2(transform.rect.width, transform.rect.height);

    public Color color => image.color;

    public void Move(Vector3 moveBy)
    {
        transform.anchoredPosition += (Vector2) moveBy;
    }

    public void Move()
    { 
        transform.anchoredPosition += (Vector2)_InputManager.mouseMovement;
    }

    public void SetPosition(Vector3 position)
    {
        transform.anchoredPosition = position;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void SetPixelSize(Vector2 size)
    {
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }

    public void SetParent(Transform transform)
    {
        this.transform.SetParent(transform);
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void Initialize()
    {
        if (initialized) return;
        _evtrigger = GetComponent<EventTrigger>();
        transform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        InitializeParticleSystem();


        AddToEvent(EventTriggerType.PointerClick, new UnityAction<BaseEventData>(Select));
        AddToEvent(EventTriggerType.Drag, delegate { _FieldManager.Move(_InputManager.mouseMovement); }/* new UnityAction<BaseEventData>(Move)*/);

        initialized = true;

        void InitializeParticleSystem()
        {
            partS = GetComponent<ParticleSystem>();
            Vector3 center = transform.anchoredPosition.FromScreenToWorldCoords();
            Vector3 edge = (transform.anchoredPosition + new Vector2(transform.rect.width / 2, transform.rect.height / 2)).FromScreenToWorldCoords();

            Vector2 diff = edge - center;

            //Love the getter modules :/
            //radius
            ParticleSystem.ShapeModule shapeModule = partS.shape;
            shapeModule.radius = (diff.x > diff.y) ?diff.x:diff.y;

            //lifetime + speed
            ParticleSystem.MainModule mainModule = partS.main;
            mainModule.startSpeed = shapeModule.radius / 2 / partS.main.duration;

            ShowSelection(false);
        } 
    } 

    public void AddToEvent(EventTriggerType trigType, UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = trigType; 
        entry.callback.AddListener((eventData) => { action(eventData); });
        _evtrigger.triggers.Add(entry);  
    }

    public void Select(BaseEventData eData)
    {
        _FieldManager.ProcessSelection(this);
    }

    public void Select()
    {
        //Relies on event system, for now NI
        _FieldManager.ProcessSelection(this);
    }

    public void ShowSelection(bool state)
    {
        ParticleSystem.EmissionModule emission = partS.emission;
        emission.enabled = state;
        if (!state)
            partS.Clear();
    } 
}
