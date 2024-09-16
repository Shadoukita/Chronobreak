namespace Spells
{
    public class PoisonTrail : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 22, 34, 46, 58, 70 };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PoisonTrail)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PoisonTrail), owner, 0);
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 1);
            }
            else
            {
                Vector3 pos = GetUnitPosition(owner);
                Vector3 nextBuffVars_LastPosition = pos;
                int nextBuffVars_DamagePerTick = effect0[level - 1];
                float nextBuffVars_ManaCost = 13;
                AddBuff(owner, owner, new Buffs.PoisonTrail(nextBuffVars_LastPosition, nextBuffVars_DamagePerTick, nextBuffVars_ManaCost), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class PoisonTrail : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "bag_b", },
            AutoBuffActivateEffect = new[] { "AcidTrail_buf.troy", },
            BuffName = "Poison Trail",
            BuffTextureName = "ChemicalMan_AcidSpray.dds",
            SpellToggleSlot = 1,
        };
        Vector3 lastPosition;
        int damagePerTick;
        float manaCost;
        float lastTimeExecuted;
        public PoisonTrail(Vector3 lastPosition = default, int damagePerTick = default, float manaCost = default)
        {
            this.lastPosition = lastPosition;
            this.damagePerTick = damagePerTick;
            this.manaCost = manaCost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lastPosition);
            //RequireVar(this.damagePerTick);
            //RequireVar(this.manaCost);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            int nextBuffVars_DamagePerTick = damagePerTick;
            Minion other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)owner);
            AddBuff((ObjAIBase)owner, other3, new Buffs.PoisonTrailApplicator(nextBuffVars_DamagePerTick), 1, 1, 3.25f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            lastPosition = curPos;
        }
        public override void OnUpdateActions()
        {
            Vector3 curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            float distance = DistanceBetweenPoints(curPos, lastPosition);
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float ownerMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if (ownerMana < manaCost)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float negManaCost = -1 * manaCost;
                    IncPAR(owner, negManaCost, PrimaryAbilityResourceType.MANA);
                    if (distance <= 90)
                    {
                        TeamId teamID = GetTeamID_CS(attacker);
                        Vector3 frontPos = GetPointByUnitFacingOffset(owner, 35, 0);
                        float nextBuffVars_DamagePerTick = damagePerTick;
                        Minion other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", frontPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)attacker);
                        AddBuff(attacker, other3, new Buffs.PoisonTrailApplicator(nextBuffVars_DamagePerTick), 1, 1, 3.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                        lastPosition = curPos;
                    }
                }
            }
            if (distance >= 90)
            {
                TeamId teamID = GetTeamID_CS(attacker);
                float nextBuffVars_DamagePerTick = damagePerTick;
                Minion other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)attacker);
                AddBuff(attacker, other3, new Buffs.PoisonTrailApplicator(nextBuffVars_DamagePerTick), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                lastPosition = curPos;
            }
        }
    }
}