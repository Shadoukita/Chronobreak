namespace CharScripts
{
    public class CharScriptNasus : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            int level = GetLevel(owner);
            if (level >= 11)
            {
                IncPercentLifeStealMod(owner, 0.2f);
            }
            else if (level >= 6)
            {
                IncPercentLifeStealMod(owner, 0.17f);
            }
            else
            {
                IncPercentLifeStealMod(owner, 0.14f);
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float damageBonus = 0 + charVars.DamageBonus;
                SetSpellToolTipVar(damageBonus, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.SoulEater(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.DamageBonus = 0;
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}