namespace Buffs
{
    public class BeaconAuraAP : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Divineblessing_buf.troy", },
            BuffName = "Rally Aura AP",
            BuffTextureName = "Summoner_rally.dds",
        };
        float bonusHealth;
        float lastTimeExecuted;
        public BeaconAuraAP(float bonusHealth = default)
        {
            this.bonusHealth = bonusHealth;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            returnValue = false;
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusHealth);
            SpellEffectCreate(out _, out _, "summoner_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, bonusHealth);
            SetStunned(owner, true);
            SetMagicImmune(owner, true);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                //AddBuffToEachUnitInArea(owner, owner.Position3D, 850, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, attacker, new Buffs.BeaconAuraNoParticleAP(), BuffAddType.RENEW_EXISTING, BuffType.AURA, 1, 1, 1.1f, 0, false, true);
                foreach (var unit in GetUnitsInArea(owner, owner.Position3D, 850, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.BeaconAuraNoParticleAP(), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, default, default, false);
                }
            }
        }
    }
}