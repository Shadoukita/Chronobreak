namespace Spells
{
    public class FizzMarinerDoomMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        bool ultFired;
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if (ultFired)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 nextBuffVars_MissilePosition = missileEndPosition;
                bool nextBuffVars_willStick = true;
                AddBuff(owner, owner, new Buffs.FizzMarinerDoomMissile(nextBuffVars_willStick, nextBuffVars_MissilePosition), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                charVars.UltFired = false;
                ultFired = false;
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            DestroyMissile(missileNetworkID);
            BreakSpellShields(target);
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff(attacker, target, new Buffs.FizzMarinerDoomBomb(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            charVars.UltFired = false;
            if (GetBuffCountFromCaster(target, default, nameof(Buffs.FizzMarinerDoomBomb)) == 0)
            {
                level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 missileEndPosition = GetUnitPosition(target);
                Vector3 nextBuffVars_MissilePosition = missileEndPosition;
                bool nextBuffVars_willStick = false;
                AddBuff(owner, owner, new Buffs.FizzMarinerDoomMissile(nextBuffVars_willStick, nextBuffVars_MissilePosition), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class FizzMarinerDoomMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PersistsThroughDeath = true,
        };
        bool willStick;
        Vector3 missilePosition;
        EffectEmitter temp4;
        EffectEmitter temp3;
        EffectEmitter temp;
        EffectEmitter temp2;
        Region tempVision; // UNUSED
        bool exploded;
        int[] effect0 = { 200, 325, 450 };
        float[] effect1 = { -0.5f, -0.6f, -0.7f };
        public FizzMarinerDoomMissile(bool willStick = default, Vector3 missilePosition = default)
        {
            this.willStick = willStick;
            this.missilePosition = missilePosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.missilePosition);
            //RequireVar(this.willStick);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 groundPos = GetGroundHeight(missilePosition);
            SpellEffectCreate(out temp4, out temp3, "Fizz_Ring_Green.troy", "Fizz_Ring_Red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "head", groundPos, default, default, missilePosition, false, false, false, false, false);
            SpellEffectCreate(out temp, out temp2, "Fizz_UltimateMissile_Orbit.troy", "Fizz_UltimateMissile_Orbit.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "head", groundPos, default, default, missilePosition, false, false, false, false, false);
            tempVision = AddPosPerceptionBubble(teamID, 350, missilePosition, 3, default, false);
            exploded = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(temp);
            SpellEffectRemove(temp2);
            SpellEffectRemove(temp3);
            SpellEffectRemove(temp4);
            if (!exploded)
            {
                Vector3 nextBuffVars_CenterPos;
                float nextBuffVars_MoveSpeedMod;
                TeamId teamID = GetTeamID_CS(owner);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missilePosition, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, false, (ObjAIBase)owner);
                    nextBuffVars_CenterPos = missilePosition;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.FizzMoveback(nextBuffVars_CenterPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                    nextBuffVars_MoveSpeedMod = effect1[level - 1];
                    AddBuff(attacker, unit, new Buffs.FizzMarinerDoomSlow(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                Minion other1 = SpawnMinion("OMNOMNOMNOMONOM", "FizzShark", "idle.lua", missilePosition, teamID, true, true, true, true, true, true, 100, true, false, (Champion)owner);
                Vector3 groundPos = GetGroundHeight(missilePosition);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash.troy", "Fizz_SharkSplash.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, groundPos, default, default, groundPos, true, false, false, false, false);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash_Ground.troy ", "Fizz_SharkSplash_Ground.troy ", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, groundPos, default, default, groundPos, true, false, false, false, false);
                AddBuff(other1, other1, new Buffs.FizzShark(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                exploded = true;
            }
        }
        public override void OnUpdateActions()
        {
            if (willStick)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, missilePosition, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    float duration = GetBuffRemainingDuration(owner, nameof(Buffs.FizzMarinerDoomMissile));
                    SpellEffectRemove(temp);
                    SpellEffectRemove(temp2);
                    SpellEffectRemove(temp3);
                    SpellEffectRemove(temp4);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.FizzMarinerDoomBomb(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    exploded = true;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}