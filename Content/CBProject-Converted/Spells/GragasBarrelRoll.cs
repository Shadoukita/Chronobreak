namespace Spells
{
    public class GragasBarrelRoll : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 85, 140, 195, 250, 305 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            int nextBuffVars_DamageLevel = effect0[level - 1]; // UNUSED
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, owner, new Buffs.GragasBarrelRoll(nextBuffVars_TargetPos), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellCast(attacker, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class GragasBarrelRoll : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasBarrelRoll",
            BuffTextureName = "GragasBarrelRoll.dds",
            SpellToggleSlot = 1,
        };
        Vector3 targetPos;
        public GragasBarrelRoll(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if (spellName == nameof(Spells.GragasBarrelRollMissile))
            {
                TeamId teamID = GetTeamID_CS(owner);
                int gragasSkinID = GetSkinID(owner);
                Vector3 targetPos = this.targetPos;
                Minion other1 = SpawnMinion("DoABarrelRoll", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, true, (Champion)owner);
                int nextBuffVars_SkinID = gragasSkinID;
                AddBuff(other1, owner, new Buffs.GragasBarrelRollBoom(nextBuffVars_SkinID), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                nextBuffVars_SkinID = gragasSkinID;
                AddBuff(other1, other1, new Buffs.GragasBarrelRollRender(), 1, 1, 20, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}