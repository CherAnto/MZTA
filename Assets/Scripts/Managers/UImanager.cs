using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{

    [SerializeField] RectTransform _fieldSizePanel;
    [SerializeField] DragManager _DragManager;

    public void Initialize(bool to)
    {
        _DragManager.Initialize();
    }

    public (Vector2,Vector2) CalculateFieldSize()
    {
        if (_fieldSizePanel == null)
        {
            Debug.LogError("Unknown field size: fieldSizeObj i null!");
            return (Vector2.zero, Vector2.zero);
        }
        if (_fieldSizePanel.gameObject.activeSelf)
            _fieldSizePanel.gameObject.SetActive(false);
        return (new Vector2(_fieldSizePanel.anchorMin.x * Screen.width, _fieldSizePanel.anchorMin.y * Screen.height)
            , new Vector2(_fieldSizePanel.anchorMax.x * Screen.width, _fieldSizePanel.anchorMax.y * Screen.height)); 
    }

    
}
