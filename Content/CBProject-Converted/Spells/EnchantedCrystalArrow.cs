namespace Spells
{
    public class EnchantedCrystalArrow : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 250, 425, 600 };
        float[] effect1 = { -0.5f, -0.5f, -0.5f };
        int[] effect2 = { 0, 0, 0 };
        int[] effect3 = { 3, 3, 3 };
        int[] effect4 = { 125, 212, 300 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float distance = DistanceBetweenObjectAndPoint(target, charVars.CastPoint);
            float stunDuration = distance * 0.00125f;
            stunDuration = Math.Max(stunDuration, 1);
            stunDuration = Math.Min(stunDuration, 3.5f);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float nextBuffVars_MovementSpeedMod;
                float nextBuffVars_AttackSpeedMod;
                if (unit == target)
                {
                    ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.EnchantedCrystalArrow(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    nextBuffVars_MovementSpeedMod = effect1[level - 1];
                    nextBuffVars_AttackSpeedMod = effect2[level - 1];
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, effect3[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
                else
                {
                    BreakSpellShields(target);
                    ApplyDamage(attacker, unit, effect4[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    nextBuffVars_MovementSpeedMod = effect1[level - 1];
                    nextBuffVars_AttackSpeedMod = effect2[level - 1];
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, effect3[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            DestroyMissile(missileNetworkID);
        }
    }
}
namespace Buffs
{
    public class EnchantedCrystalArrow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "head", },
            AutoBuffActivateEffect = new[] { "", "Stun_glb.troy", },
            BuffName = "Enchanted Crystal Arrow",
            BuffTextureName = "Bowmaster_EnchantedArrow.dds",
            PopupMessage = new[] { "game_floatingtext_Stunned", },
        };
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}