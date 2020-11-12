using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class SerializationManager : MonoBehaviour
{

    [Inject] FieldManager _FieldManager;

    public string CreateJSON()
    {
        JSONarray<JSONfieldObject> arrayObj = new JSONarray<JSONfieldObject>();
        List<IFieldable> fieldables = _FieldManager.AllFieldables;
        for (int i = 0; i < fieldables.Count; i++)
        {
            arrayObj.objects.Add(new JSONfieldObject(fieldables[i].sprite.name, fieldables[i].scale, fieldables[i].position, fieldables[i].pixelSize));
        } 
        var jsontxt = JsonUtility.ToJson(arrayObj);
        Debug.LogError(jsontxt);
        return jsontxt;
    }

    public void Save()
    {
        string json = CreateJSON();
        string path = EditorUtility.OpenFilePanel("new save", "", ".txt");
    }

    [System.Serializable]
    private class JSONarray<T>
    {
        public List<T> objects = new List<T>();
    }

    [System.Serializable]
    private class JSONfieldObject
    {
        public string spriteName;
        public Vector3 scale;
        public Vector3 position;
        public Vector3 pixelSize;

        public JSONfieldObject(string spriteName, Vector3 scale, Vector3 position, Vector3 pixelSize)
        {
            this.spriteName = spriteName;
            this.scale = scale;
            this.position = position;
            this.pixelSize = pixelSize;
        }
    }
}
