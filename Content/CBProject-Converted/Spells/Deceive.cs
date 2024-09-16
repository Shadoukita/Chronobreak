namespace Spells
{
    public class Deceive : SpellScript
    {
        int[] effect0 = { 11, 11, 11, 11, 11 };
        float[] effect1 = { -0.6f, -0.4f, -0.2f, 0, 0.2f };
        public override void SelfExecute()
        {
            int nextBuffVars_DCooldown = effect0[level - 1];
            SpellEffectCreate(out _, out _, "jackintheboxpoof2.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);
            Vector3 castPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, castPos);
            if (distance > 500)
            {
                FaceDirection(owner, castPos);
                castPos = GetPointByUnitFacingOffset(owner, 500, 0);
            }
            Vector3 nextBuffVars_CastPos = castPos;
            float nextBuffVars_CritDmgBonus = effect1[level - 1];
            SealSpellSlot(0, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff(owner, owner, new Buffs.DeceiveFade(nextBuffVars_DCooldown, nextBuffVars_CastPos, nextBuffVars_CritDmgBonus), 1, 1, 0.05f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
        }
    }
}
namespace Buffs
{
    public class Deceive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deceive",
            BuffTextureName = "Jester_ManiacalCloak2.dds",
            IsDeathRecapSource = true,
        };
        float dCooldown;
        public Deceive(float dCooldown = default)
        {
            this.dCooldown = dCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dCooldown);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            PushCharacterFade(owner, 1, 0);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * dCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if (!spellVars.CastingBreaksStealth)
            {
            }
            else if (!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}