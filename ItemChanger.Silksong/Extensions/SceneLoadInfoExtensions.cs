namespace ItemChanger.Silksong.Extensions;

public static class SceneLoadInfoExtensions
{
    extension(GameManager.SceneLoadInfo info)
    {
        public bool IsBaseGameSceneLoadInfo()
        {
            Type t = info.GetType();
            return t == typeof(GameManager.SceneLoadInfo)
                || t == typeof(TransitionPoint.SceneLoadInfo)
                || t == typeof(FastTravelCutscene.FastTravelAsyncLoadInfo);
        }
    }
}
