namespace Spells
{
    public class OlafAxeThrow : SpellScript
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
        float[] effect0 = { -0.24f, -0.28f, -0.32f, -0.36f, -0.4f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MovementSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            BreakSpellShields(target);
            AddBuff(owner, target, new Buffs.OlafSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.OlafAxeThrowDamage(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OlafAxeThrow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
        };
        Vector3 facingPos;
        Vector3 targetPos;
        float[] effect0 = { -0.24f, -0.28f, -0.32f, -0.36f, -0.4f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public OlafAxeThrow(Vector3 facingPos = default, Vector3 targetPos = default)
        {
            this.facingPos = facingPos;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.facingPos);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = this.targetPos;
            Minion other3 = SpawnMinion("HiddenMinion", "OlafAxe", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, false, (Champion)owner);
            Vector3 facingPos = this.facingPos;
            FaceDirection(other3, facingPos);
            float cooldownPerc = GetPercentCooldownMod(owner);
            float cooldownMult = 1 + cooldownPerc;
            float durationVar = 10 * cooldownMult;
            durationVar -= 0.5f;
            AddBuff(attacker, other3, new Buffs.OlafAxeExpirationTimer(), 1, 1, durationVar, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MovementSpeedMod = effect0[level - 1];
            int nextBuffVars_AttackSpeedMod = effect1[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, other3.Position3D, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.OlafSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.OlafAxeThrowDamage(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}