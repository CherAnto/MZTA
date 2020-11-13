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

    public Vector3 GetCenterPosition()
    {
        return new Vector2(transf.anchoredPosition.x + transf.rect.width / 2, transf.anchoredPosition.y - transf.rect.height / 2);
        //transf.anchoredPosition - new Vector2(transf.rect.width / 2, transf.rect.height / 2); 
    }

    public Vector2 GetSize()
    {
        return new Vector2(transf.rect.width, transf.rect.height);
    } 
    
    public void ChangeSprite(Sprite newSprite)
    {
        image.sprite = newSprite;
    }

    public void ChangeSize(Vector2 newSize)
    {
        transf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,newSize.x);
        transf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,newSize.y); 
    }

    public void SetPosition(Vector2 newPos)
    {
        transf.anchoredPosition = newPos;
    }

}
 