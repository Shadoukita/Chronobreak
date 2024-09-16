namespace Spells
{
    public class UrgotPlasmaGrenadeBoom : SpellScript
    {
        int[] effect0 = { 5, 5, 5, 5, 5 };
        int[] effect1 = { 75, 130, 185, 240, 295 };
        float[] effect2 = { -0.12f, -0.14f, -0.16f, -0.18f, -0.2f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos = GetUnitPosition(target);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId teamID = GetTeamID_CS(owner);
            float buffDuration = effect0[level - 1];
            SpellEffectCreate(out _, out _, "UrgotPlasmaGrenade_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            float aD = GetFlatPhysicalDamageMod(owner);
            float dmg = effect1[level - 1];
            float bonusDamage = aD * 0.6f;
            float totalDamage = bonusDamage + dmg;
            float remainder = buffDuration % 0.5f;
            float ticks = buffDuration - remainder;
            float tickDamage = totalDamage / ticks;
            float nextBuffVars_ArmorReduced = effect2[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(owner, targetPos, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                float nextBuffVars_TickDamage;
                bool isStealthed = GetStealthed(unit);
                if (!isStealthed)
                {
                    BreakSpellShields(unit);
                    nextBuffVars_TickDamage = tickDamage;
                    AddBuff(owner, unit, new Buffs.UrgotCorrosiveDebuff(nextBuffVars_TickDamage), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, true, false);
                    AddBuff(owner, unit, new Buffs.UrgotPlasmaGrenadeBoom(nextBuffVars_ArmorReduced), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, true, false);
                }
                else
                {
                    if (unit is Champion)
                    {
                        BreakSpellShields(unit);
                        nextBuffVars_TickDamage = tickDamage;
                        AddBuff(owner, unit, new Buffs.UrgotCorrosiveDebuff(nextBuffVars_TickDamage), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, true, false);
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, unit);
                        if (canSee)
                        {
                            BreakSpellShields(unit);
                            nextBuffVars_TickDamage = tickDamage;
                            AddBuff(owner, unit, new Buffs.UrgotCorrosiveDebuff(nextBuffVars_TickDamage), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, true, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class UrgotPlasmaGrenadeBoom : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrgotCorrosiveDebuff",
            BuffTextureName = "UrgotCorrosiveCharge.dds",
        };
        EffectEmitter hitParticle;
        float armorReduced;
        public UrgotPlasmaGrenadeBoom(float armorReduced = default)
        {
            this.armorReduced = armorReduced;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out hitParticle, out _, "UrgotPlasmaGrenade_hit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.armorReduced);
            IncPercentArmorMod(owner, armorReduced);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(hitParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, armorReduced);
        }
    }
}