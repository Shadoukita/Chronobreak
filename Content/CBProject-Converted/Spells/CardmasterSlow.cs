﻿namespace Buffs
{
    public class CardmasterSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "CardmasterSlow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float attackSpeedMod;
        public CardmasterSlow(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void UpdateBuffs()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, target, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}