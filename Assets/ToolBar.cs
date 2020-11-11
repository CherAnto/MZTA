using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
    Image image;
    RectTransform transf;

    public void Initialize()
    {
        image = GetComponent<Image>();
        if (image == null) Debug.LogError($"Image not set on {gameObject.name}");
        transf = GetComponent<RectTransform>();
        if (transf == null) Debug.LogError($"Toolbar is not a UI object: {gameObject.name}");
    }
    
    public void ChangeSprite(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    public void SetPosition(Vector2 newPos)
    {
        transf.anchoredPosition = newPos;
    }

}
 