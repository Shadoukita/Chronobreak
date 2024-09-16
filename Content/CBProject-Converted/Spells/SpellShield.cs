namespace Spells
{
    public class SivirE: SpellShield {}
    public class SpellShield : SpellScript
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
            AddBuff(owner, owner, new Buffs.SpellShield(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class SpellShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SpellBlock_eff.troy", },
            BuffName = "Spell Shield",
            BuffTextureName = "Sivir_SpellBlock.dds",
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
                SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                willRemove = true;
                return false;
            }
            return true;
        }
        public override void OnUpdateStats()
        {
            if (willRemove)
            {
                IncPAR(owner, 150);
                SpellBuffRemoveCurrent(owner);
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
                        SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    }
                    else if (spellVars.DoesntBreakShields)
                    {
                    }
                    else if (!spellVars.DoesntTriggerSpellCasts)
                    {
                        willRemove = true;
                        SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    }
                }
            }
        }
    }
}