using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SelectionMetric : MonoBehaviour
{
    [Inject] FieldManager _FieldManager;
    [SerializeField] Text text;

    public void Initialize(bool to)
    {
        if (to)
            _FieldManager.onSelect += UpdateText;
        else
            _FieldManager.onSelect -= UpdateText;
    }

    public void UpdateText()
    {
        int count = _FieldManager.Selected.Count;
        text.text = $" {_FieldManager.Selected.Count} item" + ((count > 1) ? "s " : " ") + "selected";
    }


}
