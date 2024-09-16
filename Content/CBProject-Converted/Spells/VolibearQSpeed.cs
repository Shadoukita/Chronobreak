namespace Buffs
{
    public class VolibearQSpeed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "VolibearQSpeed",
            BuffTextureName = "VolibearQ.dds",
        };
        float speedMod;
        public VolibearQSpeed(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.VolibearQSpeedPart), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateStats()
        {
            bool hunt = false;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                if (IsInFront(owner, unit))
                {
                    bool visible = CanSeeTarget(owner, unit);
                    if (visible)
                    {
                        hunt = true;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.VolibearQHunted(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.VolibearQHunted)) > 0)
                    {
                        hunt = true;
                    }
                }
            }
            if (hunt)
            {
                IncPercentMovementSpeedMod(owner, speedMod);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VolibearQSpeedPart)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearQSpeedPart(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.VolibearQSpeedPart), (ObjAIBase)owner, 0);
                IncPercentMovementSpeedMod(owner, 0.15f);
            }
        }
    }
}