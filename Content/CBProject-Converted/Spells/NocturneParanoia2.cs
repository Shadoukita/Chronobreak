namespace Spells
{
    public class NocturneParanoia2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        EffectEmitter greenDash;
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            else if (!canCast)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.NocturneParanoia)) > 0)
            {
                foreach (Champion unit in GetChampions(GetEnemyTeam(teamOfOwner), default, true))
                {
                    SpellBuffRemove(unit, nameof(Buffs.NocturneParanoiaTargeting), attacker);
                }
                Vector3 ownerPos = GetUnitPosition(attacker); // UNUSED
                Vector3 targetPos = GetUnitPosition(target);
                float distance = DistanceBetweenObjects(owner, target);
                int nextBuffVars_dashSpeed = 1800;
                Vector3 nextBuffVars_TargetPos = targetPos;
                float nextBuffVars_Distance = distance; // UNUSED
                AddBuff(owner, owner, new Buffs.UnstoppableForceMarker(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out greenDash, out _, "NocturneParanoiaTeamTarget.troy", default, GetEnemyTeam(teamOfOwner), 0, 0, teamOfOwner, default, default, false, target, default, default, target, default, default, false, default, default, false);
                EffectEmitter nextBuffVars_GreenDash = greenDash;
                AddBuff((ObjAIBase)target, owner, new Buffs.NocturneParanoiaDash(nextBuffVars_dashSpeed, nextBuffVars_TargetPos, nextBuffVars_GreenDash), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                SpellBuffRemove(attacker, nameof(Buffs.NocturneParanoia), attacker);
            }
        }
    }
}
namespace Buffs
{
    public class NocturneParanoia2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "NocturneParanoia",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
    }
}