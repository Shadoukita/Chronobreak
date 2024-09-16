namespace CharScripts
{
    public class CharScriptWorm : CharScript
    {
        Region bubble; // UNUSED
        public override void OnUpdateStats()
        {
            AddBuff(owner, owner, new Buffs.ResistantSkin(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60, true, false);
            AddBuff(owner, owner, new Buffs.WormRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.SweepTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.PropelTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer2)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer)) == 0)
            {
                float distance = DistanceBetweenObjects(attacker, owner);
                if (distance <= 950)
                {
                    FaceDirection(owner, attacker.Position3D);
                    SpellCast(owner, attacker, owner.Position3D, owner.Position3D, 3, SpellSlotType.SpellSlots, 1, false, false, false, false, false, false);
                }
                else
                {
                    damageAmount = 0;
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            SpellBuffRemove(owner, nameof(Buffs.ActionTimer), owner);
            SpellBuffRemove(owner, nameof(Buffs.PropelTimer), owner);
            SpellBuffRemove(owner, nameof(Buffs.WrathTimer), owner);
            SpellBuffRemove(owner, nameof(Buffs.SweepTimer), owner);
            if (RandomChance() < 0.04f)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathCooldown)) > 0)
                {
                    AddBuff(owner, owner, new Buffs.ActionTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathCooldown)) == 0)
                {
                    AddBuff(owner, owner, new Buffs.WrathTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    AddBuff(owner, owner, new Buffs.WrathCooldown(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            else if (RandomChance() < 0.12f)
            {
                AddBuff(owner, owner, new Buffs.SweepTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else if (RandomChance() < 0.18f)
            {
                AddBuff(owner, owner, new Buffs.PropelTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.ActionTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.SweepTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.PropelTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer)) == 0)
            {
                AddBuff(owner, owner, new Buffs.ActionTimer2(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            TeamId teamID = TeamId.TEAM_NEUTRAL;
            bubble = AddPosPerceptionBubble(teamID, 1600, owner.Position3D, 25000, default, false);
            SetImmovable(owner, true);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}