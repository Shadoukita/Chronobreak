namespace Buffs
{
    public class SejuaniWintersClawDmg : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "NetherBladeArmorPen",
            BuffTextureName = "Voidwalker_NullBlade.dds",
            SpellToggleSlot = 2,
        };
        float baseDamage;
        EffectEmitter chargedBladeEffect;
        int[] effect0 = { 30, 45, 60, 75, 90 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = effect0[level - 1];
            SpellEffectCreate(out chargedBladeEffect, out _, "enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, false, false, false, false);
            OverrideAnimation("Attack1", "Crit", owner);
            OverrideAnimation("Attack2", "Crit", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(chargedBladeEffect);
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                TeamId teamID = GetTeamID_CS(attacker);
                SpellEffectCreate(out _, out _, "Leona_ShieldOfDaybreak_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.25f, 0, false, false, attacker);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.SejuaniWintersClawBuff), 1);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.SejuaniWintersClawBuff));
                if (count <= 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.SejuaniWintersClawBuff), (ObjAIBase)owner, 0);
                    SpellBuffRemove(owner, nameof(Buffs.SejuaniWintersClawDmg), (ObjAIBase)owner, 0);
                }
            }
        }
    }
}