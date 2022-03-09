using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ScreenManager.Loaders.Scenes
{
    public static class SceneUtils
    {
        static public void AffectAComponentInSceneChildren<T>(Scene scene, Action<T> action = null) {
            T comp = GetAComponentInSceneChildren<T> (scene);
            if(!object.Equals(comp, default(T)) && action != null) {
                action (comp);
            }
        }
        
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> handler)
        {
            foreach (T item in enumerable)
            {
                handler(item);
            }
        }

        static public T GetAComponentInSceneChildren<T>(Scene scene)
        {
            if (scene == null) { return default(T); }
            if (!scene.IsValid()) { return default(T); }

            T comp = default(T);
            var rootGameObjects = scene.GetRootGameObjects();
            if (rootGameObjects != null)
            {
                rootGameObjects.ForEach(go =>
                {
                    //if(myThing != null) return;
                    T temp = go.GetComponentInChildren<T>(true);
                    if (!object.Equals(temp, default(T)))
                    {
                        comp = temp;
                    }
                });
            }
            return comp;
        }

    }
}