namespace Spells
{
    public class OdinPlantedObeliskBuff : SpellScript
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
    public class OdinPlantedObeliskBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "pelvis", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        float nextAttackTime;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            nextAttackTime = GetGameTime();
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                float currentTime = GetGameTime();
                bool foundTarget = false;
                int count = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                if (count >= 1)
                {
                    nextAttackTime = currentTime + 1;
                }
                if (nextAttackTime <= currentTime)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, nameof(Buffs.OdinGuardianBuff), true))
                    {
                        if (!foundTarget)
                        {
                            if (count <= 0)
                            {
                                int _1; // UNITIALIZED
                                _1 = 1; //TODO: Verify
                                SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.SpellSlots, 1 + _1, true, false, false, false, false, false);
                                nextAttackTime = currentTime + 1.25f;
                                foundTarget = true;
                            }
                        }
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