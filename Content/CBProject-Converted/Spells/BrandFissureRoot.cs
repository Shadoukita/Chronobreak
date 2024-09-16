namespace Spells
{
    public class BrandFissureRoot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class BrandFissureRoot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainShadowGraspRoot",
            BuffTextureName = "SwainNevermove.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        float mRDebuff;
        EffectEmitter rootParticleEffect2;
        EffectEmitter rootParticleEffect;
        int[] effect0 = { 0, 0, 0 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SwainMetamorphism)) > 0)
            {
                mRDebuff = effect0[level - 1];
            }
            else
            {
                mRDebuff = 0;
            }
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            SetCanMove(owner, false);
            SpellEffectCreate(out rootParticleEffect2, out _, "SwainShadowGraspRootTemp.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out rootParticleEffect, out _, "swain_shadowGrasp_magic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(rootParticleEffect2);
            SpellEffectRemove(rootParticleEffect);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, mRDebuff);
            SetCanMove(owner, false);
        }
    }
}