namespace Spells
{
    public class IreliaHitenStyle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.IreliaHitenStyle), owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, owner, new Buffs.IreliaHitenStyleCharged(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class IreliaHitenStyle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", "", },
            BuffName = "IreliaHitenStyle",
            BuffTextureName = "Irelia_HitenStyleReady.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter ireliaHitenStyle1;
        EffectEmitter ireliaHitenStyle2;
        int[] effect0 = { 10, 14, 18, 22, 26 };
        public override void OnActivate()
        {
            OverrideAnimation("Attack1", "Attack1b", owner);
            OverrideAnimation("Attack2", "Attack2b", owner);
            OverrideAnimation("Crit", "Critb", owner);
            OverrideAnimation("Idle1", "Idle1b", owner);
            OverrideAnimation("Run", "Runb", owner);
            TeamId ireliaTeamID = GetTeamID_CS(owner);
            SpellEffectCreate(out ireliaHitenStyle1, out _, "irelia_hitenStyle_passive.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false);
            SpellEffectCreate(out ireliaHitenStyle2, out _, "irelia_hitenStlye_passive_glow.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(ireliaHitenStyle1);
            SpellEffectRemove(ireliaHitenStyle2);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healthRestoration = effect0[level - 1];
            IncHealth(owner, healthRestoration, owner);
        }
    }
}