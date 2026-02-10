using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module that provides a way to generate random numbers that are consistent for fixed inputs.
/// </summary>
[SingletonModule]
public sealed class ConsistentRandomnessModule : Module
{
    // TODO - this should be derived from the seed in rando
    public int Seed { get; set; } = (new Random()).Next(int.MinValue, int.MaxValue);

    protected override void DoLoad() { }

    protected override void DoUnload() { }

    /// <summary>
    /// Generate a random number between <paramref name="min"/> and <paramref name="max"/> inclusive,
    /// which is consistent for fixed inputs and <see cref="Seed"/>.
    /// 
    /// The random numbers generated for a fixed <paramref name="key"/> are correlated,
    /// so the key should be changed if the bounds change.
    /// </summary>
    public int Choose(string key, int min, int max)
    {
        uint hash = (uint)Seed ^ 2166136261U;

        if (key != null)
        {
            foreach (char c in key)
            {
                hash ^= (uint)c;
                hash *= 16777619U;
            }
        }

        hash ^= hash >> 16;
        hash *= 0x85ebca6b;
        hash ^= hash >> 13;
        hash *= 0xc2b2ae35;
        hash ^= hash >> 16;

        int range = max - min + 1;
        return min + (int)(hash % (uint)range);
    }
}
