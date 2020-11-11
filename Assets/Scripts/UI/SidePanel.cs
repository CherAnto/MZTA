using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidePanel : MonoBehaviour
{

    public void SpawnSprite(GameObject callingButton)
    {
        if (callingButton == null) return;
        Image bImage = callingButton.GetComponent<Image>();
        //sanity check - move a getComponent level down if prefab is changed
        if (bImage == null)
        {
            bImage = callingButton.GetComponentInChildren<Image>();
            if (bImage == null)
            {
                Debug.LogError($"NO IMAGE PRESENT ON {callingButton.name}");
                return;
            }
        }
        Sprite newSprite = bImage.sprite;

    }

}
