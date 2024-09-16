namespace Buffs
{
    public class CrestoftheAncientGolem : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "CrestoftheAncientGolem",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        int cooldownVar; // UNUSED
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            cooldownVar = 0;
            SetBuffToolTipVar(1, 20);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            if (owner is Champion)
            {
                IncPercentCooldownMod(owner, -0.2f);
                float maxMana = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
                float manaRegen = maxMana * 0.01f;
                IncFlatPARRegenMod(owner, 5 + manaRegen, PrimaryAbilityResourceType.MANA);
                float maxEnergy = GetMaxPAR(target, PrimaryAbilityResourceType.Energy);
                float energyRegen = maxEnergy * 0.01f;
                IncFlatPARRegenMod(owner, 5 + energyRegen, PrimaryAbilityResourceType.Energy);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float newDuration;
            int count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            if (attacker is Champion)
            {
                if (!IsDead(attacker))
                {
                    newDuration = 150;
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(attacker, attacker, new Buffs.CrestoftheAncientGolem(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if (count != 0)
            {
                ObjAIBase caster = GetPetOwner((Pet)attacker);
                if (caster is Champion && !IsDead(caster))
                {
                    newDuration = 150;
                    if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(caster, caster, new Buffs.CrestoftheAncientGolem(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}