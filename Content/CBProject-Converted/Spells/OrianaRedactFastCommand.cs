namespace Spells
{
    public class OrianaRedactFastCommand : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 40, 70, 100, 130, 160 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_DamageBlock;
            Vector3 castPos;
            PlayAnimation("Spell2", 1.25f, owner, false, false, true);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, target, new Buffs.OrianaRedactTarget(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            bool deployed = false;
            float baseDamageBlock = effect0[level - 1];
            float selfAP = GetFlatMagicDamageMod(owner);
            float bonusShield = selfAP * 0.7f;
            float totalShield = bonusShield + baseDamageBlock;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
                deployed = true;
                if (GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaGhost)) > 0)
                {
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff(owner, target, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                    castPos = GetUnitPosition(unit);
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff(owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                    {
                        SpellCast(owner, target, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        SpellCast(owner, target, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff(owner, unit, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            if (!deployed)
            {
                if (charVars.GhostAlive)
                {
                }
                else if (target != owner)
                {
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
                    castPos = GetUnitPosition(owner);
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff(owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                    {
                        SpellCast(owner, target, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        SpellCast(owner, target, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff(owner, owner, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff(owner, owner, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class OrianaRedactFastCommand : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}