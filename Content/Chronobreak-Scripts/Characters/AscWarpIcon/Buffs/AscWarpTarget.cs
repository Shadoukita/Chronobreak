namespace Buffs
{
    internal class AscWarpTarget : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        EffectEmitter p1;
        public override void OnActivate()
        {
            target.IconInfo.ChangeBorder("Teleport", "ascwarptarget");
            target.IconInfo.ChangeIcon("NoIcon");
            //p1 = AddParticleTarget(owner, "global_asc_teleport_target", target, lifetime: -1);
        }

        public override void OnDeactivate(bool expired)
        {
            RemoveParticle(p1);
            target.IconInfo.ResetBorder();
            TeleportTo(owner, target.Position);
            if (owner is Champion ch)
            {
                TeleportCamera(ch, target);
            }
            target.Die(CreateDeathData(false, 3, target, target, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_RAW, 0.0f));
            ownerObjAiBase.Spells[6 + (byte)SpellSlotType.InventorySlots].SetCooldown(float.MaxValue);
        }
    }
}