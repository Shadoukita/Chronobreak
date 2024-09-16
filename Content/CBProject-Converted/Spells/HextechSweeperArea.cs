namespace Spells
{
    public class HextechSweeperArea : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}
namespace Buffs
{
    public class HextechSweeperArea : BuffScript
    {
        Vector3 targetPos;
        EffectEmitter particle;
        Region bubbleID;
        float lastTimeExecuted;
        public HextechSweeperArea(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            TeamId casterID = GetTeamID_CS(attacker);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out particle, out _, "Odin_HextechSweeper_tar_green.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            bubbleID = AddPosPerceptionBubble(casterID, 550, targetPos, 6, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            SpellEffectRemove(particle);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.OdinLightbringer(), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}