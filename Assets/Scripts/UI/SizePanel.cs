using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizePanel : MonoBehaviour
{

    [SerializeField] Text valueText; 

    private void OnEnable()
    {
        //TODO: calculate current scale
    } 

    public void ShowScale(float value)
    {
        valueText.text = value.ToString();
    }
     
}
