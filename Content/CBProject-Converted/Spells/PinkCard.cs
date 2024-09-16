namespace Buffs
{
    public class PinkCard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        bool willRemove;
        EffectEmitter effectID;
        int[] effect0 = { 50, 100, 150, 200, 250 };
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
            SpellEffectCreate(out effectID, out _, "Card_Yellow_Tag.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(effectID);
            float baseCooldown = 4;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (target is ObjAIBase)
            {
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
            {
                BreakSpellShields(unit);
                ApplyStun(attacker, unit, 1.25f);
                ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0);
            }
            willRemove = true;
        }
    }
}