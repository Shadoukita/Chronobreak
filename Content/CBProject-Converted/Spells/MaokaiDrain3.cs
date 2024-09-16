namespace Spells
{
    public class MaokaiDrain3 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.8f, 0.8f, 0.8f };
        float[] effect1 = { 0.8f, 0.75f, 0.7f };
        int[] effect2 = { 15, 15, 15 };
        int[] effect3 = { 100, 150, 200 };
        int[] effect4 = { 200, 250, 300 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_DefenseBonus = effect0[level - 1];
            float nextBuffVars_CCReduction = effect1[level - 1];
            float nextBuffVars_ManaCost = effect2[level - 1];
            float nextBuffVars_BaseDamage = effect3[level - 1];
            float nextBuffVars_BonusCap = effect4[level - 1];
            AddBuff(owner, owner, new Buffs.MaokaiDrain3(nextBuffVars_TargetPos, nextBuffVars_DefenseBonus, nextBuffVars_CCReduction, nextBuffVars_ManaCost, nextBuffVars_BaseDamage, nextBuffVars_BonusCap), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MaokaiDrain3 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiDrain",
            BuffTextureName = "Maokai_VengefulMaelstrom.dds",
            SpellToggleSlot = 4,
        };
        Vector3 targetPos;
        float defenseBonus;
        float cCReduction;
        float manaCost;
        float baseDamage;
        float bonusCap;
        EffectEmitter particle;
        EffectEmitter particle2;
        EffectEmitter particle3;
        float damageManaTimer;
        float slowTimer;
        int[] effect0 = { 40, 30, 20 };
        public MaokaiDrain3(Vector3 targetPos = default, float defenseBonus = default, float cCReduction = default, float manaCost = default, float baseDamage = default, float bonusCap = default)
        {
            this.targetPos = targetPos;
            this.defenseBonus = defenseBonus;
            this.cCReduction = cCReduction;
            this.manaCost = manaCost;
            this.baseDamage = baseDamage;
            this.bonusCap = bonusCap;
        }
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MaokaiDrain3Toggle));
            SetSlotSpellCooldownTimeVer2(1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            //RequireVar(this.targetPos);
            //RequireVar(this.defenseBonus);
            //RequireVar(this.cCReduction);
            //RequireVar(this.manaCost);
            //RequireVar(this.baseDamage);
            //RequireVar(this.bonusCap);
            Vector3 targetPos = this.targetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            int ownerSkinID = GetSkinID(owner);
            SpellEffectCreate(out particle, out _, "maoki_torrent_cas_01.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            if (ownerSkinID == 3)
            {
                SpellEffectCreate(out particle2, out particle3, "maoki_torrent_01_teamID_Christmas_green.troy", "maoki_torrent_01_teamID_Christmas_red.troy", teamOfOwner, 300, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out particle2, out particle3, "maoki_torrent_01_teamID_green.troy", "maoki_torrent_01_teamID_red.troy", teamOfOwner, 300, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 550, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                float nextBuffVars_DefenseBonus = defenseBonus;
                float nextBuffVars_CCReduction = cCReduction; // UNUSED
                Vector3 nextBuffVars_TargetPos = this.targetPos;
                AddBuff(attacker, unit, new Buffs.MaokaiDrain3Defense(nextBuffVars_DefenseBonus, nextBuffVars_TargetPos), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                ApplyAssistMarker((ObjAIBase)owner, unit, 10);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MaokaiDrain3));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown = effect0[level - 1];
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = cooldown * multiplier;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
            if (charVars.Tally >= 0)
            {
                float bonus = charVars.Tally * 2;
                bonus = Math.Min(bonusCap, bonus);
                float totalDamage = baseDamage + bonus;
                SpellEffectCreate(out _, out _, "maoki_torrent_deflect_self_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, owner, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "maoki_torrent_deflect_cas_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, owner, default, default, true, false, false, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "maoki_torrent_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                charVars.Tally = 0;
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref damageManaTimer, false))
            {
                float curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if (manaCost > curMana)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float negMana = manaCost * -1;
                    IncPAR(owner, negMana, PrimaryAbilityResourceType.MANA);
                }
            }
            if (ExecutePeriodically(0.25f, ref slowTimer, false))
            {
                Vector3 targetPos = this.targetPos;
                Vector3 ownerPos = GetUnitPosition(owner);
                float distance = DistanceBetweenPoints(ownerPos, targetPos);
                if (distance >= 1400)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 550, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    Vector3 nextBuffVars_TargetPos = this.targetPos;
                    float nextBuffVars_DefenseBonus = defenseBonus;
                    float nextBuffVars_CCReduction = cCReduction; // UNUSED
                    AddBuff(attacker, unit, new Buffs.MaokaiDrain3Defense(nextBuffVars_DefenseBonus, nextBuffVars_TargetPos), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                }
            }
        }
    }
}