namespace Spells
{
    public class AkaliSmokeBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 8, 8, 8, 8, 8 };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.AkaliSmokeBomb(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AkaliSmokeBomb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter particle2;
        EffectEmitter particle;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle2, out particle, "akali_smoke_bomb_tar_team_green.troy", "akali_smoke_bomb_tar_team_red.troy", casterID, 250, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
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
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_InitialTime; // UNUSED
                    float nextBuffVars_TimeLastHit; // UNUSED
                    if (unit == attacker)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
                        {
                            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AkaliHoldStealth)) == 0)
                            {
                                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AkaliSBStealth)) == 0)
                                {
                                    nextBuffVars_InitialTime = GetTime();
                                    nextBuffVars_TimeLastHit = GetTime();
                                    AddBuff(attacker, attacker, new Buffs.AkaliSmokeBombInternal(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                }
                                else
                                {
                                    AddBuff(attacker, attacker, new Buffs.AkaliSBStealth(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
                                }
                            }
                        }
                        AddBuff(attacker, attacker, new Buffs.AkaliSBBuff(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                    if (unit.Team != attacker.Team)
                    {
                        AddBuff(attacker, unit, new Buffs.AkaliSBDebuff(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
        }
    }
}