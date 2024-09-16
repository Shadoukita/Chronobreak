namespace Spells
{
    public class FizzMarinerDoomBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class FizzMarinerDoomBomb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Fizz_UltimateMissile_Orbit.troy", "", },
            BuffName = "FizzChurnTheWatersCling",
            BuffTextureName = "FizzMarinerDoom.dds",
        };
        Region tempID;
        EffectEmitter temp2;
        EffectEmitter temp;
        float tickDamage;
        float lastTimeExecuted;
        float[] effect0 = { -0.5f, -0.6f, -0.7f };
        int[] effect1 = { 200, 325, 450 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            tempID = AddUnitPerceptionBubble(teamID, 300, owner, 4, default, default, false);
            SpellEffectCreate(out temp2, out temp, "Fizz_Ring_Green.troy", "Fizz_Ring_Red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, owner, default, default, true, false, true, false, false);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            AddBuff(attacker, owner, new Buffs.FizzMarinerDoomSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            tickDamage = 3;
        }
        /*
        //TODO: Uncomment and fix
        public override void OnDeactivate(bool expired)
        {
            int level;
            float nextBuffVars_MoveSpeedMod;
            Vector3 nextBuffVars_CenterPos;
            SpellEffectRemove(this.temp);
            SpellEffectRemove(this.temp2);
            RemovePerceptionBubble(this.tempID);
            if(expired)
            {
                AttackableUnit other1; // UNITIALIZED
                TeamId teamID = GetTeamID(attacker);
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, false, attacker);
                    nextBuffVars_MoveSpeedMod = this.effect0[level - 1];
                    AddBuff(attacker, unit, new Buffs.FizzMarinerDoomSlow(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    if(unit != owner)
                    {
                        Vector3 ownerPos = GetUnitPosition(owner);
                        nextBuffVars_CenterPos = ownerPos;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.FizzMoveback(nextBuffVars_CenterPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                    }
                    else
                    {
                        AddBuff(attacker, owner, new Buffs.FizzKnockup(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                    }
                }
                teamID = GetTeamID(attacker);
                Vector3 targetPos = GetUnitPosition(owner);
                Minion other3 = SpawnMinion("Omnomnomnom", "FizzShark", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, false, false, (Champion)attacker);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash.troy", "Fizz_SharkSplash.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, "BUFFBONE_CSTM_GROUND", targetPos, other1, "BUFFBONE_CSTM_GROUND", targetPos, true, false, false, false, false);
                Vector3 groundPos = GetGroundHeight(targetPos);
                SpellEffectCreate(out temp, out temp, "Fizz_SharkSplash_Ground.troy ", "Fizz_SharkSplash_Ground.troy ", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, groundPos, default, default, groundPos, true, false, false, false, false);
                AddBuff(other3, other3, new Buffs.FizzShark(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float remDuration = GetBuffRemainingDuration(owner, nameof(Buffs.FizzMarinerDoomBomb));
                bool nextBuffVars_willStick = false;
                Vector3 missileEndPosition = GetUnitPosition(owner);
                Vector3 nextBuffVars_MissilePosition = missileEndPosition;
                AddBuff(attacker, attacker, new Buffs.FizzMarinerDoomMissile(nextBuffVars_willStick, nextBuffVars_MissilePosition), 1, 1, remDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                charVars.UltFired = false;
            }
        }
        */
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                if (tickDamage > 0)
                {
                    float nextBuffVars_TickDamage = tickDamage;
                    AddBuff(attacker, owner, new Buffs.TimeBombCountdown(nextBuffVars_TickDamage), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, owner, tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                    tickDamage--;
                }
            }
        }
    }
}