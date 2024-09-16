namespace Buffs
{
    public class AlZaharSummonVoidling : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Voidling_Ready.troy", },
            BuffName = "AlZaharSummonVoidlingReady",
            BuffTextureName = "AlZahar_VoidlingReady.dds",
            NonDispellable = true,
        };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                TeamId teamID = GetTeamID_CS(owner);
                Vector3 castPos = GetPointByUnitFacingOffset(owner, 100, 0);
                Minion other3 = SpawnMinion("Voidling", "MalzaharVoidling", "MalzaharVoidling.lua", castPos, teamID, false, false, true, false, false, true, 0, default, false, (Champion)owner);
                int level = GetLevel(owner);
                float bonusDamage = level * 5;
                float bonusHealth = level * 50;
                float nextBuffVars_BonusHealth = bonusHealth;
                float nextBuffVars_BonusDamage = bonusDamage;
                AddBuff((ObjAIBase)owner, other3, new Buffs.AlZaharVoidling(nextBuffVars_BonusHealth, nextBuffVars_BonusDamage), 1, 1, 21, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                SpellBuffRemove(owner, nameof(Buffs.AlZaharSummonVoidling), (ObjAIBase)owner);
            }
        }
    }
}