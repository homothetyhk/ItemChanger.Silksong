using ItemChanger.Serialization;
using Silksong.UnityHelper.Extensions;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs.BigUIDefs;

internal class RemoveBackboardWhenDamaged : MonoBehaviour
{
    private HeroController _hc;
    public string BackboardName;

    void Awake()
    {
        _hc = HeroController.instance;
        _hc.OnTakenDamage += OnHeroDamaged;
    }

    void OnDisable()
    {
        if (_hc)
        {
            _hc.OnTakenDamage -= OnHeroDamaged;
        }
    }

    private void OnHeroDamaged()
    {
        gameObject.FindChild(BackboardName)?.GetComponent<SpriteRenderer>()?.sprite = new EmptySprite().Value;
    }
}
