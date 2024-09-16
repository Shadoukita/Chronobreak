namespace Spells
{
    public class NocturneShroudofDarkness : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        bool willRemove; // UNUSED
        public override void SelfExecute()
        {
            willRemove = false;
            AddBuff(owner, owner, new Buffs.NocturneShroudofDarkness(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 1, owner, false, true, true);
        }
    }
}
namespace Buffs
{
    public class NocturneShroudofDarkness : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "C_BUFFBONE_GLB_CENTER_LOC", "Head", "", "", },
            AutoBuffActivateEffect = new[] { "nocturne_shroudofDarkness_shield.troy", "nocturne_shroudofDarkness_shield_cas_02.troy", "nocturne_shroudofDarkness_shield_cas_ground.troy", "", },
            BuffName = "NocturneShroudofDarknessShield",
            BuffTextureName = "Nocturne_ShroudofDarkness.dds",
        };
        bool willRemove;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if (willRemove)
            {
                return owner.Team == attacker.Team;
            }
            else if (duration == 37037)
            {
                SpellEffectCreate(out _, out _, "nocturne_shroud_deactivateTrigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
                willRemove = true;
                return false;
            }
            return true;
        }
        public override void OnUpdateStats()
        {
            if (willRemove)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneShroudofDarknessBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.NocturneShroudofDarkness), (ObjAIBase)owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (willRemove)
            {
                damageAmount = 0;
            }
        }
        public override void OnBeingSpellHit(ObjAIBase attacker, Spell spell, SpellScriptMetadata spellVars)
        {
            SetTriggerUnit(attacker);
            ObjAIBase owner = GetBuffCasterUnit();
            if (owner.Team != attacker.Team)
            {
                bool isAttack = GetIsAttackOverride(spell);
                if (!isAttack)
                {
                    if (!spellVars.DoesntBreakShields)
                    {
                        willRemove = true;
                        SpellEffectCreate(out _, out _, "nocturne_shroud_deactivateTrigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
                    }
                    else if (spellVars.DoesntBreakShields)
                    {
                    }
                    else if (!spellVars.DoesntTriggerSpellCasts)
                    {
                        willRemove = true;
                        SpellEffectCreate(out _, out _, "nocturne_shroud_deactivateTrigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
                    }
                }
            }
        }
    }
}