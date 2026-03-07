using Benchwarp.Data;
using ItemChanger.Containers;
using ItemChanger.Silksong.Extensions;
using Silksong.AssetHelper.ManagedAssets;

namespace ItemChanger.Silksong.Containers
{
    public class ChestContainer : Container
    {
        private sealed record ChestPrefabData(string SceneName, string ObjectName, ManagedAsset<GameObject> Prefab);

        private static readonly ChestPrefabData[] PrefabCandidates =
        [
            new(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest")),
            new(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest (1)", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest (1)")),
            new(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest (2)", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest (2)")),
            new(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest")),
            new(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest (1)", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest (1)")),
            new(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest (2)", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest (2)")),
            new(SceneNames.Tut_01, "Bone Chest", ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Tut_01, "Bone Chest")),
        ];

        private static bool prefabSourceLogged;

        public static ChestContainer Instance { get; } = new();

        public override string Name => ContainerNames.Chest;

        public override uint SupportedCapabilities => ContainerCapabilities.None;

        public override bool SupportsInstantiate => true;

        public override bool SupportsModifyInPlace => true;

        public override GameObject GetNewContainer(ContainerInfo info)
        {
            ChestPrefabData source = ResolveChestPrefabOrThrow();
            source.Prefab.EnsureLoaded();
            GameObject chest = source.Prefab.InstantiateInScene(info.ContainingScene);
            chest.name = $"IC Chest for {info.GiveInfo.Placement.Name}";
            ModifyContainerInPlace(chest, info);
            return chest;
        }

        public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
        {
            ChestContainerBehaviour.Setup(obj, info);
        }

        protected override void Load()
        {
            foreach (ChestPrefabData source in PrefabCandidates)
            {
                source.Prefab.Load();
            }
        }

        protected override void Unload()
        {
            foreach (ChestPrefabData source in PrefabCandidates)
            {
                source.Prefab.Unload();
            }
        }

        private static ChestPrefabData ResolveChestPrefabOrThrow()
        {
            for (int i = 0; i < PrefabCandidates.Length; i++)
            {
                ChestPrefabData source = PrefabCandidates[i];
                try
                {
                    source.Prefab.EnsureLoaded();
                    if (source.Prefab.Handle.Result != null)
                    {
                        LogPrefabSource(source);
                        return source;
                    }
                }
                catch (Exception e)
                {
                    ItemChangerHost.Singleton.Logger.LogWarn($"[Chest] Failed prefab candidate {source.SceneName}/{source.ObjectName}: {e.GetType().Name}");
                }
            }

            string candidates = string.Join(", ", PrefabCandidates.Select(x => $"{x.SceneName}/{x.ObjectName}"));
            string message = $"[Chest] Failed to resolve chest prefab from candidates: {candidates}";
            ItemChangerHost.Singleton.Logger.LogError(message);
            throw new InvalidOperationException(message);
        }

        private static void LogPrefabSource(ChestPrefabData source)
        {
            if (prefabSourceLogged)
            {
                return;
            }

            ItemChangerHost.Singleton.Logger.LogInfo($"[Chest] Using prefab source {source.SceneName}/{source.ObjectName}");
            prefabSourceLogged = true;
        }
    }

}
