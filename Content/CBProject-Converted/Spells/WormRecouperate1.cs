namespace Spells
{
    public class WormRecouperate1 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class WormRecouperate1 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "WormRecouperate1",
            BuffTextureName = "1035_Short_Sword.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            SpellBuffRemove(owner, nameof(Buffs.WormRecouperateOn), (ObjAIBase)owner);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnUpdateActions()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent < 1)
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float healthToInc = maxHealth * 0.03f;
                IncHealth(owner, healthToInc, owner);
            }
            if (healthPercent >= 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecoupDebuff(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecoupDebuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}