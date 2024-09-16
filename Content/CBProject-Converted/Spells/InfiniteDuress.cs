namespace Spells
{
    public class InfiniteDuress : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 200, 300, 400 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, target, new Buffs.Suppression(), 100, 1, 1.8f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SUPPRESSION, 0, true, false, false);
            Vector3 pos = GetRandomPointInAreaUnit(target, 150, 150);
            TeleportToPosition(owner, pos);
            FaceDirection(attacker, target.Position3D);
            bool canMove = GetCanMove(target);
            if (!canMove)
            {
                float nextBuffVars_LifestealBonus = 0.3f;
                float nextBuffVars_hitsRemaining = 5;
                AddBuff(attacker, attacker, new Buffs.InfiniteDuress(nextBuffVars_LifestealBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                float baseDamage = effect0[level - 1];
                float totalAD = GetTotalAttackDamage(owner);
                float bonusDamage = 1.667f * totalAD;
                float totalDamage = baseDamage + bonusDamage;
                float damagePerTick = totalDamage / nextBuffVars_hitsRemaining;
                float nextBuffVars_damagePerTick = damagePerTick;
                AddBuff(owner, target, new Buffs.InfiniteDuressChannel(nextBuffVars_hitsRemaining, nextBuffVars_damagePerTick), 1, 1, 1.8f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.InfiniteDuressSound(), 1, 1, 1.8f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellCast(attacker, target, target.Position3D, target.Position3D, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            }
            else
            {
                IssueOrder(owner, OrderType.AttackTo, default, target);
            }
        }
    }
}
namespace Buffs
{
    public class InfiniteDuress : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InfiniteDuress",
            BuffTextureName = "Wolfman_InfiniteDuress.dds",
        };
        float lifestealBonus;
        public InfiniteDuress(float lifestealBonus = default)
        {
            this.lifestealBonus = lifestealBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifestealBonus);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, lifestealBonus);
        }
    }
}