namespace Spells
{
    public class BantamTrapDetonate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class BantamTrapDetonate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teemoTeam = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "ShroomMine.troy", default, teemoTeam, 300, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                float nextBuffVars_AttackSpeedMod = 0;
                float nextBuffVars_MoveSpeedMod = -0.6f;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
            }
            ApplyDamage((ObjAIBase)owner, owner, 500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}