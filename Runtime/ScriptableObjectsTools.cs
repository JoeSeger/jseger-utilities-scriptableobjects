using JSeger.Utilities.Directories.Runtime;
using UnityEngine;

namespace JSeger.Utilities.Scriptableobjects.Runtime
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;
        private static bool _isLoading = false;

        private static T _instanceResult;

        public static T Instance
        {
            get
            {
                if (_instanceResult == null)
                {
                    _instanceResult = ResourcesExtensions.LoadAsset<T>(typeof(T).Name);
                }

                return _instanceResult;
            }
        }
    }
}