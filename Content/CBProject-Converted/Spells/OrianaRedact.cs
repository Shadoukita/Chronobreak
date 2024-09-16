namespace Spells
{
    public class OrianaRedact : SpellScript
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
        bool hit; // UNUSED
        int[] effect0 = { 80, 120, 160, 200, 240 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell = false;
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaRedact));
            if (spellName == nameof(Spells.OrianaRedact))
            {
                correctSpell = true;
            }
            else if (spellName == nameof(Spells.OrianaRedact))
            {
                correctSpell = true;
            }
            if (correctSpell)
            {
                hit = true;
                charVars.GhostAlive = false;
                SpellBuffClear(owner, nameof(Buffs.OrianaRedact));
            }
            if (duration >= 0.01f)
            {
                bool found = false;
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, missileEndPosition, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
                {
                    found = true;
                }
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, missileEndPosition, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhostSelf), true))
                {
                    found = true;
                }
                if (!found)
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out _, out _, "Orianna_Ball_Flash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", missileEndPosition, owner, default, default, false, false, false, false, false);
                    AddBuff(owner, owner, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "Orianna_Ball_Flash_Reverse.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, caster, false, owner, "SpinnigBottomRidge", default, owner, default, default, false, false, false, false, false);
                }
            }
            DestroyMissile(charVars.MissileID);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_DamageBlock;
            float nextBuffVars_TotalDamage;
            float baseDamageBlock = effect0[level - 1];
            float selfAP = GetFlatMagicDamageMod(owner);
            float bonusShield = selfAP * 0.4f;
            float totalShield = bonusShield + baseDamageBlock;
            float halfShield = 0.75f * totalShield;
            if (target != owner)
            {
                if (GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaRedactTarget)) > 0)
                {
                    if (!IsDead(target))
                    {
                        AddBuff(owner, target, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        nextBuffVars_DamageBlock = totalShield;
                        AddBuff(owner, target, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        SpellBuffClear(target, nameof(Buffs.OrianaRedactTarget));
                    }
                    DestroyMissile(missileNetworkID);
                }
                else if (target.Team != owner.Team)
                {
                    nextBuffVars_TotalDamage = halfShield;
                    AddBuff(attacker, target, new Buffs.OrianaRedactDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (target is Champion)
                {
                }
            }
            else
            {
                if (GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaRedactTarget)) > 0)
                {
                    if (!IsDead(owner))
                    {
                        AddBuff(owner, owner, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        nextBuffVars_DamageBlock = totalShield;
                        AddBuff(owner, owner, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        SpellBuffClear(target, nameof(Buffs.OrianaRedactTarget));
                    }
                    DestroyMissile(missileNetworkID);
                }
            }
        }
    }
}
namespace Buffs
{
    public class OrianaRedact : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Malady",
            BuffTextureName = "3114_Malady.dds",
        };
        bool hit; // UNUSED
        public override void OnActivate()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            hit = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            DestroyMissile(charVars.MissileID);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
        }
    }
}