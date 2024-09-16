namespace Buffs
{
    public class RumbleHeatDelay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Aegis_buf.troy", "", "", },
            BuffName = "Heating Up",
            BuffTextureName = "034_Steel_Shield.dds",
        };
        public override void OnActivate()
        {
            AddBuff(attacker, target, new Buffs.RumbleHeatingUp(), 1, 1, 500, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.RumbleHeatingUp2(), 1, 1, 500, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.RumbleHeatingUp), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.RumbleHeatingUp2), (ObjAIBase)owner);
            AddBuff(attacker, target, new Buffs.RumbleHeatingUp2(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.RumbleHeatingUp(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}