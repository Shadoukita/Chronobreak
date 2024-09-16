namespace Spells
{
    public class FrostArcherIceArrowAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            DebugSay(owner, "execute");
            if (target is ObjAIBase && target is not BaseTurret)
            {
                DebugSay(owner, "add buff");
                AddBuff(owner, owner, new Buffs.FrostArcherIceArrowAttack(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true);
            }
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
        }
    }
}
namespace Buffs
{
    public class FrostArcherIceArrowAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Freeze.troy", },
            BuffName = "FrostArrow",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        float[] effect0 = { -0.1f, -0.2f, -0.3f, -0.4f, -0.5f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void OnActivate()
        {
            DebugSay(owner, "applicator activatre");
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_MovementSpeedMod = effect0[level - 1];
                int nextBuffVars_AttackSpeedMod = effect1[level - 1]; // UNUSED
                AddBuff((ObjAIBase)owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
            }
        }
    }
}