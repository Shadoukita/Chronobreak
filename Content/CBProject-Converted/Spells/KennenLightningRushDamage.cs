namespace Buffs
{
    public class KennenLightningRushDamage : BuffScript
    {
        float rushDamage;
        public KennenLightningRushDamage(float rushDamage = default)
        {
            this.rushDamage = rushDamage;
        }
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            //RequireVar(this.rushDamage);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID = GetTeamID_CS(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.KennenLightningRushMarker)) == 0)
                {
                    float aPValue = GetFlatMagicDamageMod(owner);
                    float aPMod = aPValue * 0.6f;
                    float totalDamage = rushDamage + aPMod;
                    float minionDamage = totalDamage / 2;
                    AddBuff(attacker, unit, new Buffs.KennenLightningRushMarker(), 1, 1, 2.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    if (unit is Champion)
                    {
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                        SpellEffectCreate(out _, out _, "kennen_lr_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, default, default, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage(attacker, unit, minionDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                        SpellEffectCreate(out _, out _, "kennen_lr_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, default, default, false, false);
                    }
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KennenLREnergy)) == 0)
                    {
                        IncPAR(owner, 40, PrimaryAbilityResourceType.Energy);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.KennenLREnergy(), 1, 1, 2.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}