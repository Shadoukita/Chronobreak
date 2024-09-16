namespace Spells
{
    public class TrundleTrollSmash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 4, 4, 4, 4, 4 };
        public override void SelfExecute()
        {
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.TrundleTrollSmash(nextBuffVars_SpellCooldown), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class TrundleTrollSmash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "TrundleTrollSmash",
            BuffTextureName = "Trundle_Bite.dds",
        };
        EffectEmitter geeves1;
        float spellCooldown;
        public TrundleTrollSmash(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out geeves1, out _, "Trundle_TrollSmash_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_Mouth", default, owner, default, default, true, default, default, false);
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
            UnlockAnimation(owner, true);
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(geeves1);
            SetDodgePiercing(owner, false);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 targetPos = GetUnitPosition(target);
                FaceDirection(owner, targetPos);
                SkipNextAutoAttack((ObjAIBase)owner);
                SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.TrundleTrollSmash), (ObjAIBase)owner);
            }
        }
    }
}