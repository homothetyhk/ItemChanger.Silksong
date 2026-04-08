using ItemChanger.Items;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.RawData;
using UnityEngine;

namespace ItemChanger.Silksong.Components;

public class ItemParticles : MonoBehaviour
{
    public IEnumerable<Item> items;
    GameObject benchParticles;
    ParticleSystem ps;
    public Vector3 offset;

    public void Awake()
    {
        benchParticles = GameObjectKeys.BENCH_PARTICLE.InstantiateAsset(gameObject.scene);
        benchParticles.transform.SetParent(transform);
        ps = benchParticles.GetComponent<ParticleSystem>();
    }

    public void Start()
    {
        Vector3 pos = transform.position;
        pos.z -= 3f;
        pos += offset;
        benchParticles.transform.position = pos;

        // I couldn't get the Particle B/F to work so I'm using this - consequently several modifications
        // to the particle system have to be made.
        // I think it looks good enough but perhaps could be improved...
        ParticleSystem ps = benchParticles.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.loop = true;
        main.maxParticles = 100;
        main.duration = 5;
        ParticleSystem.EmissionModule em = ps.emission;
        em.rateOverTime = 20f;

        benchParticles.SetActive(true);
        StartEmission();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StopEmission();
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartEmission();
        }
    }

    public void SetEmission(bool enable)
    {
        LogInfo($"SetEmission: {enable}");
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.enabled = enable;

        if (enable && !ps.isPlaying)
        {
            ps.Play();
        }
    }

    public void StartEmission()
    {
        if (items == null || items.All(item => item.IsObtained()))
        {
            SetEmission(false);
            return;
        }

        if (items.All(item => item.WasEverObtained()))
        {
            SetColor(ItemChangerConstants.WasEverObtainedColor);
        }

        SetEmission(true);
    }

    public void StopEmission() => SetEmission(false);

    public void SetColor(Color color)
    {
        // I'm not sure which of these actually sets the color
        ParticleSystem.ColorOverLifetimeModule col = ps.colorOverLifetime;
        col.color = color;
        ParticleSystem.MainModule main = ps.main;
        main.startColor = color;
    }
}