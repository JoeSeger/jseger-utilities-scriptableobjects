using System.IO;
using JSeger.Utilities.ScriptableObjects.Runtime;
using UnityEditor;
using UnityEngine;

namespace JSeger.Utilities.ScriptableObjects.Editor
{
    public class SingletonScriptableObjectAssetNameMatcher : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var newPath in movedAssets)
            {
                // Check if the asset is a ScriptableObject and implements the ISingletonScriptableObject interface
                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(newPath);

                if (obj == null || obj is not ISingletonScriptableObject) continue;
                // If the name does not match the type, revert the name
                if (obj.name == obj.GetType().Name) continue;
                var correctPath = Path.GetDirectoryName(newPath) + "/" + obj.GetType().Name + ".asset";
                AssetDatabase.MoveAsset(newPath, correctPath);
                Debug.LogError($"You cannot change the name of {obj.GetType().Name}. It has been reverted back.");
            }
        }
    }
}