﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Button = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour
{ 
    [Inject] SerializationManager _SerializationManager;
    [SerializeField] Button[] SavingButtons;

    private void Awake()
    {
#if UNITY_WEBGL
        for (int i = 0; i < SavingButtons.Length; i++)
        {
            SavingButtons[i].gameObject.SetActive(false);
        }
#endif
    }

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
        _SerializationManager.CreateJSON();

        SaveFileDialog save = new SaveFileDialog();
        save.ShowDialog();
        //save.
    }

    public void InitiateLoading()
    {
        OpenFileDialog open = new OpenFileDialog();
        open.ShowDialog();
    }

}
