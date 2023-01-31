using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    public int buttonIntervalPosY = 40;
    public GameObject buttonDefault;

    private static string[] objectNames;
    private static GameObject currentObject;

    void Awake()
    {
        GetAllPrefabNames();
        InitializeUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllPrefabNames()
    {
        //改为数组
        DirectoryInfo direction = new DirectoryInfo(GetAllPrefabPath());  
        FileInfo[] files = direction.GetFiles("*.prefab" , SearchOption.TopDirectoryOnly);
        List<string> names = new List<string>();
        foreach(var file in files)
        {
            names.Add(file.Name);
        }
        objectNames = names.ToArray();
    }

    void InitializeUI()
    {
        if(objectNames.Length < 1) return;
        for( int i = 0; i<objectNames.Length;i++)
        {
            GameObject buttonObject = Instantiate(buttonDefault , buttonDefault.transform.parent);
            var pos = buttonObject.transform.localPosition;
            var str = objectNames[i];
            buttonObject.transform.localPosition = new Vector3(pos.x, -(buttonIntervalPosY * i),pos.z);
            buttonObject.transform.name = str;
            buttonObject.GetComponentInChildren<Text>().text = str;
            buttonObject.GetComponent<Button>().onClick.AddListener(delegate{OnbuttonClick(str);} );
        }
        this.transform.GetComponentInChildren<ScrollRect>().verticalScrollbar.value = 1;
        Destroy(buttonDefault);
    }

    void OnbuttonClick(string name)
    {
        DestroyImmediate(currentObject);
        GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(GetPrefabPath(name));
        currentObject = Instantiate(gameObject);
    }

    string GetAllPrefabPath()
    {
        string path = "";
        path = Application.dataPath+"/Resources/Prefabs";
        return path;
    }

    string GetPrefabPath(string name)
    {
        string path = "";
        path = "Assets/Resources/Prefabs" + "/" + name;
        Debug.Log(path);
        return path;
    }
}
