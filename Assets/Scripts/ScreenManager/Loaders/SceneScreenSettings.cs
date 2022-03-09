using ScreenManager.Interfaces;

namespace ScreenManager.Loaders.Scenes
{
    public class SceneScreenSettings : IScreenSettings
    {
        public string Layer { get; private set; }
        public string Path { get; private set; }
        public string Name { get; private set; }

        public SceneScreenSettings(string scene, string name)
        {
            Name = name;
            Path = scene;
            Layer = scene;
        }
    }
}
