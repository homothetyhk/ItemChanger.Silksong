using System.Collections;
using UnityEngine;

namespace ItemChanger.Silksong.Extensions;

public static class UnityExtensions
{
    public static void DoNextFrame(this MonoBehaviour b, Action? a)
    {
        b.StartCoroutine(NextFrame(a));
    }

    private static IEnumerator NextFrame(Action? a)
    {
        yield return null;
        try
        {
            a?.Invoke();
        }
        catch (Exception e)
        {
            LogError(e.ToString());
            throw;
        }
    }

}
