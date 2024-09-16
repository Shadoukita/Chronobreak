namespace CharScripts
{
    public class CharScriptCaitlyn : CharScript
    {
        int[] effect0 = { 8, 8, 8, 8, 8, 8, 7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 6 };
        int[] effect1 = { 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4 };
        public override void OnUpdateActions()
        {
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 2;
            SetSpellToolTipVar(bonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void SetVarsByLevel()
        {
            charVars.TooltipAmount = effect0[level - 1];
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0 && hitResult != HitResult.HIT_Dodge)
            {
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.CaitlynHeadshot)) == 0)
                {
                    bool isInBrush = IsInBrush(attacker);
                    if (isInBrush)
                    {
                        AddBuff(attacker, attacker, new Buffs.CaitlynHeadshotCount(), 8, 2, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, attacker, new Buffs.CaitlynHeadshotCount(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
                else if (target is ObjAIBase and not BaseTurret)
                {
                    RemoveOverrideAutoAttack(owner, false);
                }
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            int level = GetLevel(owner);
            int brushCount = effect1[level - 1];
            bool isInBrush = IsInBrush(attacker);
            if (isInBrush)
            {
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.CaitlynHeadshotCount));
                if (count >= brushCount)
                {
                    AddBuff(owner, owner, new Buffs.CaitlynHeadshot(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.CaitlynHeadshotCount), 0);
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.CaitlynPiltoverPeacemaker))
            {
                charVars.PercentOfAttack = 1;
                AddBuff(owner, owner, new Buffs.CantAttack(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.CaitlynHeadshotpassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}