using HarmonyLib;
using ItemChanger.Costs;
using ItemChanger.Modules;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Extensions;
using MonoDetour.Cil;
using MonoDetour.DetourTypes;
using MonoDetour.Reflection.Unspeakable;
using Silksong.UnityHelper.Extensions;

namespace ItemChanger.Silksong.Modules.YNBox;

/// <summary>
/// Module that allows for certain components to send more customizable YN boxes,
/// provided that the gameObject has a <see cref="CustomYNBoxInfo"/> component.
/// </summary>
[SingletonModule]
public sealed class CustomYNEnableModule : Module
{
    protected override void DoLoad()
    {
        // Allow showing cost text, rather than sprite + amount
        Using(Md.SavedItemDisplay.Setup.ControlFlowPrefix(OverrideSavedItemDisplay));

        // Modify the CIP pickup routine so that a YN dialog is shown
        Using(Md.CollectableItemPickup.Pickup.ControlFlowPrefixMoveNext(OverrideCIPPickup));
        Using(Md.CollectableItemPickup.DoPickupAction.ControlFlowPrefix(SkipPickupIfNotClicked));
    }

    protected override void DoUnload() { }

    private ReturnFlow SkipPickupIfNotClicked(CollectableItemPickup self, ref bool breakIfAtMax, ref bool returnValue)
    {
        YNOptionMarker marker = self.gameObject.GetComponent<YNOptionMarker>();
        if (marker != null && !marker.SelectedOption)
        {
            returnValue = false;
            return ReturnFlow.SkipOriginal;
        }

        return ReturnFlow.None;
    }

    // TODO - determine this at runtime? Probably blocked by DataManager #32
    private const int VanillaJustBeforePickupState = 2;
    private const int DialogOpenState = 1345134;
    private const int DialogJustClosedState = 1345135;

    private ReturnFlow OverrideCIPPickup(SpeakableEnumerator<object, CollectableItemPickup> self, ref bool continueEnumeration)
    {
        // The state set by the `yield return new WaitForSeconds(0.75)` shortly before DoPickupAction is called
        // We add in states to modify the behaviour of the enumerator
        if (self.State != VanillaJustBeforePickupState && self.State != DialogOpenState && self.State != DialogJustClosedState)
        {
            return ReturnFlow.None;
        }

        CustomYNBoxInfo info = self.This.gameObject.GetComponent<CustomYNBoxInfo>();
        if (info == null)
        {
            // Original behaviour
            return ReturnFlow.None;
        }

        switch (self.State)
        {
            // Returning to the enumerator after yield return 0.75s has finished
            case VanillaJustBeforePickupState:
                // Show cost box
                Open(() => 
                {
                    self.This.gameObject.GetOrAddComponent<YNOptionMarker>().SelectedOption = true;
                    self.State = DialogJustClosedState;
                }, () =>
                {
                    self.This.gameObject.GetOrAddComponent<YNOptionMarker>().SelectedOption = false;
                    self.State = DialogJustClosedState;
                }, info.Cost, info.TextGetter());

                // yield return null
                self.State = DialogOpenState;
                self.Current = null!;
                continueEnumeration = true;
                return ReturnFlow.SkipOriginal;

            // Waiting while the box is open
            case DialogOpenState:
                self.Current = null!;
                continueEnumeration = true;
                return ReturnFlow.SkipOriginal;

            // Box has closed, continue the routine as normal
            // Skipping the actual pickup if no was selected is in a separate hook
            case DialogJustClosedState:
                self.State = VanillaJustBeforePickupState;
                continueEnumeration = true;
                return ReturnFlow.None;

            default:
                LogWarn($"CollectableItemPickup.Pickup routine unexpectedly in state {self.State}");
                self.State = VanillaJustBeforePickupState;
                continueEnumeration = true;
                return ReturnFlow.None;
        }
    }

    private ReturnFlow OverrideSavedItemDisplay(SavedItemDisplay self, ref SavedItem item, ref int amount)
    {
        // Repair any damage from before (not sure if this is necessary...)
        self.amountText.alignment = TMProOld.TextAlignmentOptions.BottomRight;

        if (item is not ItemChangerCostProxy costProxy)
        {
            // Not an ItemChanger Cost, so fall through to the original method
            return ReturnFlow.None;
        }

        if (costProxy.Cost is IDisplayCost)
        {
            // DisplayCosts are handled via the overridden methods on ItemChangerCostProxy
            return ReturnFlow.None;
        }

        self.icon.sprite = null;
        self.amountText.text = costProxy.Cost.GetCostText();
        self.amountText.alignment = TMProOld.TextAlignmentOptions.Bottom;

        return ReturnFlow.SkipOriginal;
    }
    
    /// <summary>
    /// Open a YN dialogue box that displays the provided text and cost.
    /// </summary>
    /// <param name="yes">Callback invoked when "yes" is selected.</param>
    /// <param name="no">Callback invoked when "no" is selected.</param>
    /// <param name="cost">The <see cref="Cost"/> to be paid.</param>
    /// <param name="text">
    /// The text to diplay. This should describe what will happen when "yes" is selected,
    /// and will typically be the result of <see cref="ItemChanger.Placements.Placement.GetUIName()"/>
    /// or a similar method.
    /// </param>
    /// <param name="shouldPay">
    /// If true, the cost will be paid when "yes" is selected.
    /// Typically, this should be false if and only if cost payment is already included
    /// in the <paramref name="yes"/> delegate.
    /// </param>
    public static void Open(Action? yes, Action? no, Cost cost, string text, bool shouldPay = true)
    {
        void updatedYes()
        {
            if (!shouldPay)
            {
                yes?.Invoke();
                return;
            }

            if (cost.TryToPay())
            {
                yes?.Invoke();
            }
            else
            {
                no?.Invoke();
            }
        }
        
        if (cost is ICurrencyCost currencyCost)
        {
            int amount = cost.Paid ? 0 : currencyCost.Amount;
            DialogueYesNoBox.Open(updatedYes, no, true, text, currencyCost.CurrencyType, amount, consumeCurrency: false);
            return;
        }

        List<Cost> costs = [.. (new MultiCost(cost))];
        if (!costs.All(x => x is IDisplayCost))
        {
            if (cost.Paid)
            {
                DialogueYesNoBox.Open(updatedYes, no, true, text, [], [], displayHudPopup: true, consumeCurrency: false, null);
            }
            else
            {
                ItemChangerCostProxy costProxy = ItemChangerCostProxy.FromCost(cost);
                DialogueYesNoBox.Open(updatedYes, no, true, text, [costProxy], [1], displayHudPopup: true, consumeCurrency: false, null);
            }
            return;
        }

        List<Cost> unpaidCosts = costs.Where(x => !x.Paid).ToList();
        List<IDisplayCost> displayableCosts = unpaidCosts.Cast<IDisplayCost>().ToList();

        List<ItemChangerCostProxy> displays = unpaidCosts.Select(x => ItemChangerCostProxy.FromCost(x)).ToList();
        List<int> amounts = displayableCosts.Select(x => x.Amount).ToList();

        DialogueYesNoBox.Open(updatedYes, no, true, text, displays, amounts, true, false, null);
    }
}
