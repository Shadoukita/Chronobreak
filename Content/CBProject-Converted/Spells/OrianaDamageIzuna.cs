namespace Spells
{
    public class OrianaDamageIzuna : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
        object targetPos; // UNITIALIZED
        bool landed; // UNUSED
        float[] effect0 = { -0.24f, -0.28f, -0.32f, -0.36f, -0.4f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        int[] effect2 = { 60, 100, 140, 180, 220 };
        int[] effect3 = { 50, 80, 110, 140, 170 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell = false;
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaIzuna));
            if (spellName == nameof(Spells.OrianaIzuna))
            {
                correctSpell = true;
            }
            /*
            // Non-existent spells
            else if(spellName == nameof(Spells.Yomuizuna))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.OrianaFastIzuna))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.Yomufastizuna))
            {
                correctSpell = true;
            }
            */
            if (duration >= 0.001f)
            {
                if (correctSpell)
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    object targetPos = this.targetPos; // UNUSED
                    Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, default, true, (Champion)owner);
                    AddBuff(owner, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(owner, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float nextBuffVars_MovementSpeedMod = effect0[level - 1]; // UNUSED
                    int nextBuffVars_AttackSpeedMod = effect1[level - 1]; // UNUSED
                    foreach (AttackableUnit unit in GetUnitsInArea(attacker, other3.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaIzunaDamage), false))
                    {
                        BreakSpellShields(unit);
                        float baseDamage = effect2[level - 1];
                        float aP = GetFlatMagicDamageMod(owner);
                        float bonusDamage = aP * 0.5f;
                        float totalDamage = bonusDamage + baseDamage;
                        float nextBuffVars_TotalDamage = totalDamage;
                        AddBuff(attacker, unit, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    charVars.GhostAlive = false;
                    DestroyMissile(charVars.MissileID);
                    SpellBuffClear(owner, nameof(Buffs.OrianaIzuna));
                    landed = true;
                }
                else
                {
                    Say(owner, "SpellName: ", correctSpell);
                }
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.OrianaIzunaDamage)) == 0)
            {
                BreakSpellShields(target);
                float baseDamage = effect3[level - 1];
                float aP = GetFlatMagicDamageMod(owner);
                float bonusDamage = aP * 0.5f;
                float totalDamage = bonusDamage + baseDamage;
                totalDamage *= 1.25f;
                float nextBuffVars_TotalDamage = totalDamage;
                AddBuff(attacker, target, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class OrianaDamageIzuna : BuffScript
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
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            landed = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            Vector3 ownerCenter = GetUnitPosition(owner); // UNUSED
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
            missilePosition = GetMissilePosFromID(missileId);
        }
    }
}