namespace Spells
{
    public class KarthusLayWasteA1: LayWaste {}
    public class LayWaste : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 120, 160, 200, 240 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Region bubbleID = AddPosPerceptionBubble(teamOfOwner, 200, targetPos, 1, default, false); // UNUSED
            Minion other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            float nextBuffVars_DamageAmount = effect0[level - 1];
            AddBuff(attacker, other3, new Buffs.LayWaste(nextBuffVars_DamageAmount), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LayWaste : BuffScript
    {
        float damageAmount;
        TeamId teamOfOwner;
        EffectEmitter particle;
        public LayWaste(float damageAmount = default)
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
            teamOfOwner = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "LayWaste_point.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectCreate(out particle, out _, "LayWaste_tar.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            float numUnits = 0;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                numUnits++;
            }
            if (numUnits == 1)
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                }
            }
            else if (numUnits >= 2)
            {
                float damageAmount = this.damageAmount / 2;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                }
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}