using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISelectable
{
    void Select();
    void Select(BaseEventData eData);
    void ShowSelection(bool state);
}
