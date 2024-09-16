namespace Spells
{
    public class XerathArcaneBarrage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        int[] effect1 = { 150, 200, 250, 0, 0 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            float nextBuffVars_Distance = distance; // UNUSED
            SpellEffectCreate(out _, out _, "Xerath_E_cas.troy", default, TeamId.TEAM_ORDER, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "chest", default, attacker, default, default, true, false, false, false, false);
            Minion other3 = SpawnMinion("HiddenMinion", "XerathArcaneBarrageLauncher", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            float nextBuffVars_SlowAmount = effect0[level - 1]; // UNUSED
            int nextBuffVars_DamageAmount = effect1[level - 1]; // UNUSED
            int nextBuffVars_Level = level; // UNUSED
            AddBuff(attacker, other3, new Buffs.XerathArcaneBarrage(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Region nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 600, targetPos, 4, default, false);
            AddBuff(owner, owner, new Buffs.XerathArcaneBarrageVision(nextBuffVars_Bubble), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(other3, owner, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
        }
    }
}
namespace Buffs
{
    public class XerathArcaneBarrage : BuffScript
    {
        EffectEmitter particle1;
        EffectEmitter particle;
        public override void OnActivate()
        {
            //RequireVar(this.level);
            //RequireVar(this.damageAmount);
            //RequireVar(this.slowAmount);
            //RequireVar(this.distance);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle1, out particle, "Xerath_E_cas_green.troy", "Xerath_E_cas_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, false, false, false, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}