﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Action = System.Action;

public class InputManager : MonoBehaviour
{
    [Inject] GameManager _GameManager;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GraphicRaycaster graphicRaycaster;

    public Vector3 mousePosition => _mousePosition;
    Vector3 _mousePosition;
    public Vector3 mouseMovement => _mouseMovement;
    Vector3 _mouseMovement;

    //Left Click
    public Action onClick0BeginUI;
    public System.Action<GameObject> onClick0BeginHit;
    public Action onClick0BeginMiss;

    //Right Click Hold
    public Action onHold0; 

    //Shift
    public bool shiftActive;

    public bool overUI => _overUI;
    [SerializeField] bool _overUI;

    public void Initialize(bool toState)
    {
        if (toState)
        {
            _GameManager.onUpdate += CheckForInput;
        }
        else
        {
            _GameManager.onUpdate -= CheckForInput;
        }

        if (_eventSystem == null)
            _eventSystem = FindObjectOfType<EventSystem>();

#if UNITY_EDITOR
        onClick0BeginUI += () => Debug.Log("UI");
        onClick0BeginHit += (GameObject obj) => Debug.Log($"Hit: {obj.name}");
        onClick0BeginMiss += () => Debug.Log("Miss");
#endif
    }

    public void CheckForInput()
    {
        CalculatePositions();
         
        shiftActive = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        _overUI = false;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        PointerEventData _PEData = new PointerEventData(_eventSystem);
        _PEData.position = _mousePosition;
        graphicRaycaster.Raycast(_PEData, raycastResults);
        if (raycastResults.Count > 0)
        {
            bool anyUIcatcher = false;
            if (raycastResults != null)
            {
                for (int i = 0; i < raycastResults.Count; i++)
                { 
                    if (raycastResults[i].gameObject.layer != (int)LayerManager.Layers.UI_Ignore)
                    {
                        anyUIcatcher = true;
                        break;
                    }
                }
            }
            _overUI = anyUIcatcher;
        }

        //Unneded - now syste is based on UI

        //Click left
        ClickMouseInput2D(Input.GetMouseButtonDown, 0, onClick0BeginUI, onClick0BeginHit, onClick0BeginMiss);

        void ClickMouseInput2D(System.Func<int, bool> inputcheck, int button, Action onUI, System.Action<GameObject> onObj, Action onMiss)
        {
            if (inputcheck(button))
            {
                if (overUI)
                {
                    onUI?.Invoke();
                }
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward);
                    if (hit.transform != null)
                    {
                        onObj?.Invoke(hit.transform.gameObject);
                    }
                    else
                    {
                        onMiss?.Invoke();
                    }
                }
            }
        }

        void HoldMouseInput(int button, Action onHold)
        {
            if (Input.GetMouseButton(button))
                onHold?.Invoke();
        }

    }

    void CalculatePositions()
    {
        _mouseMovement = Input.mousePosition - _mousePosition;
        _mousePosition = Input.mousePosition; 
        //_mouseMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
    }
}
