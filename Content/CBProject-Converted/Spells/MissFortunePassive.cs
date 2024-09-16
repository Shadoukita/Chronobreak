namespace Buffs
{
    public class MissFortunePassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortunePassive",
            BuffTextureName = "MissFortune_ImpureShots.dds",
            IsDeathRecapSource = true,
            PersistsThroughDeath = true,
        };
        float damageCounter;
        int[] effect0 = { 6, 8, 10, 12, 14 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageCounter = effect0[level - 1];
            //RequireVar(this.missFortunePassive);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is ObjAIBase && target is not BaseTurret)
            {
                float aPMod = GetFlatMagicDamageMod(owner);
                float moddedDmg = aPMod * 0.05f;
                float preCount = moddedDmg + damageCounter;
                TeamId teamID = GetTeamID_CS(owner);
                AddBuff((ObjAIBase)owner, target, new Buffs.MissFortunePassiveStack(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.DAMAGE, 0, false, false, false);
                int count = GetBuffCountFromAll(target, nameof(Buffs.MissFortunePassiveStack));
                float damageDealt = preCount * count;
                ApplyDamage((ObjAIBase)owner, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, (ObjAIBase)owner);
                SpellEffectCreate(out _, out _, "missFortune_passive_tar_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}