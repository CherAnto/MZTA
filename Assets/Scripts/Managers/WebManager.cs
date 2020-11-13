using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DateTime = System.DateTime;
using System.Threading;
using Asyncoroutine;
using UnityEngine.Networking;
using Zenject;

public class WebManager : MonoBehaviour
{
    [Inject] UImanager _UImanager;

    public string serverAdress = "http://www.microsoft.com";
    public int timeoutSec = 5;

    public void Initialize()
    {
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
            var www = UnityWebRequest.Get(serverAdress);
            www.SetRequestHeader("Access-Control-Allow-Credentials", "true");
            www.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
            www.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            www.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return www.SendWebRequest();
            //while (!www.isDone | www.error != null)
            //{
            //    timer += Time.deltaTime;
            //    if (timer > timeoutSec)
            //        break;
            //    yield return null;
            //}
            timeText = www?.GetResponseHeader("Date");
            ShowText();
            yield return null;
        }
#else
        CancellationTokenSource s_cts = new CancellationTokenSource();
        CancellationToken c_token = s_cts.Token;
        timeText = await LoadPageAsync();
        ShowText();

        //Chad async
        async Task<string> LoadPageAsync()
        {
            UnityWebRequest www = null;
            try
            {
                s_cts.CancelAfter(timeoutSec * 1000);

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

        void ShowText()
        {
            _UImanager.ShowCurrentTime(timeText.Equals(string.Empty) ? "Error" : timeText); 
            if (!timeText.Equals(string.Empty))
            {
                _UImanager.ShowCurrentTime(timeText);
            }
        }

        



    }
}
