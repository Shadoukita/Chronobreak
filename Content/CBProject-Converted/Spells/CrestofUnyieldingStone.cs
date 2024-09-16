namespace Buffs
{
    public class CrestofUnyieldingStone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Unyielding Stone",
            BuffTextureName = "PlantKing_AnimateEntangler.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        float bonusDamage;
        float bonusResist;
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "Global_Invulnerability.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            float gameTimeSec = GetGameTime();
            float bonusResist = 0.0666f * gameTimeSec;
            float bonusDamage = 0.000333f * gameTimeSec;
            float resistFloored = Math.Max(40, bonusResist);
            float resistCapped = Math.Min(80, resistFloored);
            float damageFloored = Math.Max(0.2f, bonusDamage);
            float damageCapped = Math.Min(0.4f, damageFloored);
            this.bonusDamage = damageCapped;
            this.bonusResist = resistCapped;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectTurrets))
            {
                IncPermanentPercentPhysicalDamageMod(unit, this.bonusDamage);
                IncPermanentFlatArmorMod(unit, this.bonusResist);
                IncPermanentFlatSpellBlockMod(unit, this.bonusResist);
            }
            float tTDmg = 100 * this.bonusDamage;
            SetBuffToolTipVar(1, tTDmg);
            SetBuffToolTipVar(2, this.bonusResist);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            bonusDamage *= -1;
            bonusResist *= -1;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectTurrets))
            {
                IncPermanentPercentPhysicalDamageMod(unit, bonusDamage);
                IncPermanentFlatArmorMod(unit, bonusResist);
                IncPermanentFlatSpellBlockMod(unit, bonusResist);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (attacker is Champion && !IsDead(attacker))
            {
                AddBuff(attacker, attacker, new Buffs.CrestofUnyieldingStone(), 1, 1, 120, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            }
        }
    }
}