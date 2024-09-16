namespace Spells
{
    public class AhriSeduce : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class AhriSeduce : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "AhriSeduce",
            BuffTextureName = "Ahri_Charm.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float slowPercent;
        EffectEmitter particle1;
        EffectEmitter particle2;
        EffectEmitter particle3;
        public AhriSeduce(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
            SetCanAttack(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out particle1, out _, "Ahri_Charm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "head", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle2, out _, "Ahri_Charm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "l_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle3, out _, "Ahri_Charm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "r_hand", default, owner, default, default, false, false, false, false, false);
            if (owner is Champion)
            {
                IssueOrder(owner, OrderType.MoveTo, default, attacker);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            StopMove(owner);
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            IncPercentMultiplicativeMovementSpeedMod(owner, slowPercent);
            IncFlatAttackRangeMod(owner, -600);
        }
    }
}