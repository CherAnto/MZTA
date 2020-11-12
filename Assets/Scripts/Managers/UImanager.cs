using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public RectTransform fieldPanel => _fieldPanel;
    [SerializeField] RectTransform _fieldPanel;
    public DragManager dragManager => _DragManager;
    [SerializeField] DragManager _DragManager;

    [SerializeField] SelectionMetric metric;

    public void Initialize(bool to)
    {
        _DragManager.Initialize();
        metric.Initialize(to);
    }

    public (Vector2,Vector2) CalculateFieldSize()
    {
        if (_fieldPanel == null)
        {
            Debug.LogError("Unknown field size: fieldSizeObj i null!");
            return (Vector2.zero, Vector2.zero);
        }
        if (_fieldPanel.gameObject.activeSelf)
            _fieldPanel.gameObject.GetComponent<Image>().enabled = false;
        return (new Vector2(_fieldPanel.anchorMin.x * Screen.width, _fieldPanel.anchorMin.y * Screen.height)
            , new Vector2(_fieldPanel.anchorMax.x * Screen.width, _fieldPanel.anchorMax.y * Screen.height)); 
    }

    
}
