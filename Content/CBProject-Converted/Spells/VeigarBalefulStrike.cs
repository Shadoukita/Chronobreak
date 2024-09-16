namespace Spells
{
    public class VeigarBalefulStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 8f, 7f, 6f, 5f, 4f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 80, 125, 170, 215, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (!IsDead(target))
            {
                AddBuff((ObjAIBase)target, owner, new Buffs.VeigarBalefulStrike(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class VeigarBalefulStrike : BuffScript
    {
        object bonusAP;
        int[] effect0 = { 9999, 9999, 9999, 9999, 9999 };
        public VeigarBalefulStrike(object bonusAP = default)
        {
            this.bonusAP = bonusAP;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusAP);
            //RequireVar(this.maxBonus);
        }
        public override void OnDeactivate(bool expired)
        {
            if (IsDead(attacker))
            {
                SpellEffectCreate(out _, out _, "permission_ability_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnKill(AttackableUnit target)
        {
            if (charVars.TotalBonus < charVars.MaxBonus)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                float bonusAdd = 1 + charVars.TotalBonus;
                charVars.TotalBonus = bonusAdd;
                IncPermanentFlatMagicDamageMod(owner, 1);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                object nextBuffVars_BonusAP = bonusAP; // UNUSED
                charVars.MaxBonus = effect0[level - 1];
                float nextBuffVars_MaxBonus = charVars.MaxBonus; // UNUSED
            }
        }
    }
}