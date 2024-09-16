namespace Spells
{
    public class UndyingRage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 110f, 100f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "TryndamereDemonsword", },
            SpellVOOverrideSkins = new[] { "TryndamereDemonsword", },
        };
        int[] effect0 = { 50, 75, 100 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.UndyingRage(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncPAR(owner, effect0[level - 1], PrimaryAbilityResourceType.Other);
        }
    }
}
namespace Buffs
{
    public class UndyingRage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", "", "", "spine", },
            AutoBuffActivateEffect = new[] { "UndyingRage_buf.troy", "", "", "UndyingRageSpine_glow.troy", },
            BuffName = "Undying Rage",
            BuffTextureName = "DarkChampion_EndlessRage.dds",
            NonDispellable = true,
            OnPreDamagePriority = 2,
        };
        EffectEmitter a;
        EffectEmitter b;
        public override void OnActivate()
        {
            OverrideAnimation("run", "run2", owner);
            SpellEffectCreate(out a, out _, "UndyingRage_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out b, out _, "UndyingRage_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent <= 0.03f)
            {
                float health = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float healthFactor = maxHealth * 0.03f;
                float healthToInc = healthFactor - health;
                IncHealth(owner, healthToInc, owner);
            }
            ClearOverrideAnimation("Run", owner);
            SpellEffectRemove(a);
            SpellEffectRemove(b);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (curHealth <= damageAmount)
            {
                damageAmount = curHealth - 1;
                Say(owner, "game_lua_UndyingRage");
            }
        }
    }
}