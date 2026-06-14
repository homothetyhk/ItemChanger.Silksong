using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.RawData;
using Silksong.UnityHelper.Extensions;
using UnityEngine;

namespace ItemChanger.Silksong.Tags.SpecialItemTags;

public class TwistedBudCryTag : ShinyModifierTag
{
    public override void ModifyShinyContainer(Placement placement, Item item, GameObject shiny)
    {
        // Don't modify the vanilla location.
        // TODO: The location should also disable the Crying Audio Control object when randomized, unless it's a dearest.
        if (placement.Name == LocationNames.Twisted_Bud) return;

        var audioObj = GameObjectKeys.TWISTED_BUD_AUDIO.InstantiateInCurrentScene();
        audioObj.SetActive(false);

        audioObj.transform.SetParent(shiny.transform);
        audioObj.transform.localPosition = Vector3.zero;
        audioObj.transform.localScale = Vector3.one;

        audioObj.RemoveComponents<PlayMakerFixedUpdate>();
        audioObj.RemoveComponents<PlayMakerFSM>();

        // The FSM is designed for two disjoint 'close' and 'far' regions, but we want an instant shift.
        // It's easier to code our intent directly than to try and frankenstein the FSM.
        audioObj.AddComponent<TwistedBudAudioController>().Item = item;

        audioObj.SetActive(true);
    }
}

file class TwistedBudAudioController : MonoBehaviour
{
    internal Item? Item;

    private AudioSource? farAudio;
    private AudioSource? closeAudio;
    private AudioSource? singAudio;

    private AudioSource FindAudioSource(string name)
    {
        var src = gameObject.FindChild(name)!.GetComponent<AudioSource>();
        src.volume = 0;
        return src;
    }

    private void Awake()
    {
        farAudio = FindAudioSource("Audio Player Far");
        closeAudio = FindAudioSource("Audio Player Close");
        singAudio = FindAudioSource("Audio Player Sing");
    }

    private bool wasPlaying = false;
    private float playChangeTime = 0;

    private void Update()
    {
        bool isPlaying = HeroPerformanceRegion.GetAffectedState(transform, ignoreRange: false) == HeroPerformanceRegion.AffectedState.ActiveInner;
        if (isPlaying != wasPlaying)
        {
            playChangeTime += Time.deltaTime;
            if (playChangeTime >= (isPlaying ? 3 : 1))
            {
                wasPlaying = isPlaying;
                playChangeTime = 0;
            }
        }
        else playChangeTime = 0;

        float dist = (HeroController.instance.transform.position - transform.position).magnitude;

        const float CLOSE = 20;
        const float FAR = 75;
        UpdateAudio(farAudio, (dist > CLOSE && dist <= FAR) ? 1 : 0, 0.5f);
        UpdateAudio(closeAudio, (dist <= CLOSE && !wasPlaying) ? 1 : 0, 0.5f);
        UpdateAudio(singAudio, (dist <= CLOSE && wasPlaying) ? 1 : 0, 1f);
    }

    private void UpdateAudio(AudioSource? source, float volume, float fadeTime)
    {
        if (source == null) return;
        if (Item?.IsObtained() ?? false) volume = 0;

        float current = source.volume;
        float delta = Mathf.Sign(volume - current) * Time.deltaTime / fadeTime;
        if ((current <= volume && current + delta >= volume) || (current >= volume && current + delta <= volume)) current = volume;
        else current += delta;

        source.volume = current;
        if (!source.isPlaying && current > 0) source.Play();
    }
}
