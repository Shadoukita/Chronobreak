namespace Spells
{
    public class Wither : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { -0.35f, -0.35f, -0.35f, -0.35f, -0.35f };
        float[] effect1 = { -0.03f, -0.06f, -0.09f, -0.12f, -0.15f };
        int[] effect2 = { 5, 5, 5, 5, 5 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_SpeedMod = effect0[level - 1];
            float nextBuffVars_BonusSpeedMod = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.Wither(nextBuffVars_BonusSpeedMod, nextBuffVars_SpeedMod), 1, 1, effect2[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Wither : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", "nassus_wither_tar.troy", "", },
            BuffName = "Wither",
            BuffTextureName = "Nasus_Wither.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float bonusSpeedMod;
        float speedMod;
        float duration;
        float timeBetweenTicks;
        float lastTimeExecuted;
        public Wither(float bonusSpeedMod = default, float speedMod = default)
        {
            this.bonusSpeedMod = bonusSpeedMod;
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusSpeedMod);
            //RequireVar(this.speedMod);
            ApplyAssistMarker(attacker, owner, 10);
            duration = GetBuffRemainingDuration(target, nameof(Buffs.Wither));
            timeBetweenTicks = duration / 5;
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0, ref lastTimeExecuted, false, timeBetweenTicks))
            {
                speedMod += bonusSpeedMod;
            }
            IncPercentMultiplicativeAttackSpeedMod(owner, speedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, speedMod);
        }
    }
}