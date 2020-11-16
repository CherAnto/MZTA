using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisual2DUI
{
    Sprite sprite { get; }
    Color color { get; }
    Vector2 pixelSize { get; }
    void SetSprite(Sprite sprite);
    void SetColor(Color color);
    void SetPixelSize(Vector2 size);
}
