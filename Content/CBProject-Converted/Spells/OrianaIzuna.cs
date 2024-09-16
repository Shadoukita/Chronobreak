namespace Spells
{
    public class OrianaIzuna : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
        bool landed; // UNUSED
        object targetPos; // UNITIALIZED
        int[] effect0 = { 60, 100, 140, 180, 220 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell = false;
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaIzuna));
            if (spellName == nameof(Spells.OrianaIzuna))
            {
                correctSpell = true;
            }
            charVars.GhostAlive = false;
            SpellBuffClear(owner, nameof(Buffs.OrianaIzuna));
            landed = true;
            if (duration >= 0.001f && correctSpell)
            {
                TeamId teamID = GetTeamID_CS(owner);
                object targetPos = this.targetPos; // UNUSED
                Minion other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, false, true, (Champion)owner);
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out _, out _, "Oriana_Izuna_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, default, default, missileEndPosition, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, other3.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    float baseDamage = effect0[level - 1];
                    float aP = GetFlatMagicDamageMod(owner);
                    float bonusDamage = aP * 0.6f;
                    float totalDamage = bonusDamage + baseDamage;
                    totalDamage *= charVars.IzunaPercent;
                    charVars.IzunaPercent *= 0.9f;
                    charVars.IzunaPercent = Math.Max(0.4f, charVars.IzunaPercent);
                    float nextBuffVars_TotalDamage = totalDamage;
                    if (GetBuffCountFromCaster(unit, default, nameof(Buffs.OrianaIzunaDamage)) == 0)
                    {
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
            DestroyMissile(charVars.MissileID);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, default, nameof(Buffs.OrianaIzunaDamaged)) == 0)
            {
                float baseDamage = effect0[level - 1];
                float aP = GetFlatMagicDamageMod(owner);
                float bonusDamage = aP * 0.6f;
                float totalDamage = bonusDamage + baseDamage;
                totalDamage *= charVars.IzunaPercent;
                charVars.IzunaPercent *= 0.9f;
                charVars.IzunaPercent = Math.Max(0.4f, charVars.IzunaPercent);
                BreakSpellShields(target);
                float nextBuffVars_TotalDamage = totalDamage;
                AddBuff(attacker, target, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class OrianaIzuna : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
        };
        bool landed; // UNUSED
        Vector3 missilePosition; // UNUSED
        public override void OnActivate()
        {
            //RequireVar(this.castPos);
            //RequireVar(this.targetPos);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            landed = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            Vector3 ownerCenter = GetUnitPosition(owner); // UNUSED
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
            missilePosition = GetMissilePosFromID(missileId);
        }
    }
}