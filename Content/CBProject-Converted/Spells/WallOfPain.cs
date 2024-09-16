namespace Spells
{
    public class KarthusWallOfPain: WallOfPain {}
    public class WallOfPain : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 17, 19, 21, 23, 25 };
        int[] effect1 = { 800, 900, 1000, 1100, 1200 };
        float[] effect2 = { -0.4f, -0.5f, -0.6f, -0.7f, -0.8f };
        int[] effect3 = { -15, -20, -25, -30, -35 };
        public override void SelfExecute()
        {
            Vector3 firstPos = Vector3.Zero;
            Vector3 lastPos = Vector3.Zero;
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            TeamId teamID = GetTeamID_CS(owner);
            int iterations = effect0[level - 1];
            float lineWidth = effect1[level - 1];
            bool foundFirstPos = false;
            float nextBuffVars_MoveSpeedMod = effect2[level - 1];
            float nextBuffVars_ArmorMod = effect3[level - 1];
            foreach (Vector3 pos in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, iterations))
            {
                Vector3 nextBuffVars_Pos = pos;
                AddBuff(owner, owner, new Buffs.WallOfPain(nextBuffVars_MoveSpeedMod, nextBuffVars_ArmorMod, nextBuffVars_Pos), 50, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0.1f, true, false, false);
                if (!foundFirstPos)
                {
                    firstPos = pos;
                    foundFirstPos = true;
                }
                lastPos = pos;
            }
            Minion other1 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", firstPos, teamID, false, true, false, false, false, true, 300, false, true, (Champion)owner);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Minion other2 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", lastPos, teamID, false, true, false, false, false, true, 300, false, true, (Champion)owner);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other1, other2, new Buffs.WallOfPainBeam(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other2);
            Minion other3 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", targetPos, teamID, false, true, false, false, false, true, 300 + lineWidth, false, true, (Champion)owner);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other3);
            LinkVisibility(other2, other3);
        }
    }
}
namespace Buffs
{
    public class WallOfPain : BuffScript
    {
        float moveSpeedMod;
        float armorMod;
        Vector3 pos;
        float lastTimeExecuted;
        public WallOfPain(float moveSpeedMod = default, float armorMod = default, Vector3 pos = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.armorMod = armorMod;
            this.pos = pos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.armorMod);
            //RequireVar(this.pos);
            Vector3 pos = this.pos;
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            float nextBuffVars_ArmorMod = armorMod;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.WallofPainTarget(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.WallofPainExtra(nextBuffVars_ArmorMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.WallOfPainMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.1f, ref lastTimeExecuted, false))
            {
                Vector3 pos = this.pos;
                float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                float nextBuffVars_ArmorMod = armorMod;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.WallofPainTarget), false))
                {
                    if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.WallOfPainMarker)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.WallofPainTarget(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.WallofPainExtra(nextBuffVars_ArmorMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                        AddBuff(attacker, unit, new Buffs.WallOfPainMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}