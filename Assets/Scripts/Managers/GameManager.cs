using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Asyncoroutine;
using Zenject;
using System.Threading.Tasks;
using DateTime = System.DateTime;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [Inject] InputManager _InputManager;
    [Inject] UImanager _UImanager;
    [Inject] FieldManager _FieldManager; 
    [Inject] Prefab_Refs _Prefab_Refs;
    [Inject] SerializationManager _SerializationManager;

    public System.Action onUpdate;
    public string serverAdress = "http://www.microsoft.com";
    public int timeoutSec = 5;
     
    void Start()
    {
        //Room for manuevers: perhaps scene-based initialization, etc
        _InputManager.Initialize(true);
        _FieldManager.Initialize(true);
        _UImanager.Initialize(true);
        _SerializationManager.Initialize();

        PingTime();
    }

    public async void PingTime()
    {
        string timeText = "";
#if UNITY_WEBGL
        StartCoroutine(LoadPageCoroutine());

        //virgin Coroutine
        IEnumerator LoadPageCoroutine()
        {
            float timer = 0;
            var www =  UnityWebRequest.Get(serverAdress);
            yield return www.SendWebRequest();
            while (!www.isDone | www.error != null)
            {
                timer += Time.deltaTime;
                if (timer > timeoutSec)
                    break;
                yield return null;
            }
            timeText = www?.GetResponseHeader("Date"); 
            yield return null;
        }
#else
        CancellationTokenSource s_cts = new CancellationTokenSource();
        CancellationToken c_token = s_cts.Token;
        timeText = await LoadPageAsync();

        //Chad async
        async Task<string> LoadPageAsync()
        {
            UnityWebRequest www = null; 
            try
            {
                s_cts.CancelAfter(timeoutSec*1000);

                www = await UnityWebRequest.Get(serverAdress);
                await www.SendWebRequest();
            }
            catch (TaskCanceledException)
            {
                Debug.LogError($"Timer error: requset timeout");
            } 
            return www?.GetResponseHeader("Date");
        } 

#endif

        if(!timeText.Equals(string.Empty))
        {
            _UImanager.ShowCurrentTime(timeText);
        }  


        
    }
     
    void Update()
    {
        onUpdate.Invoke();
    }
}
