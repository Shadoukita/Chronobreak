namespace Spells
{
    public class DeathLotus : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.65f,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.DeathLotusSound(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            int nextBuffVars_Level = level;
            AddBuff(owner, owner, new Buffs.DeathLotus(nextBuffVars_Level), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.25f, true, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.DeathLotusSound), owner);
            SpellBuffRemove(owner, nameof(Buffs.DeathLotus), owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.DeathLotusSound), owner);
            SpellBuffRemove(owner, nameof(Buffs.DeathLotus), owner);
        }
    }
}
namespace Buffs
{
    public class DeathLotus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        int level;
        public DeathLotus(int level = default)
        {
            this.level = level;
        }
        public override void OnUpdateActions()
        {
            int level = this.level;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                SpellCast((ObjAIBase)owner, unit, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
    }
}