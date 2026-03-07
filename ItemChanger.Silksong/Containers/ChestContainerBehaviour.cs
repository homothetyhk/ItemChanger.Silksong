using System.Collections;
using HutongGames.PlayMaker;
using ItemChanger.Containers;
using ItemChanger.Enums;
using ItemChanger.Items;
using UnityEngine;
using ILogger = ItemChanger.Logging.ILogger;

namespace ItemChanger.Silksong.Containers
{
    internal sealed class ChestContainerBehaviour : MonoBehaviour
    {
        private static readonly Vector3[] ShinyOffsets =
        [
            new(-0.6f, 0.35f, 0f),
            new(0f, 0.45f, 0f),
            new(0.6f, 0.35f, 0f),
            new(-0.3f, 0.7f, 0f),
            new(0.3f, 0.7f, 0f),
            new(0f, 0.95f, 0f),
        ];
        private static readonly string[] AttackNameTokens = ["attack", "slash", "weapon", "nail", "hit", "sword", "cut"];

        private const string ActivationTriggerObjectName = "IC Chest Activation Trigger";

        public ContainerInfo? Info { get; private set; }

        private bool opened;
        private bool spawnedShinies;
        private bool persistentBoolInitialized;
        private PersistentBoolItem? persistentBoolItem;

        internal static GameObject Setup(GameObject host, ContainerInfo info)
        {
            var beh = host.GetComponent<ChestContainerBehaviour>() ?? host.AddComponent<ChestContainerBehaviour>();
            beh.Info = info;
            beh.opened = info.GiveInfo.Placement.CheckVisitedAny(VisitState.Opened);
            beh.spawnedShinies = false;
            beh.persistentBoolInitialized = false;
            beh.persistentBoolItem = null;
            beh.ConfigureHost();
            return host;
        }

        private void ConfigureHost()
        {
            if (Info == null)
            {
                return;
            }

            ConfigurePhysics(gameObject);
            ConfigureActivationTrigger();
            NeutralizeVanillaRewardLogic(gameObject);
        }

        private static void ConfigurePhysics(GameObject host)
        {
            if (host.GetComponent<Rigidbody2D>() != null)
            {
                return;
            }

            var body = host.AddComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Static;
            body.gravityScale = 0f;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void ConfigureActivationTrigger()
        {
            Transform existing = transform.Find(ActivationTriggerObjectName);
            GameObject triggerObject = existing?.gameObject ?? new GameObject(ActivationTriggerObjectName);
            if (existing == null)
            {
                triggerObject.transform.SetParent(transform, worldPositionStays: false);
            }

            var box = triggerObject.GetComponent<BoxCollider2D>() ?? triggerObject.AddComponent<BoxCollider2D>();
            box.isTrigger = true;
            Collider2D? reference = GetComponentsInChildren<Collider2D>(includeInactive: true)
                .FirstOrDefault(c => c != null && !c.isTrigger && c.gameObject != triggerObject);
            if (reference != null)
            {
                Bounds bounds = reference.bounds;
                Vector3 localCenter = transform.InverseTransformPoint(bounds.center);
                box.offset = localCenter;
                box.size = new Vector2(bounds.size.x * 1.2f, bounds.size.y * 1.2f);
            }
            else if (box.size == Vector2.zero)
            {
                box.size = new Vector2(1.5f, 1.2f);
            }
        }

        private static void NeutralizeVanillaRewardLogic(GameObject host)
        {
            Transform itemParent = host.transform.Find("Item");
            if (itemParent != null)
            {
                foreach (PlayMakerFSM fsm in itemParent.GetComponentsInChildren<PlayMakerFSM>(includeInactive: true).Where(fsm => fsm != null))
                {
                    fsm.enabled = false;
                }

                foreach (Transform child in itemParent)
                {
                    UObject.Destroy(child.gameObject);
                }
            }
        }

        private void Start()
        {
            if (Info == null)
            {
                return;
            }

            EnsurePersistentBool();
            opened = opened || Info.GiveInfo.Placement.CheckVisitedAny(VisitState.Opened) || GetPersistentValue();
            if (opened)
            {
                bool vanillaHandled = TryTriggerVanillaOpenState(instant: true);
                if (!vanillaHandled)
                {
                    ApplyOpenedVisuals(instant: true);
                }

                SpawnShiniesForRemainingItems();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) => TryOpenFrom(other);

        private void OnTriggerStay2D(Collider2D other) => TryOpenFrom(other);

        private void TryOpenFrom(Collider2D other)
        {
            if (opened || Info == null || other == null)
            {
                return;
            }

            if (!IsAttackCollider(other))
            {
                return;
            }

            OpenChest();
        }

        private void OpenChest()
        {
            if (Info == null || opened)
            {
                return;
            }

            opened = true;
            Info.GiveInfo.Placement.AddVisitFlag(VisitState.Opened);
            SetPersistentValue(true);
            bool vanillaHandled = TryTriggerVanillaOpenState(instant: false);
            if (!vanillaHandled)
            {
                ApplyOpenedVisuals(instant: false);
            }

            SpawnShiniesForRemainingItems();
        }

        private void ApplyOpenedVisuals(bool instant)
        {
            if (!TryPlayTk2dOpen(instant))
            {
                EnableOpenedSpriteRenderers();
            }
        }

        private bool TryPlayTk2dOpen(bool instant)
        {
            try
            {
                var animator = GetComponent<tk2dSpriteAnimator>();
                if (animator == null)
                {
                    return false;
                }

                tk2dSpriteAnimationClip openedClip = animator.GetClipByName("Opened");
                tk2dSpriteAnimationClip openClip = animator.GetClipByName("Open");

                if (instant)
                {
                    if (openedClip != null)
                    {
                        animator.Play(openedClip);
                        SetAnimatorToLastFrame(animator, openedClip);
                        return true;
                    }

                    if (openClip != null)
                    {
                        animator.Play(openClip);
                        SetAnimatorToLastFrame(animator, openClip);
                        return true;
                    }

                    return false;
                }

                if (openClip != null)
                {
                    animator.Play(openClip);
                    if (openedClip != null)
                    {
                        StartCoroutine(PlayOpenedAfterOpen(animator, openClip, openedClip));
                    }
                    return true;
                }

                if (openedClip != null)
                {
                    animator.Play(openedClip);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Logger().LogWarn($"[Chest] Failed to play chest animation: {e.GetType().Name}");
                return false;
            }
        }

        private bool TryTriggerVanillaOpenState(bool instant)
        {
            bool foundChestFsm = false;
            foreach (PlayMakerFSM fsm in GetComponentsInChildren<PlayMakerFSM>(includeInactive: true).Where(fsm => fsm != null))
            {
                string owner = fsm.gameObject.name?.ToLowerInvariant() ?? string.Empty;
                string fsmName = fsm.FsmName?.ToLowerInvariant() ?? string.Empty;
                if (!owner.Contains("chest") && !fsmName.Contains("chest") && !fsmName.Contains("control"))
                {
                    continue;
                }

                foundChestFsm = true;
                fsm.SendEvent("HIT");
                fsm.SendEvent("OPEN");
                if (instant)
                {
                    fsm.SendEvent("OPENED");
                }
            }

            return foundChestFsm;
        }

        private static IEnumerator PlayOpenedAfterOpen(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip openClip, tk2dSpriteAnimationClip openedClip)
        {
            float fps = openClip.fps <= 0f ? 30f : openClip.fps;
            int frameCount = openClip.frames?.Length ?? 1;
            float duration = Mathf.Max(0.05f, frameCount / fps);
            yield return new WaitForSeconds(duration);
            if (animator == null || openedClip == null)
            {
                yield break;
            }

            animator.Play(openedClip);
        }

        private static void SetAnimatorToLastFrame(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip)
        {
            if (animator == null || clip == null || clip.frames == null || clip.frames.Length == 0)
            {
                return;
            }

            int index = clip.frames.Length - 1;
            int spriteId = clip.frames[index].spriteId;
            var tkSprite = animator.GetComponent<tk2dBaseSprite>();
            if (tkSprite != null && clip.frames[index].spriteCollection != null)
            {
                tkSprite.SetSprite(clip.frames[index].spriteCollection, spriteId);
            }
        }

        private void EnableOpenedSpriteRenderers()
        {
            foreach (var sr in GetComponentsInChildren<SpriteRenderer>(includeInactive: true).Where(sr => sr != null))
            {
                string name = sr.name?.ToLowerInvariant() ?? string.Empty;
                if (name.Contains("open") || name.Contains("opened"))
                {
                    sr.gameObject.SetActive(true);
                    sr.enabled = true;
                }
            }
        }

        private void SpawnShiniesForRemainingItems()
        {
            if (spawnedShinies || Info == null)
            {
                return;
            }

            List<Item> items = Info.GiveInfo.Items.Where(i => !i.IsObtained()).ToList();
            if (items.Count == 0)
            {
                spawnedShinies = true;
                return;
            }

            FlingType flingType = Info.GiveInfo.FlingType == FlingType.DirectDeposit
                ? FlingType.Everywhere
                : Info.GiveInfo.FlingType;

            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                ContainerInfo shinyInfo = ContainerInfo.FromPlacementAndItems(
                    Info.GiveInfo.Placement,
                    [item],
                    gameObject.scene,
                    ShinyContainer.Instance.Name,
                    flingType
                );

                GameObject shiny = ShinyContainer.Instance.GetNewContainer(shinyInfo);
                Vector3 offset = ShinyOffsets[i % ShinyOffsets.Length] + new Vector3(0f, 0.2f * (i / ShinyOffsets.Length), 0f);
                ShinyContainer.Instance.ApplyTargetContext(shiny, transform.position + offset, Vector3.zero);
            }

            spawnedShinies = true;
            Logger().LogInfo($"[Chest] Open event for {Info.GiveInfo.Placement.Name}; spawned {items.Count} shinies.");
        }

        private void EnsurePersistentBool()
        {
            if (persistentBoolInitialized || Info == null)
            {
                return;
            }

            try
            {
                persistentBoolItem = GetComponent<PersistentBoolItem>() ?? gameObject.AddComponent<PersistentBoolItem>();
                PersistentItemData<bool>? data = persistentBoolItem.ItemData;
                if (data == null)
                {
                    Logger().LogWarn("[Chest] PersistentBoolItem.ItemData was null.");
                    persistentBoolInitialized = true;
                    return;
                }

                bool initialValue = data.Value || Info.GiveInfo.Placement.CheckVisitedAny(VisitState.Opened);
                data.ID = GetPersistentId();
                data.SceneName = ResolveSceneName();
                data.IsSemiPersistent = false;
                data.Mutator = SceneData.PersistentMutatorTypes.None;
                data.Value = initialValue;
            }
            catch (Exception e)
            {
                Logger().LogWarn($"[Chest] Failed to initialize PersistentBoolItem: {e.GetType().Name}");
            }
            finally
            {
                persistentBoolInitialized = true;
            }
        }

        private bool GetPersistentValue()
        {
            if (persistentBoolItem == null)
            {
                return false;
            }

            try
            {
                PersistentItemData<bool>? data = persistentBoolItem.ItemData;
                if (data == null)
                {
                    return false;
                }

                return data.Value;
            }
            catch
            {
                return false;
            }
        }

        private void SetPersistentValue(bool value)
        {
            if (persistentBoolItem == null)
            {
                return;
            }

            try
            {
                PersistentItemData<bool>? data = persistentBoolItem.ItemData;
                if (data == null)
                {
                    return;
                }

                data.Value = value;
            }
            catch (Exception e)
            {
                Logger().LogWarn($"[Chest] Failed setting PersistentBoolItem value: {e.GetType().Name}");
            }
        }

        private string GetPersistentId()
        {
            if (Info?.GiveInfo?.Placement?.Name is string placementName && !string.IsNullOrEmpty(placementName))
            {
                return placementName;
            }

            return gameObject.name;
        }

        private string ResolveSceneName()
        {
            if (Info != null && Info.ContainingScene.IsValid() && !string.IsNullOrEmpty(Info.ContainingScene.name))
            {
                return Info.ContainingScene.name;
            }

            return gameObject.scene.IsValid() ? gameObject.scene.name : "UnknownScene";
        }

        private static bool IsAttackCollider(Collider2D col)
        {
            if (!IsPlayerCollider(col))
            {
                return false;
            }

            string name = col.gameObject.name?.ToLowerInvariant() ?? string.Empty;
            for (int i = 0; i < AttackNameTokens.Length; i++)
            {
                if (name.Contains(AttackNameTokens[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsPlayerCollider(Collider2D col)
        {
            GameObject? player = GameManager.instance?.hero_ctrl?.gameObject;
            if (player == null)
            {
                return false;
            }

            return col.gameObject == player || col.transform.IsChildOf(player.transform);
        }

        private static ILogger Logger() => ItemChangerHost.Singleton.Logger;
    }
}
