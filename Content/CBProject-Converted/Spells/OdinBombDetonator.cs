namespace Spells
{
    public class OdinBombDetonator : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 120f, 100f, 80f, 10f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OdinBombDetonator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "pelvis", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        float timePassed;
        float previousGameTime;
        float lastTimeExecuted;
        float lastTimeExecuted2;
        public override void OnActivate()
        {
            timePassed = 0;
            previousGameTime = GetGameTime();
        }
        public override void OnUpdateActions()
        {
            int count;
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                count = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                float currentGameTime = GetGameTime();
                if (count <= 0)
                {
                    float currentTimePassed = currentGameTime - previousGameTime;
                    timePassed += currentTimePassed;
                }
                previousGameTime = currentGameTime;
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted2, true))
            {
                float timeRemaining = 10 - timePassed;
                float toPrint = MathF.Floor(timeRemaining);
                if (timeRemaining <= 0)
                {
                    Say(owner, "KaBoom");
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, nameof(Buffs.OdinGuardianBuff), true))
                    {
                        AddBuff(attacker, unit, new Buffs.OdinBombDetonation(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    AddBuff(attacker, owner, new Buffs.OdinBombDetonation(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "CrashBoom.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, default, default, false);
                }
                else
                {
                    count = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                    if (count > 0)
                    {
                        Say(owner, "Defusing - ", toPrint);
                    }
                    else
                    {
                        Say(owner, "  ", toPrint);
                    }
                }
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            TeamId myTeamID;
            if (attacker is not Champion)
            {
                float healthToDecreaseBy;
                float myMaxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                myTeamID = GetTeamID_CS(owner);
                if (myTeamID == TeamId.TEAM_NEUTRAL)
                {
                    healthToDecreaseBy = 0.015f * myMaxHealth;
                }
                else
                {
                    healthToDecreaseBy = 0.02f * myMaxHealth;
                }
                ApplyDamage(attacker, owner, healthToDecreaseBy, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
            if (attacker is not Champion)
            {
                myTeamID = GetTeamID_CS(owner);
                if (myTeamID == TeamId.TEAM_NEUTRAL)
                {
                    float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                    if (healthPercent > 0.99f)
                    {
                        ApplyDamage(attacker, owner, 10000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                    }
                    float attackerMaxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
                    float damageReturn = 0.151f * attackerMaxHealth;
                    ApplyDamage((ObjAIBase)owner, attacker, damageReturn, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 0.75f;
        }
    }
}