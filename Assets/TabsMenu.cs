using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabsMenu : MonoBehaviour
{

    [SerializeField] List<SerializedKeyValuePair<Button, GameObject>> tabs = new List<SerializedKeyValuePair<Button, GameObject>>();
    SerializedKeyValuePair<Button, GameObject> openTab = null;
    [SerializeField] Color focusColor;
    [SerializeField] Color normalColor;
    [SerializeField] bool DisableAllOnStart; 

    // Start is called before the first frame update
    void Start()
    {
        //int index;
        for (int i = 0; i < tabs.Count; i++)
        {
            //TODO: add null checcking!
            if(tabs[i] == null)
            {
                Debug.LogError($"Tabs creation error: NULL in tab {i}");
                continue;
            }
            else if (tabs[i].key == null || tabs[i].value == null)
            {
                Debug.LogError($"Tabs creation error: NULL in tab {i} with {tabs[i].key} and {tabs[i].value}");
                Destroy(tabs[i].key);
                Destroy(tabs[i].value);
            }
            int index = i;
            tabs[i].key.onClick.AddListener(delegate { OpenTab(index); }); 
        }
        if(DisableAllOnStart)
            CloseAll();
    } 

    public void OpenTab(int n)
    { 
        //(in)sanity check
        if (n < 0 || n >= tabs.Count)
        {
            Debug.LogError($"Tab error {gameObject.name} : tab number {n} is below/over limit: {tabs.Count} ");
            return;
        }
        if (openTab != null)
            EnableTab(openTab, false);
        EnableTab(tabs[n], true);
    }

    void EnableTab(SerializedKeyValuePair<Button, GameObject> tab, bool state)
    {
        tab.key.GetComponent<Image>().color = (state) ? focusColor: normalColor;
        tab.value.SetActive(state);
        openTab = tab;
    }

    public void CloseAll()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            EnableTab(tabs[i], false);
        }
    }

    //костыли-костылики
    [System.Serializable]
    public class SerializedKeyValuePair<K, V>
    { 
        public K key;
        public V value;
    }

}
