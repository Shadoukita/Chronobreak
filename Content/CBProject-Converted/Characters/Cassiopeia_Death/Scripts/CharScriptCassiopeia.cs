namespace CharScripts
{
    public class CharScriptCassiopeia : CharScript
    {
        float lastTimeExecuted;
        EffectEmitter particle; // UNUSED
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.4f, ref lastTimeExecuted, false))
            {
                if (IsDead(owner))
                {
                    AddBuff(owner, owner, new Buffs.CassiopeiaDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CassiopeiaDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.CassiopeiaDeathParticle), owner);
                    }
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                AddBuff(owner, owner, new Buffs.CassiopeiaDeadlyCadence(), 5, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellEffectCreate(out particle, out _, "CassDeadlyCadence_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "root", default, attacker, default, default, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            charVars.SecondSkinArmor = 11;
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnResurrect()
        {
            SpellBuffRemove(owner, nameof(Buffs.CassiopeiaDeathParticle), owner);
        }
    }
}