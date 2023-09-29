using UnityEngine;

namespace JSeger.Utilities.ScriptableObjects.Runtime
{
    public interface ISingletonScriptableObject
    {
        string ResourcePath();
    }

    public abstract class SingletonScriptableObject<T> : ScriptableObject
        where T : ScriptableObject, ISingletonScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                var tempInstance = CreateInstance<T>();
                var resourcePath = tempInstance.ResourcePath() + typeof(T).Name;
                DestroyImmediate(tempInstance);

                _instance = Resources.Load<T>(resourcePath);
                if (_instance == null)
                {
                    Debug.LogError($"Failed to load {typeof(T).Name} from path: {resourcePath}");
                }

                return _instance;
            }
        }
    }
}