using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveLoadMap))]
public class SaveLoadMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var saveLoadMap = (SaveLoadMap) target;
        if (GUILayout.Button("Save"))
        {
            Debug.Log("Saved map");
            saveLoadMap.Save();
        }
        if (GUILayout.Button("Load"))
        {
            Debug.Log("Loaded map");
            saveLoadMap.Load();
        }
    }
}