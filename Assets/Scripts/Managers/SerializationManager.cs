using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text; 
using UnityEditor;
using UnityEngine;
using Zenject; 

public class SerializationManager : MonoBehaviour
{
    [SerializeField] string SpritesPath;
    [SerializeField] int SpritesNumber;

    [Inject] FieldManager _FieldManager;
    public IReadOnlyDictionary<string,Sprite> allSprites; 

    public async void Initialize()
    { 
        Dictionary<string, Sprite> localSprites = new Dictionary<string, Sprite>();
        Sprite[] loaded = Resources.LoadAll<Sprite>(SpritesPath) as Sprite[]; 
        for (int i = 0; i < loaded.Length; i++)
        {
            localSprites.Add(loaded[i].name, loaded[i]); 
        } 
        allSprites = localSprites as IReadOnlyDictionary<string,Sprite>;
        SpritesNumber = allSprites.Count; 
    }

    public string CreateJSON()
    {
        JSONarray<JSONfieldObject> arrayObj = new JSONarray<JSONfieldObject>();
        List<IFieldable> fieldables = _FieldManager.AllFieldables;
        for (int i = 0; i < fieldables.Count; i++)
        {
            arrayObj.objects.Add(new JSONfieldObject(fieldables[i].sprite.name, fieldables[i].scale, fieldables[i].position, fieldables[i].pixelSize, fieldables[i].color));
        } 
        var jsontxt = JsonUtility.ToJson(arrayObj); 
        return jsontxt;
    }

    public void Save()
    {
        try
        {
            string json = CreateJSON();
        //string path = EditorUtility.OpenFilePanel("new save", "", ".txt"); 
        string path = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "new save", "txt");// (string path) => {
             
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(json);
            }
    }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void Load()
    {
        try
        {
            StandaloneFileBrowser.OpenFilePanelAsync("Open File", Application.dataPath, "txt", false, (string[] path) => {

                using (StreamReader reader = new StreamReader(path[0]))
                {
                    var fileContent = reader.ReadToEnd();
                    try
                    {
                        JSONarray<JSONfieldObject> arrayObj = JsonUtility.FromJson<JSONarray<JSONfieldObject>>(fileContent);
                        _FieldManager.Load(arrayObj.objects);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(fileContent);
                        Debug.LogError($"Cannot read JSON: {path[0]} {e.Message}");
                    }
                }

            });
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        

        //OpenFileDialog openFileDialog = new OpenFileDialog();
        //openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        //openFileDialog.Title = "Save an Image File";
        //if (openFileDialog.ShowDialog() == DialogResult.OK)
        //{
        //    if (openFileDialog.FileName != "")
        //    {
        //        var fStream = openFileDialog.OpenFile();
        //        using (StreamReader reader = new StreamReader(fStream))
        //        {
        //            var fileContent = reader.ReadToEnd();
        //            try
        //            {
        //                JSONarray<JSONfieldObject> arrayObj = JsonUtility.FromJson<JSONarray<JSONfieldObject>>(fileContent); 
        //                _FieldManager.Load(arrayObj.objects);
        //            }
        //            catch (System.Exception e)
        //            {
        //                Debug.LogError(fileContent);
        //                Debug.LogError($"Cannot read JSON: {openFileDialog.FileName} {e.Message}" );
        //            }
        //        }  
        //    }
        //}

    }

    [System.Serializable]
    public class JSONarray<T>
    {
        public List<T> objects = new List<T>();
    }

    [System.Serializable]
    public class JSONfieldObject
    {
        public string spriteName;
        public Vector3 scale;
        public Vector3 position;
        public Vector3 pixelSize;
        public Color color;

        public JSONfieldObject(string spriteName, Vector3 scale, Vector3 position, Vector3 pixelSize, Color color)
        {
            this.spriteName = spriteName;
            this.scale = scale;
            this.position = position;
            this.pixelSize = pixelSize;
            this.color = color;
        }
    }
} 
