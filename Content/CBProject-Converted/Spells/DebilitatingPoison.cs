namespace Spells
{
    public class TwitchVenomCask : DebilitatingPoison { }
    public class DebilitatingPoison : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 2, 2.6f, 3.2f, 3.8f, 4.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod = -0.3f;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.DebilitatingPoison(nextBuffVars_MoveSpeedMod), 1, 1, effect0[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class TwitchVenomCask : DebilitatingPoison { }
    public class DebilitatingPoison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", "twitch_debilitatingPoison_tar.troy", },
            BuffName = "DebilitatingPoison",
            BuffTextureName = "Twitch_Fade.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        public DebilitatingPoison(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
            float bonusMove = count * -0.06f;
            float totalMoveSpeedMod = bonusMove + moveSpeedMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalMoveSpeedMod);
        }
    }
}