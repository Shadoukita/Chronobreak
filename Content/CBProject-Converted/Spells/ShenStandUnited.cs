namespace Spells
{
    public class ShenStandUnited : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 200, 475, 750 };
        public override void ChannelingStart()
        {
            AddBuff(attacker, owner, new Buffs.ShenStandUnited(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            float baseShieldHealth = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusShieldHealth = 1.5f * abilityPower;
            float shieldHealth = baseShieldHealth + bonusShieldHealth;
            float nextBuffVars_shieldHealth = shieldHealth;
            AddBuff(owner, target, new Buffs.ShenStandUnitedShield(nextBuffVars_shieldHealth), 1, 1, 7.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.ShenStandUnitedTarget(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            DestroyMissileForTarget(owner);
            Vector3 castPos = GetPointByUnitFacingOffset(target, 150, 180);
            TeleportToPosition(owner, castPos);
        }
    }
}
namespace Buffs
{
    public class ShenStandUnited : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Shen Stand United Channel",
            BuffTextureName = "Shen_StandUnited.dds",
        };
        EffectEmitter particleID;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out particleID, out _, "ShenTeleport_v2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
        }
    }
}