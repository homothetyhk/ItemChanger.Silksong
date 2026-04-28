using ItemChanger.Extensions;
using ItemChanger.Modules;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module that allows destroying objects when entering a specific scene.
/// </summary>
[SingletonModule]
public sealed class ObjectDestroyModule : Module
{
    protected override void DoLoad()
    {
        SilksongHost.Instance.GameEvents.OnNextSceneLoaded += DestroyObjects;
    }

    protected override void DoUnload()
    {
        SilksongHost.Instance.GameEvents.OnNextSceneLoaded -= DestroyObjects;
    }

    [JsonProperty] private Dictionary<string, HashSet<string>> _destroyedObjects = [];

    public void AddDestroyer(string scene, string gameObject)
    {
        if (!_destroyedObjects.TryGetValue(scene, out HashSet<string> gameObjects))
        {
            gameObjects = _destroyedObjects[scene] = new();
        }

        gameObjects.Add(gameObject);
    }

    public void RemoveDestroyer(string scene, string gameObject)
    {
        if (!_destroyedObjects.TryGetValue(scene, out HashSet<string> gameObjects))
        {
            gameObjects = _destroyedObjects[scene] = new();
        }

        gameObjects.Remove(gameObject);
    }

    private void DestroyObjects(Events.Args.SceneLoadedEventArgs args)
    {
        if (_destroyedObjects.TryGetValue(args.Scene.name, out HashSet<string> gameObjects))
        {
            foreach (string gameObject in gameObjects)
            {
                GameObject? toDestroy = args.Scene.FindGameObject(gameObject);

                if (toDestroy)
                {
                    UObject.Destroy(toDestroy);
                }
            }
        }
    }
}
