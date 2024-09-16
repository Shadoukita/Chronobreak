namespace CharScripts
{
    public class CharScriptAshe : CharScript
    {
        float[] effect0 = { 0.03f, 0.03f, 0.03f, 0.06f, 0.06f, 0.06f, 0.09f, 0.09f, 0.09f, 0.12f, 0.12f, 0.12f, 0.15f, 0.15f, 0.15f, 0.18f, 0.18f, 0.18f };
        public override void OnUpdateStats()
        {
            float critToAdd = charVars.NumSecondsSinceLastCrit * charVars.CritPerSecond;
            IncFlatCritChanceMod(owner, critToAdd);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(3, ref charVars.LastCrit, false))
            {
                charVars.NumSecondsSinceLastCrit++;
            }
            if (!IsDead(owner) && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ArchersMark)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    AddBuff(owner, owner, new Buffs.ArchersMark(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
            float aD = GetTotalAttackDamage(owner);
            SetSpellToolTipVar(aD, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void SetVarsByLevel()
        {
            charVars.CritPerSecond = effect0[level - 1];
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                charVars.LastCrit = GetTime();
                charVars.NumSecondsSinceLastCrit = 0;
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string tempName = GetSpellName(spell);
            if (tempName == nameof(Spells.EnchantedCrystalArrow))
            {
                charVars.CastPoint = GetUnitPosition(owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Focus(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.BowMasterFocusDisplay(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}