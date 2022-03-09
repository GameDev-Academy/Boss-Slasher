using UnityEngine;

namespace ScreenManager.Loaders.Scenes
{
    public class CoroutinesHolder : MonoBehaviour
    {
        public static CoroutinesHolder Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}