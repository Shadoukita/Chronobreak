namespace Spells
{
    public class CannonBarrageBall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 75, 120, 165 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, true);
            float nextBuffVars_DamageAmount = effect0[level - 1];
            AddBuff(attacker, other3, new Buffs.CannonBarrageBall(nextBuffVars_DamageAmount), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CannonBarrageBall : BuffScript
    {
        float damageAmount;
        public CannonBarrageBall(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmount);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
            Vector3 ownerPos = GetUnitPosition(owner);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "pirate_cannonBarrage_point.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            ObjAIBase attacker = base.attacker;
            Vector3 ownerPos = GetUnitPosition(owner);
            TeamId teamID = GetTeamID_CS(owner);
            attacker = GetChampionBySkinName("Gangplank", teamID);
            SpellEffectCreate(out _, out _, "pirate_cannonBarrage_tar.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 265, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0.8f, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}