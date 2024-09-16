namespace Buffs
{
    public class CrestOfFlowingWater : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Flowing Water",
            BuffTextureName = "WaterWizard_Typhoon.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "invis_runes_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncPermanentPercentMovementSpeedMod(owner, 0.3f);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            IncPermanentPercentMovementSpeedMod(owner, -0.3f);
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
                    AddBuff(attacker, attacker, new Buffs.CrestOfFlowingWater(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
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
                    AddBuff(caster, caster, new Buffs.CrestOfFlowingWater(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}