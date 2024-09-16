namespace Spells
{
    public class TwitchFullAutomaticTwitchFullAutomatic : FullAutomatic { }
    public class FullAutomatic : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 75f, 60f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "GangsterTwitch", "PunkTwitch", },
        };
        int[] effect0 = { 5, 6, 7 };
        int[] effect1 = { 15, 25, 35 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int nextBuffVars_numAttacks = effect0[level - 1];
            float nextBuffVars_bonusDamage = effect1[level - 1];
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, level, false);
            AddBuff(attacker, attacker, new Buffs.FullAutomatic(nextBuffVars_numAttacks, nextBuffVars_bonusDamage), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.TwitchSprayAndPray(), 10, nextBuffVars_numAttacks, 12, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class TwitchSprayandPrayAttack : FullAutomatic { }
    public class FullAutomatic : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[] { "twitch_ambush_buf.troy", "twitch_ambush_buf.troy", "twitch_ambush_buf_02.troy", },
            BuffName = "Full Automatic",
            BuffTextureName = "Twitch_Clone.dds",
        };
        float numAttacks;
        float bonusDamage;
        public FullAutomatic(float numAttacks = default, float bonusDamage = default)
        {
            this.numAttacks = numAttacks;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.numAttacks);
            //RequireVar(this.bonusDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.TwitchSprayAndPray), 0);
        }
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, 375);
            IncFlatPhysicalDamageMod(owner, bonusDamage);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            numAttacks--;
            SpellBuffRemove(owner, nameof(Buffs.TwitchSprayAndPray), (ObjAIBase)owner);
            if (numAttacks <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}