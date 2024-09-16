namespace Buffs
{
    public class ViktorGravitonFieldDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", "Viktor_Catalyst_buf.troy", "", },
            BuffName = "ViktorGravitonSlow",
            BuffTextureName = "ViktorGravitonField.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        public ViktorGravitonFieldDebuff(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.ViktorGravitonFieldDebuff));
            if (count >= 3)
            {
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ViktorGravitonFieldNoStun)) == 0)
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ViktorAugmentW)) > 0)
                    {
                        SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.ViktorGravitonFieldDebuff), count);
                        AddBuff(attacker, owner, new Buffs.ViktorGravitonFieldNoStun(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(owner);
                        AddBuff(attacker, owner, new Buffs.ViktorGravitonFieldStun(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.ViktorGravitonFieldDebuff), count);
                        AddBuff(attacker, owner, new Buffs.ViktorGravitonFieldNoStun(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(owner);
                        AddBuff(attacker, owner, new Buffs.ViktorGravitonFieldStun(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
                SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.ViktorGravitonFieldDebuff), count);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
        }
    }
}