using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IFieldable
{
     

    Sprite sprite {get; }
    Vector3 scale {get; }
    Vector3 position {get; }
    Vector2 pixelSize {get; } 
    Color color {get; } 

    void Initialize(); 
    void Select();
    void Select(BaseEventData eData);
    void ShowSelection(bool state);
    void Move(Vector3 moveBy);

    void SetPosition(Vector3 position);
    void SetParent(Transform transform);
    void SetScale(Vector3 scale);
    void SetSprite(Sprite sprite);
    void SetPixelSize(Vector2 size);
    void SetColor(Color color);

}
