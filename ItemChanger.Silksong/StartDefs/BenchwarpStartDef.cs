using Benchwarp.Benches;

namespace ItemChanger.Silksong.StartDefs;

public class BenchwarpStartDef(BenchData BenchData): StartDef
{
    public override RespawnInfo GetRespawnInfo() => BenchData.RespawnInfo.GetRespawnInfo();
}
