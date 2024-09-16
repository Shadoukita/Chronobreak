namespace Buffs
{
    public class ShenWayOfTheNinjaMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Passive Marker",
            BuffTextureName = "Shen_KiStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float shurikenDamage;
        float lastHit;
        int[] effect0 = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
        public override void OnActivate()
        {
            shurikenDamage = 10;
            SetBuffToolTipVar(1, shurikenDamage);
            lastHit = 0;
        }
        public override void OnUpdateActions()
        {
            int level = GetLevel(owner);
            float shurikenDamage = effect0[level - 1];
            float maxHP = GetFlatHPPoolMod(owner);
            float bonusDmgFromHP = maxHP * 0.08f;
            float finalDamage = bonusDmgFromHP + shurikenDamage;
            SetBuffToolTipVar(1, shurikenDamage);
            SetBuffToolTipVar(2, finalDamage);
            SetBuffToolTipVar(3, bonusDmgFromHP);
            if (!IsDead(owner) && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) == 0)
            {
                float curTime = GetGameTime();
                float timeSinceLastHit = curTime - lastHit;
                if (timeSinceLastHit >= 8)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.ShenWayOfTheNinjaAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Miss && hitResult != HitResult.HIT_Dodge && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) > 0)
            {
                lastHit = GetGameTime();
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (attacker is Champion && hitResult != HitResult.HIT_Miss && hitResult != HitResult.HIT_Dodge)
            {
                lastHit -= 2;
                float curTime = GetGameTime();
                float timeSinceLastHit = curTime - lastHit;
                if (timeSinceLastHit >= 8)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.ShenWayOfTheNinjaAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
    }
}