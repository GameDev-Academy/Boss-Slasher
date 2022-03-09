using Core;
using UnityEngine;

namespace ScreenManager.Loaders.Scenes
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField]
        private UserInfo _userInfo;
        
        public void Awake()
        {
            ScreensManager.OpenScreen<Game, GameContext>(new GameContext());
        }
    }
}