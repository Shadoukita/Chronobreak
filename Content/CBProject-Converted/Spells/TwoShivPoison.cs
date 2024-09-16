namespace Spells
{
    public class TwoShivPoison : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 40, 80, 120, 160, 200 };
        float[] effect1 = { -0.2f, -0.225f, -0.25f, -0.275f, -0.3f };
        float[] effect2 = { 0.2f, 0.225f, 0.25f, 0.275f, 0.3f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float attackDamage = GetTotalAttackDamage(owner);
            float attackDamageMod = attackDamage * 0.5f;
            float backstabBonus = 0;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CastFromBehind)) > 0)
            {
                backstabBonus = 0.2f;
            }
            else
            {
                if (IsInFront(owner, target) && IsBehind(target, owner))
                {
                    backstabBonus = 0.2f;
                }
            }
            ApplyDamage(attacker, target, attackDamageMod + effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1 + backstabBonus, 1, 1, false, false, attacker);
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            float nextBuffVars_MissChance = effect2[level - 1];
            AddBuff(owner, target, new Buffs.TwoShivPoison(nextBuffVars_MoveSpeedMod, nextBuffVars_MissChance), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class TwoShivPoison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "global_slow.troy", },
            BuffName = "Two Shiv Poison",
            BuffTextureName = "Jester_IncrediblyPrecise.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float missChance;
        public TwoShivPoison(float moveSpeedMod = default, float missChance = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.missChance = missChance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.missChance);
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
            if (owner is not Champion)
            {
                IncFlatMissChanceMod(owner, missChance);
            }
        }
    }
}