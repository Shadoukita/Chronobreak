namespace Spells
{
    public class GravesSmokeGrenade : SpellScript
    {
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            FaceDirection(owner, targetPos);
            if (distance > 950)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            Minion other2 = SpawnMinion("k", "TestCubeRender10Vision", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 50, false, true, (Champion)attacker);
            SpellCast(owner, other2, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GravesSmokeGrenade : BuffScript
    {
        EffectEmitter particle2;
        EffectEmitter particle;
        float lastTimeExecuted;
        float[] effect0 = { -0.15f, -0.2f, -0.25f, -0.3f, -0.35f };
        public override void OnActivate()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle2, out particle, "Graves_SmokeGrenade_Cloud_Team_Green.troy", "Graves_SmokeGrenade_Cloud_Team_Red.troy", casterID, 250, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
                {
                    int nextBuffVars_SightReduction;
                    float nextBuffVars_MovementSpeedMod = effect0[level - 1];
                    AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeBoomSlow(nextBuffVars_MovementSpeedMod), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, true, false);
                    if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.GravesSmokeGrenadeDelay)) == 0)
                    {
                        nextBuffVars_SightReduction = -800;
                        AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeBoom(nextBuffVars_SightReduction), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.NocturneParanoiaTarget)) > 0)
                    {
                        AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeNocturneUlt(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}