namespace Spells
{
    public class Crystallize : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int meltingTime; // UNUSED
        int[] effect0 = { 4, 5, 6, 7, 8 };
        int[] effect1 = { 400, 500, 600, 700, 800 };
        int[] effect2 = { 200, 250, 300, 350, 400 };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Crystallize)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                meltingTime = 5;
                Vector3 targetPos = GetSpellTargetPos(spell);
                Vector3 ownerPos = GetUnitPosition(owner);
                float distance = DistanceBetweenPoints(ownerPos, targetPos);
                TeamId teamID = GetTeamID_CS(owner);
                int iter = effect0[level - 1];
                float lineWidth = effect1[level - 1];
                int halfLength = effect2[level - 1]; // UNUSED
                bool foundFirstPos = false; // UNUSED
                Vector3 facingPoint = GetPointByUnitFacingOffset(owner, 9999, 0);
                foreach (Vector3 pos in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, iter))
                {
                    Minion other2 = SpawnMinion("IceBlock", "AniviaIceblock", "idle.lua", pos, teamID, true, true, true, true, false, true, 0, false, false);
                    FaceDirection(other2, facingPoint);
                    AddBuff(owner, other2, new Buffs.Crystallize(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SetGhostProof(other2, true);
                }
            }
        }
    }
}
namespace Buffs
{
    public class Crystallize : BuffScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                float pushDistance;
                Vector3 targetPos;
                bool ghosted = GetGhosted(unit);
                if (unit is Champion)
                {
                    pushDistance = 120;
                }
                else
                {
                    pushDistance = 250;
                }
                if (IsBehind(owner, unit))
                {
                    targetPos = GetPointByUnitFacingOffset(owner, pushDistance, 180);
                }
                else
                {
                    targetPos = GetPointByUnitFacingOffset(owner, pushDistance, 0);
                }
                Vector3 nextBuffVars_TargetPos = targetPos;
                if (attacker.Team != unit.Team)
                {
                    ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                }
                if (!ghosted)
                {
                    AddBuff(attacker, unit, new Buffs.GlobalWallPush(nextBuffVars_TargetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, target, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
        }
    }
}