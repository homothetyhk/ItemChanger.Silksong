using static PlayerDataTest;

namespace ItemChanger.Silksong.Extensions;

internal static class PlayerDataTestExtensions
{
    public static bool IsSingleTest(this PlayerDataTest pdt, out PlayerDataTest.Test t)
    {
        if (pdt.TestGroups.Length == 1 && pdt.TestGroups[0].Tests.Length == 1)
        {
            t = pdt.TestGroups[0].Tests[0];
            return true;
        }
        else
        {
            t = default;
            return false;
        }
    }

    public static void Modify(this PlayerDataTest pdt, Func<PlayerDataTest.Test, PlayerDataTest.Test> func)
    {
        for (int i = 0; i < pdt.TestGroups.Length; i++)
        {
            for (int j = 0; j < pdt.TestGroups[i].Tests.Length; j++)
            {
                pdt.TestGroups[i].Tests[j] = func(pdt.TestGroups[i].Tests[j]);
            }
        }
    }
    
    public static void ForceResult(this PlayerDataTest pdt, bool result)
    {
        pdt.TestGroups = [new TestGroup()
        {
            Tests = [new Test() {
                Type = TestType.Bool,
                FieldName = ALWAYS_FALSE,
                BoolValue = !result
            }]
        }];
    }

    private const string ALWAYS_FALSE = "ITEMCHANGER_ALWAYS_FALSE";
}
