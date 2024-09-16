namespace Spells
{
    public class Rupture : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "DandyChogath", },
        };
        int[] effect0 = { 80, 135, 190, 245, 305 };
        float[] effect1 = { -0.6f, -0.6f, -0.6f, -0.6f, -0.6f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            float nextBuffVars_DamageAmount = effect0[level - 1];
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            AddBuff(attacker, other3, new Buffs.Rupture(nextBuffVars_DamageAmount, nextBuffVars_MoveSpeedMod), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Rupture : BuffScript
    {
        float damageAmount;
        float moveSpeedMod;
        public Rupture(float damageAmount = default, float moveSpeedMod = default)
        {
            this.damageAmount = damageAmount;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "rupture_cas_01.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            //RequireVar(this.damageAmount);
            //RequireVar(this.moveSpeedMod);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            int skin = GetSkinID(owner);
            if (skin == 4)
            {
                SpellEffectCreate(out _, out _, "rupture_dino_cas_02.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "rupture_cas_02.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.RuptureLaunch(nextBuffVars_MoveSpeedMod), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, true, false);
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}