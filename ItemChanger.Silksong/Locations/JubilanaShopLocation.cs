using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class JubilanaShopLocation : ShopLocation
{
    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new SceneEditGroup() { { SceneNames.Song_Enclave, PreventJubilanaLeaving } });
    }

    private void PreventJubilanaLeaving(Scene scene)
    {
        foreach (var obj in scene.AllGameObjects().Where(o => o.name == "City Merchant Enclave"))
        {
            foreach (var test in obj.GetComponents<TestGameObjectActivator>())
                UObject.DestroyImmediate(test);

            obj.SetActive(QuestManager.TryGetFullQuestBase(Quests.Save_City_Merchant, out var quest) && quest.IsCompleted);
        }
    }
}
