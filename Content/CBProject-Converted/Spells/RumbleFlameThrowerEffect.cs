namespace Spells
{
    public class RumbleFlameThrowerEffect : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class RumbleFlameThrowerEffect : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GatlingGunSelf",
            BuffTextureName = "Corki_GatlingGun.dds",
            SpellToggleSlot = 3,
        };
        EffectEmitter test;
        EffectEmitter test2;
        public override void OnActivate()
        {
            //RequireVar(this.dangerZone);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleFlameThrowerBuff)) > 0)
            {
                SpellEffectCreate(out test, out _, "rumble_gun_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "barrel", default, target, default, default, false, default, default, false);
                SpellEffectCreate(out test2, out _, "rumble_gun_lite.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out test, out _, "rumble_gun_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "barrel", default, target, default, default, false, default, default, false);
                SpellEffectCreate(out test2, out _, "rumble_gun_lite.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(test);
            SpellEffectRemove(test2);
        }
    }
}