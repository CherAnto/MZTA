using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Action = System.Action;

public class InputManager : MonoBehaviour
{
    [Inject] GameManager _GameManager;
    EventSystem _eventSystem;

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

    public bool overUI;

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
        onClick0BeginUI += () => Debug.LogError("UI");
        onClick0BeginHit += (GameObject obj) => Debug.LogError($"Hit: {obj.name}");
        onClick0BeginMiss += () => Debug.LogError("Miss");
#endif
    }

    public void CheckForInput()
    {
        CalculatePositions();
         
        shiftActive = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));  

        overUI = _eventSystem.IsPointerOverGameObject();

        //Unneded - now syste is based on UI

        //Click left
        //ClickMouseInput2D(Input.GetMouseButtonDown, 0, onClick0BeginUI, onClick0BeginHit, onClick0BeginMiss); 

        //void ClickMouseInput2D(System.Func<int, bool> inputcheck, int button, Action onUI, System.Action<GameObject> onObj, Action onMiss)
        //{
        //    if (inputcheck(button))
        //    {
        //        if (overUI)
        //        {
        //            onUI?.Invoke();
        //        }
        //        else
        //        {
        //            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward);
        //            if (hit.transform != null)
        //            {
        //                onObj?.Invoke(hit.transform.gameObject);
        //            }
        //            else
        //            {
        //                onMiss?.Invoke();
        //            }
        //        }
        //    }
        //}

        //void HoldMouseInput(int button, Action onHold)
        //{
        //    if (Input.GetMouseButton(button))
        //        onHold?.Invoke();
        //}

    }

    void CalculatePositions()
    {
        _mouseMovement = Input.mousePosition - _mousePosition;
        _mousePosition = Input.mousePosition; 
        //_mouseMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
    }
}
