namespace Spells
{
    public class ShenDestabilize : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { -15, -22, -29, -36, -43 };
        float[] effect1 = { 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
        float[] effect2 = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_ArmorMod = effect0[level - 1];
            float nextBuffVars_LifeReturn = effect1[level - 1];
            float nextBuffVars_NinjaBonus = effect2[level - 1];
            AddBuff(attacker, target, new Buffs.ShenDestabilize(nextBuffVars_NinjaBonus, nextBuffVars_LifeReturn, nextBuffVars_ArmorMod), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class ShenDestabilize : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "archersmark_tar.troy", },
            BuffName = "Shen Destabilize",
            BuffTextureName = "GSB_Stun.dds",
        };
        float ninjaBonus;
        float lifeReturn;
        float armorMod;
        public ShenDestabilize(float ninjaBonus = default, float lifeReturn = default, float armorMod = default)
        {
            this.ninjaBonus = ninjaBonus;
            this.lifeReturn = lifeReturn;
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.ninjaBonus);
            //RequireVar(this.lifeReturn);
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            SetTriggerUnit(target);
            if (target is Champion)
            {
                float healAmount;
                if (GetBuffCountFromCaster(target, target, nameof(Buffs.IsNinja)) > 0)
                {
                    healAmount = lifeReturn + ninjaBonus;
                }
                else
                {
                    healAmount = lifeReturn;
                }
                float healTotal = healAmount * damageAmount;
                ObjAIBase caster = GetBuffCasterUnit();
                IncHealth(target, healTotal, caster);
                SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
        }
    }
}