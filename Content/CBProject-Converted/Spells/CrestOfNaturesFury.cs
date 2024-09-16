namespace Buffs
{
    public class CrestOfNaturesFury : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Natures Fury",
            BuffTextureName = "PlantKing_AnimateVitalis.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncPermanentPercentAttackSpeedMod(owner, 0.2f);
            IncPermanentPercentCooldownMod(owner, -0.1f);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentAttackSpeedMod(owner, -0.2f);
            IncPermanentPercentCooldownMod(owner, 0.1f);
            SpellEffectRemove(buffParticle);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            int count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            float newDuration = 60;
            if (attacker is Champion)
            {
                if (!IsDead(attacker))
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(attacker, attacker, new Buffs.CrestOfNaturesFury(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if (count != 0)
            {
                ObjAIBase caster = GetPetOwner((Pet)attacker);
                if (caster is Champion && !IsDead(caster))
                {
                    if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(caster, caster, new Buffs.CrestOfNaturesFury(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}