namespace Buffs
{
    public class NocturneDuskbringerTrail : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NocturneDuskbringerTrail",
            BuffTextureName = "Nocturne_Duskbringer.dds",
        };
        Vector3 lastPosition;
        float lastTimeExecuted;
        float[] effect0 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        int[] effect1 = { 25, 35, 45, 55, 65 };
        public NocturneDuskbringerTrail(Vector3 lastPosition = default)
        {
            this.lastPosition = lastPosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lastPosition);
        }
        public override void OnUpdateActions()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                Vector3 curPos = GetPointByUnitFacingOffset(owner, 0, 0);
                float distance = DistanceBetweenPoints(lastPosition, curPos);
                if (distance > 25)
                {
                    Minion other3 = SpawnMinion("DarkPath", "testcube", "idle.lua", curPos, casterID, true, true, true, true, false, true, 0, default, true);
                    float nextBuffVars_HastePercent = effect0[level - 1];
                    int nextBuffVars_BonusAD = effect1[level - 1];
                    AddBuff(attacker, other3, new Buffs.NocturneDuskbringer(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    lastPosition = curPos;
                }
            }
        }
    }
}