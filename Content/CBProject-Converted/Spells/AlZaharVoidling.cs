namespace Spells
{
    public class AlZaharVoidling : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class AlZaharVoidling : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AlZaharVoidling",
            BuffTextureName = "AlZahar_SummonVoidling.dds",
        };
        float bonusHealth;
        float bonusDamage;
        float timer;
        float lastTimeExecuted;
        public AlZaharVoidling(float bonusHealth = default, float bonusDamage = default)
        {
            this.bonusHealth = bonusHealth;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            Vector3 targetPos; // UNITIALIZED
            targetPos = default; //TODO: Verify
            //RequireVar(this.bonusDamage);
            //RequireVar(this.bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, bonusDamage);
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            timer = 0;
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "VoidlingFlash.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, target, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 8000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.25f);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                timer += 0.5f;
                if (timer >= 7)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharVoidlingPhase2)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharVoidlingPhase2(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    }
                    if (timer >= 14)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharVoidlingPhase3)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharVoidlingPhase3(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                        }
                    }
                }
            }
        }
    }
}