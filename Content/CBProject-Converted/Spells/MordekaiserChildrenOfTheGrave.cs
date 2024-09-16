namespace Spells
{
    public class MordekaiserChildrenOfTheGrave : SpellScript
    {
        float[] effect0 = { 0.24f, 0.29f, 0.34f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float mordAP = GetFlatMagicDamageMod(owner);
            float damageToDeal = effect0[level - 1];
            float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float mordAP1 = mordAP * 0.0004f;
            damageToDeal += mordAP1;
            damageToDeal *= 0.5f;
            float initialDamageToDeal = maxHealth * damageToDeal;
            float tickDamage = damageToDeal * 0.1f;
            float nextBuffVars_LifestealPercent = tickDamage;
            AddBuff(owner, target, new Buffs.MordekaiserChildrenOfTheGrave(nextBuffVars_LifestealPercent), 1, 1, 10.4f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            float nextBuffVars_DamageToDeal = initialDamageToDeal;
            AddBuff((ObjAIBase)target, attacker, new Buffs.MordekaiserCOTGDot(nextBuffVars_DamageToDeal), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MordekaiserChildrenOfTheGrave : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "MordekaiserCOTGDot",
            BuffTextureName = "Mordekaiser_COTG.dds",
            NonDispellable = false,
            OnPreDamagePriority = 10,
        };
        float lifestealPercent;
        float damageToDeal;
        EffectEmitter mordekaiserParticle;
        bool removeParticle;
        float lastTimeExecuted;
        public MordekaiserChildrenOfTheGrave(float lifestealPercent = default)
        {
            this.lifestealPercent = lifestealPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifestealPercent);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            damageToDeal = maxHealth * lifestealPercent;
            SpellEffectCreate(out mordekaiserParticle, out _, "mordekeiser_cotg_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            removeParticle = true;
        }
        public override void OnDeactivate(bool expired)
        {
            if (removeParticle)
            {
                SpellEffectRemove(mordekaiserParticle);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float nextBuffVars_DamageToDeal = damageToDeal;
                AddBuff((ObjAIBase)owner, attacker, new Buffs.MordekaiserCOTGDot(nextBuffVars_DamageToDeal), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (owner is Champion)
            {
                float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if (curHealth <= 0)
                {
                    ObjAIBase caster = GetBuffCasterUnit();
                    removeParticle = false;
                    EffectEmitter nextBuffVars_MordekaiserParticle = mordekaiserParticle;
                    AddBuff((ObjAIBase)owner, caster, new Buffs.MordekaiserCOTGRevive(nextBuffVars_MordekaiserParticle), 1, 1, 30, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}