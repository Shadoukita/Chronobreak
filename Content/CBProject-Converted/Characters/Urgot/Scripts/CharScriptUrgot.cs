namespace CharScripts
{
    public class CharScriptUrgot : CharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTime2Executed, true))
            {
                if (IsDead(owner))
                {
                    AddBuff(owner, owner, new Buffs.UrgotDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.UrgotDeathParticle), owner, 0);
                    }
                }
                float aD = GetFlatPhysicalDamageMod(owner);
                float bonusDamage = aD * 0.6f;
                SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptUrgot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Teemo_EagleEye.dds",
        };
    }
}