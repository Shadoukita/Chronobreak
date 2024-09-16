namespace Spells
{
    public class MegaAdhesive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = { 5.5f, 5.5f, 5.5f, 5.5f, 5.5f };
        float[] effect1 = { -0.35f, -0.45f, -0.55f, -0.65f, -0.75f };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 castPos = GetSpellTargetPos(spell);
            Minion other2 = SpawnMinion("k", "SpellBook1", "idle.lua", castPos, teamID, true, true, false, false, true, false, 0, default, true);
            float nextBuffVars_Duration = effect0[level - 1];
            float nextBuffVars_SlowPercent = effect1[level - 1];
            AddBuff(attacker, other2, new Buffs.MegaAdhesive(nextBuffVars_Duration, nextBuffVars_SlowPercent), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1 + nextBuffVars_Duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class MegaAdhesive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "RunePrison_tar.troy", },
        };
        float duration;
        float slowPercent;
        public MegaAdhesive(float duration = default, float slowPercent = default)
        {
            this.duration = duration;
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.duration);
            //RequireVar(this.slowPercent);
        }
        public override void OnDeactivate(bool expired)
        {
            float nextBuffVars_Duration = duration; // UNUSED
            float nextBuffVars_SlowPercent = slowPercent;
            AddBuff(attacker, owner, new Buffs.MegaAdhesiveApplicator(nextBuffVars_SlowPercent), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}