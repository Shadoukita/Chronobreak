namespace CharScripts
{
    public class CharScriptKassadin : CharScript
    {
        float lastTimeExecuted;
        float[] effect0 = { 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f };
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.NetherBlade)) == 0)
            {
                int level2 = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level2 > 0)
                {
                    AddBuff(owner, owner, new Buffs.NetherBlade(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    if (!IsDead(owner))
                    {
                        TeamId teamID = GetTeamID_CS(owner);
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, default, true))
                        {
                            if (teamID == TeamId.TEAM_ORDER)
                            {
                                AddBuff(attacker, unit, new Buffs.ForcePulse(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            }
                            else
                            {
                                AddBuff(attacker, unit, new Buffs.Forcepulsechaos(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.MagicAbsorb = effect0[level - 1];
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.VoidStone(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptKassadin : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Teemo_EagleEye.dds",
        };
    }
}