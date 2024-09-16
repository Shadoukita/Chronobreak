namespace Spells
{
    public class VayneSilveredBolts : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "", },
        };
    }
}
namespace Buffs
{
    public class VayneSilveredBolts : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "VayneSilverBolts",
            BuffTextureName = "Vayne_SilveredBolts.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        float[] effect0 = { 0.04f, 0.05f, 0.06f, 0.07f, 0.08f };
        int[] effect1 = { 20, 30, 40, 50, 60 };
        /*
        //TODO: Uncomment and fix
        public override void OnHitUnit(AttackableUnit target, float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            bool isBlinded; // UNITIALIZED
            bool canMove = GetCanMove(owner); // UNUSED
            if(!isBlinded)
            {
                returnValue = true;
            }
            else
            {
                if(target is ObjAIBase && target is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
                {
                    int count = GetBuffCountFromCaster(target, attacker, nameof(Buffs.VayneSilveredDebuff));
                    if(count == 2)
                    {
                        TeamId teamID = GetTeamID(attacker);
                        TeamId teamIDTarget = GetTeamID(target);
                        SpellEffectCreate(out gragas, out _, "vayne_W_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, target.Position3D, target, default, default, true, false, false, false, false);
                        int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        float abilityPower = GetFlatMagicDamageMod(attacker);
                        float bonusMaxHealthDamage = 0 * abilityPower;
                        SpellBuffClear(target, nameof(Buffs.VayneSilveredDebuff));
                        float tarMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                        float rankScaling = this.effect0[level - 1];
                        float flatScaling = this.effect1[level - 1];
                        rankScaling += bonusMaxHealthDamage;
                        float damageToDeal = tarMaxHealth * rankScaling;
                        damageToDeal += flatScaling;
                        if(teamIDTarget == TeamId.TEAM_NEUTRAL)
                        {
                            damageToDeal = Math.Min(damageToDeal, 200);
                        }
                        ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.VayneSilveredDebuff(), 3, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
        */
    }
}