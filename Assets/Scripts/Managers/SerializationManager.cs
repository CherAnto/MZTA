﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
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
            arrayObj.objects.Add(new JSONfieldObject(fieldables[i].sprite.name, fieldables[i].scale, fieldables[i].position, fieldables[i].pixelSize));
        } 
        var jsontxt = JsonUtility.ToJson(arrayObj); 
        return jsontxt;
    }

    public void Save()
    {
        string json = CreateJSON();
        //string path = EditorUtility.OpenFilePanel("new save", "", ".txt");
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        saveFileDialog.Title = "Create a save file";
        saveFileDialog.FileName = "new save";
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        { 
            System.IO.FileStream fs =
                (System.IO.FileStream)saveFileDialog.OpenFile();
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
            fs.Close();
        } 
    }

    public void Load()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        openFileDialog.Title = "Save an Image File";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            if (openFileDialog.FileName != "")
            {
                var fStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fStream))
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
                        Debug.LogError($"Cannot read JSON: {openFileDialog.FileName} {e.Message}" );
                    }
                }  
            }
        }

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

        public JSONfieldObject(string spriteName, Vector3 scale, Vector3 position, Vector3 pixelSize)
        {
            this.spriteName = spriteName;
            this.scale = scale;
            this.position = position;
            this.pixelSize = pixelSize;
        }
    }
} 
