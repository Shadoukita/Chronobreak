namespace Spells
{
    public class TalonBleedDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class TalonBleedDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "Bleed",
            BuffTextureName = "TalonNoxianDiplomacy.dds",
            IsDeathRecapSource = true,
        };
        TeamId attackerTeamID;
        float bonusDamage;
        EffectEmitter blood1;
        EffectEmitter blood2;
        Region unitBubble;
        float lastTimeExecuted2;
        float lastTimeExecuted;
        int[] effect0 = { 3, 6, 9, 12, 15 };
        public override void OnActivate()
        {
            TeamId attackerTeamID; // UNITIALIZED
            this.attackerTeamID = GetTeamID_CS(attacker);
            attackerTeamID = this.attackerTeamID; //TODO: Verify
            float totalAD = GetTotalAttackDamage(attacker);
            float baseAD = GetBaseAttackDamage(attacker);
            float bonusAD = totalAD - baseAD;
            bonusDamage = bonusAD * 0.2f;
            SpellEffectCreate(out blood1, out _, "talon_Q_bleed_indicator.troy", default, attackerTeamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out blood2, out _, "talon_Q_bleed.troy", default, attackerTeamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            unitBubble = AddUnitPerceptionBubble(this.attackerTeamID, 400, owner, 6, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            //float flatAPBonus;
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float poisonBaseDamage = effect0[level - 1];
            float poisonTotalDamage = 0;
            float baseAttackDamage = GetBaseAttackDamage(attacker); // UNUSED
            //flatAPBonus *= 0.1f;
            poisonTotalDamage = poisonBaseDamage + bonusDamage;
            ApplyDamage(attacker, owner, poisonTotalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            SpellEffectRemove(blood1);
            SpellEffectRemove(blood2);
            RemovePerceptionBubble(unitBubble);
        }
        public override void OnUpdateActions()
        {
            if (owner is Champion)
            {
                if (ExecutePeriodically(1, ref lastTimeExecuted2, true))
                {
                    Vector3 ownerPos = GetUnitPosition(owner);
                    Minion other1 = SpawnMinion("BloodDrop", "TestCube", "Idle.lua", ownerPos, attackerTeamID, false, true, false, true, true, true, 450, false, false, (Champion)attacker);
                    SetTargetable(other1, false);
                    AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (ExecutePeriodically(1, ref lastTimeExecuted, true))
                {
                    //float flatAPBonus;
                    int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float poisonBaseDamage = effect0[level - 1];
                    float poisonTotalDamage = 0;
                    float baseAttackDamage = GetBaseAttackDamage(attacker); // UNUSED
                    //flatAPBonus *= 0.1f;
                    poisonTotalDamage = poisonBaseDamage + bonusDamage;
                    ApplyDamage(attacker, owner, poisonTotalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}