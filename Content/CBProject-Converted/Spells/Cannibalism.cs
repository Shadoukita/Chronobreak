namespace Spells
{
    public class Cannibalism : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.5f, 0.75f, 1 };
        float[] effect1 = { 0.25f, 0.375f, 0.5f };
        float[] effect2 = { 0.5f, 0.5f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_LifestealPercent = effect0[level - 1];
            float nextBuffVars_HealPercent = effect1[level - 1];
            float nextBuffVars_AttackSpeedMod = effect2[level - 1];
            AddBuff(attacker, target, new Buffs.Cannibalism(nextBuffVars_HealPercent, nextBuffVars_LifestealPercent, nextBuffVars_AttackSpeedMod), 1, 1, 20, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Cannibalism : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Cannibalism_buf.troy", },
            BuffTextureName = "Sion_Cannibalism.dds",
        };
        float healPercent;
        float lifestealPercent;
        float attackSpeedMod;
        public Cannibalism(float healPercent = default, float lifestealPercent = default, float attackSpeedMod = default)
        {
            this.healPercent = healPercent;
            this.lifestealPercent = lifestealPercent;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healPercent);
            //RequireVar(this.lifestealPercent);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, lifestealPercent);
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (target is ObjAIBase && target is not BaseTurret && damageSource == DamageSource.DAMAGE_SOURCE_ATTACK && target.Team != owner.Team)
            {
                SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                float healAmount = damageAmount * healPercent;
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    float temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                    if (temp1 < 1)
                    {
                        IncHealth(unit, healAmount, owner);
                        ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                    }
                    SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
                }
            }
        }
    }
}