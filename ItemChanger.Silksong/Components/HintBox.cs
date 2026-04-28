using ItemChanger.Placements;
using UnityEngine;

namespace ItemChanger.Silksong.Components;

/// <summary>
/// A component which displays custom dream text when triggered by proximity.
/// </summary>
public class HintBox : MonoBehaviour
{
    /// <summary>
    /// Create a HintBox at the specified position with the specified delegates.
    /// </summary>
    public static HintBox Create(Vector2 pos, Func<string?>? getDisplayText, Func<bool>? displayTest = null, Action<string>? onDisplay = null)
    {
        var hint = Create(pos, new Vector2(5f, 5f));
        hint.GetDisplayText = getDisplayText;
        hint.DisplayTest = displayTest;
        hint.OnDisplay = onDisplay;
        return hint;
    }

    /// <summary>
    /// Create a HintBox at the position of the transform using the placement's GetUIName, AllObtained, and AddVisitFlag(VisitState.Previewed) methods.
    /// </summary>
    public static HintBox Create(Transform transform, Placement placement)
    {
        var hint = Create(transform.position, new Vector2(5f, 5f));
        hint.GetDisplayText = placement.GetUIName;
        hint.DisplayTest = () => !placement.AllObtained();
        hint.OnDisplay = placement.OnPreview;
        return hint;
    }

    /// <summary>
    /// Creates a HintBox of specified position and size. The delegate fields of the HintBox are not set.
    /// </summary>
    public static HintBox Create(Vector2 pos, Vector2 size)
    {
        GameObject obj = new GameObject("Hint Box");
        obj.transform.position = pos;
        BoxCollider2D box = obj.AddComponent<BoxCollider2D>();
        box.size = size;
        box.isTrigger = true;

        HintBox hint = obj.AddComponent<HintBox>();

        return hint;
    }

    public Func<bool>? DisplayTest;
    public Func<string?>? GetDisplayText;
    public Action<string>? OnDisplay;

    private ILocalisedTextCollection? _currentText;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || (DisplayTest?.Invoke() == false))
        {
            return;
        }

        string? s = GetDisplayText?.Invoke();
        if (!string.IsNullOrEmpty(s))
        {
            ClearText();
            _currentText = new SingleLocalizedText() { text = s };

            NeedolinMsgBox.AddText(_currentText, skipStartDelay: true, maxPriority: true);
            OnDisplay?.Invoke(s!);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            return;
        }

        ClearText();
    }

    public void Update()
    {
        if (_currentText is not null && DisplayTest?.Invoke() == false)
        {
            ClearText();
        }
    }

    public void OnDisable()
    {
        ClearText();
    }

    private void ClearText()
    {
        if (_currentText is not null)
        {
            NeedolinMsgBox.RemoveText(_currentText);
            _currentText = null;

            HideBoxImmediate();
        }
    }

    private void HideBoxImmediate()
    {
        NeedolinMsgBox box = NeedolinMsgBox._instance;

        if (box.hideRoutine != null)
        {
            box.StopCoroutine(box.hideRoutine);
            box.hideRoutine = box.StartCoroutine(box.Hide(true));
        }
    }
}
