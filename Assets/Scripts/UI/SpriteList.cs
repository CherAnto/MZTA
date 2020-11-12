using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpriteList : MonoBehaviour
{

    [Inject] FieldManager _FieldManager;
    [Inject] InputManager _InputManager;

    void Start()
    {
        //List<GameObject> brokenButtons = new List<GameObject>();
        //int childC = transform.childCount;
        //Button current;
        //for (int i = 0; i < childC; i++)
        //{
        //    current = transform.GetChild(i).GetComponent<Button>();
        //    if (current == null) {
        //        current = GetComponentInChildren<Button>();
        //        if (current == null)
        //        {
        //            MarkObjForRemoval($"Button not found: {current.name}"); 
        //            continue;
        //        }
        //    }
        //    Image currentI = current.GetComponent<Image>();
        //    if(currentI == null)
        //    {
        //        currentI = GetComponentInChildren<Image>();
        //        if (currentI == null)
        //        {
        //            MarkObjForRemoval($"Button Image not found: {current.name}"); 
        //            continue;
        //        }
        //    }
        //    if(currentI.sprite == null) 
        //        {
        //            MarkObjForRemoval($"Button Image not found: {current.name}"); 
        //            continue;
        //        }

        //    current.onClick.AddListener(delegate{ SpawnSprite(currentI.sprite); });
        //}

        //for (int i = brokenButtons.Count-1; i > -1; i++)
        //{
        //    GameObject.Destroy(brokenButtons[i]);
        //}

        //void MarkObjForRemoval(string errText){
        //    Debug.LogError(errText);
        //    brokenButtons.Add(current.gameObject); 
        //}
    } 

}
