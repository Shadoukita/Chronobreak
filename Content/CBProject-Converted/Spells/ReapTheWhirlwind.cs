namespace Spells
{
    public class ReapTheWhirlwind : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 4f,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 35, 55, 75 };
        public override void ChannelingStart()
        {
            float baseTickAmount = effect0[level - 1];
            float aPAmount = GetFlatMagicDamageMod(owner);
            float aPTickBonus = aPAmount * 0.175f;
            float tickAmount = baseTickAmount + aPTickBonus;
            float nextBuffVars_TickAmount = tickAmount;
            AddBuff(owner, owner, new Buffs.ReapTheWhirlwind(nextBuffVars_TickAmount), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.ReapTheWhirlwind), owner, 0);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.ReapTheWhirlwind), owner, 0);
        }
    }
}
namespace Buffs
{
    public class ReapTheWhirlwind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Reap The Whirlwind",
            BuffTextureName = "Janna_ReapTheWhirlwind.dds",
        };
        float tickAmount;
        EffectEmitter particle2;
        EffectEmitter particle;
        float friendlyTimeExecuted;
        public ReapTheWhirlwind(float tickAmount = default)
        {
            this.tickAmount = tickAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tickAmount);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            charVars.Ticks = 0;
            SpellEffectCreate(out particle2, out particle, "ReapTheWhirlwind_green_cas.troy", "ReapTheWhirlwind_red_cas.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (owner.Team == unit.Team)
                {
                    float temp1 = GetHealthPercent(unit, PrimaryAbilityResourceType.MANA);
                    if (temp1 < 1)
                    {
                        ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                        IncHealth(unit, tickAmount, owner);
                    }
                }
                else
                {
                    BreakSpellShields(unit);
                    Vector3 center = GetUnitPosition(owner);
                    Vector3 nextBuffVars_Center = center;
                    float nextBuffVars_Distance = 1000;
                    int nextBuffVars_IdealDistance = 1000; // UNUSED
                    float nextBuffVars_Gravity = 10;
                    float nextBuffVars_Speed = 1200;
                    AddBuff(attacker, unit, new Buffs.MoveAway(nextBuffVars_Distance, nextBuffVars_Gravity, nextBuffVars_Speed, nextBuffVars_Center), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.5f, ref friendlyTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 700, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float temp1 = GetHealthPercent(unit, PrimaryAbilityResourceType.MANA);
                    if (temp1 < 1)
                    {
                        ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                        IncHealth(unit, tickAmount, owner);
                    }
                }
            }
        }
    }
}