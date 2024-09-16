namespace Spells
{
    public class YorickDeathGrip : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 19, 19, 19, 19, 19 };
        int[] effect1 = { 900, 900, 900, 900, 900 };
        int[] effect2 = { 200, 300, 400, 400, 400 };
        float[] effect3 = { 0.5f, 0.5f, 0.5f };
        int[] effect4 = { 5, 7, 9 };
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
            int nextBuffVars_DamageToDeal = effect2[level - 1];
            foreach (Vector3 pos in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, iterations))
            {
                Vector3 nextBuffVars_Pos = pos;
                AddBuff(owner, owner, new Buffs.YorickDeathGripDelay(nextBuffVars_DamageToDeal, nextBuffVars_Pos), 50, 1, effect3[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                if (!foundFirstPos)
                {
                    firstPos = pos;
                    foundFirstPos = true;
                }
                lastPos = pos;
            }
            Minion other1 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", firstPos, teamID, false, true, false, false, false, true, 300, default, true, (Champion)owner);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Minion other2 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", lastPos, teamID, false, true, false, false, false, true, 300, default, true, (Champion)owner);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int nextBuffVars_DurationLevel = effect4[level - 1];
            AddBuff(other1, other2, new Buffs.YorickDeathGripBeamDelay(nextBuffVars_DurationLevel), 1, 1, effect3[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other2);
            Minion other3 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", targetPos, teamID, false, true, false, false, false, true, 300 + lineWidth, default, true, (Champion)owner);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, effect4[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            LinkVisibility(other1, other3);
            LinkVisibility(other2, other3);
        }
    }
}
namespace Buffs
{
    public class YorickDeathGrip : BuffScript
    {
        float damageToDeal;
        Vector3 pos;
        float lastTimeExecuted;
        public YorickDeathGrip(float damageToDeal = default, Vector3 pos = default)
        {
            this.damageToDeal = damageToDeal;
            this.pos = pos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            //RequireVar(this.pos);
            Vector3 pos = this.pos;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, default, nameof(Buffs.YorickDeathGripExtra)) == 0)
                {
                    AddBuff(attacker, unit, new Buffs.YorickDeathGripExtra(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.YorickDeathGripTarget(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SNARE, 0, true, false, false);
                }
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.1f, ref lastTimeExecuted, false))
            {
                Vector3 pos = this.pos;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, pos, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (GetBuffCountFromCaster(unit, default, nameof(Buffs.YorickDeathGripExtra)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.YorickDeathGripExtra(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.YorickDeathGripTarget(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SNARE, 0, true, false, false);
                    }
                }
            }
        }
    }
}