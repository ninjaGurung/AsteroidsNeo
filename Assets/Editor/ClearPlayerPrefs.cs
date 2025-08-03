using UnityEditor;
using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Editor
{
    /// <summary>
    /// This script adds a menu item to the Unity Editor to clear all PlayerPrefs.
    /// </summary>
    public class ClearPlayerPrefs : EditorWindow
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All PlayerPrefs have been deleted.");
        }
    }
}