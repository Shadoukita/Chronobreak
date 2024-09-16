namespace Spells
{
    public class SummonerBoostSpellShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 44f, 38f, 30f, 22f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class SummonerBoostSpellShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SpellBlock_eff.troy", },
            BuffName = "Spell Shield",
            BuffTextureName = "Summoner_boost.dds",
        };
        bool willRemove;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (willRemove)
                {
                    returnValue = false;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnUpdateStats()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (willRemove && damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                damageAmount = 0;
            }
        }
        public override void OnBeingSpellHit(ObjAIBase attacker, Spell spell, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts && owner.Team != attacker.Team)
            {
                willRemove = true;
                SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
            }
        }
    }
}