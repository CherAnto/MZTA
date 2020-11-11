using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class MainMenu : MonoBehaviour
{
    [Inject] Prefab_Refs ui_Refs;
     

    public void Activate(bool to)
    {
        this.gameObject.SetActive(to);
    }

    public void Exit()
    {
         
        //exit depending on platform
        //check if saved too
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }

    public void InitiateSaving()
    {
        SaveFileDialog save = new SaveFileDialog();
        save.ShowDialog();
    }

    public void InitiateLoading()
    {
        OpenFileDialog open = new OpenFileDialog();
        open.ShowDialog();
    }

}
