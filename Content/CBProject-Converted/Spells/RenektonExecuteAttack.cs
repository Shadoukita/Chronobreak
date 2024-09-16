namespace Spells
{
    public class RenektonExecuteAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 35, 70, 105, 140, 175 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_BonusDamage = effect0[level - 1];
            SpellBuffRemove(owner, nameof(Buffs.RenektonPreExecute), owner);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                float ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
                if (ragePercent >= 0.5f)
                {
                    SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                    charVars.Swung = true;
                }
                else
                {
                    SpellCast(attacker, target, default, default, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                    charVars.Swung = true;
                }
            }
            //damageAmount *= 0;
        }
    }
}