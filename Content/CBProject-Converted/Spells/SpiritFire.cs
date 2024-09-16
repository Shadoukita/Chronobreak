namespace Spells
{
    public class SpiritFire : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 55, 95, 135, 175, 215 };
        int[] effect2 = { -20, -25, -30, -35, -40 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            int nextBuffVars_InitialDamage = effect0[level - 1];
            int nextBuffVars_Damage = effect0[level - 1];
            Vector3 nextBuffVars_TargetPos = targetPos;
            int nextBuffVars_ArmorReduction = effect2[level - 1];
            AddBuff(attacker, attacker, new Buffs.SpiritFire(nextBuffVars_TargetPos, nextBuffVars_InitialDamage, nextBuffVars_Damage, nextBuffVars_ArmorReduction), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SpiritFire : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Vector3 targetPos;
        int initialDamage;
        int damage;
        int armorReduction;
        Region bubbleID; // UNUSED
        public SpiritFire(Vector3 targetPos = default, int initialDamage = default, int damage = default, int armorReduction = default)
        {
            this.targetPos = targetPos;
            this.initialDamage = initialDamage;
            this.damage = damage;
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.initialDamage);
            //RequireVar(this.damage);
            //RequireVar(this.armorReduction);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out _, out _, "nassus_spiritFire_warning.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            bubbleID = AddPosPerceptionBubble(teamID, 200, targetPos, 2.6f, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float nextBuffVars_InitialDamage = initialDamage;
            float nextBuffVars_Damage = damage;
            float nextBuffVars_ArmorReduction = armorReduction;
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, owner, new Buffs.SpiritFireAoE(nextBuffVars_InitialDamage, nextBuffVars_Damage, nextBuffVars_ArmorReduction, nextBuffVars_TargetPos), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}