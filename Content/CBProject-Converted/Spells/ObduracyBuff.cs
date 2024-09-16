namespace Spells
{
    public class ObduracyBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ObduracyBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MalphiteDamageBuff",
            BuffTextureName = "Malphite_BrutalStrikes.dds",
        };
        float percMod;
        float damageIncrease;
        float armorIncrease;
        EffectEmitter sandroot;
        EffectEmitter sandRHand;
        EffectEmitter sandLHand;
        public ObduracyBuff(float percMod = default)
        {
            this.percMod = percMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percMod);
            float damageVar = GetTotalAttackDamage(owner);
            damageIncrease = damageVar * percMod;
            IncPermanentFlatPhysicalDamageMod(owner, damageIncrease);
            float armorVar = GetArmor(owner);
            armorIncrease = armorVar * percMod;
            IncPermanentFlatArmorMod(owner, armorIncrease);
            int malphiteSkinID = GetSkinID(owner);
            SpellEffectCreate(out sandroot, out _, "Malphite_Enrage_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            if (malphiteSkinID == 3)
            {
                SpellEffectCreate(out sandRHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_finger_b", default, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out sandLHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger_b", default, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out sandRHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_thumb_b", default, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out sandLHand, out _, "Malphite_Enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger_b", default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            damageIncrease *= -1;
            IncPermanentFlatPhysicalDamageMod(owner, damageIncrease);
            armorIncrease *= -1;
            IncPermanentFlatArmorMod(owner, armorIncrease);
            RemoveOverrideAutoAttack(owner, true);
            SpellEffectRemove(sandLHand);
            SpellEffectRemove(sandRHand);
            SpellEffectRemove(sandroot);
        }
    }
}