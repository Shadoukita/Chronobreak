namespace Spells
{
    public class OdinShamanAura : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OdinShamanAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            BuffName = "OdinShamanAura",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
            PersistsThroughDeath = true,
        };
        EffectEmitter shamanAuraParticle;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out shamanAuraParticle, out _, "SonaSongofDiscord_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(shamanAuraParticle);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                if (!IsDead(owner))
                {
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, default, true))
                    {
                        string unitName = GetUnitSkinName(unit);
                        if (unitName == "Red_Minion_Melee")
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.OdinShamanBuff(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        if (unitName == "Blue_Minion_Melee")
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.OdinShamanBuff(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}