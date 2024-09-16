namespace Spells
{
    public class AlZaharCalloftheVoid : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 firstPos = Vector3.Zero;
            Vector3 lastPos = Vector3.Zero;
            float lineWidth = 0; // UNITIALIZED
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            TeamId teamID = GetTeamID_CS(owner);
            bool foundFirstPos = false;
            foreach (Vector3 pos in GetPointsOnLine(ownerPos, targetPos, 750, distance, 15))
            {
                if (!foundFirstPos)
                {
                    firstPos = pos;
                    foundFirstPos = true;
                }
                lastPos = pos;
            }
            Minion other1 = SpawnMinion("Portal to the Void", "TestCubeRender", "idle.lua", firstPos, teamID, false, true, false, true, false, true, 300, false, false, (Champion)owner);
            Minion other2 = SpawnMinion("Portal to the Void", "TestCubeRender", "idle.lua", lastPos, teamID, false, true, false, true, false, true, 300 + lineWidth, false, false, (Champion)owner);
            FaceDirection(other1, targetPos);
            FaceDirection(other2, targetPos);
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(owner, other1, new Buffs.AlZaharCalloftheVoid(nextBuffVars_TargetPos), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, other2, new Buffs.AlZaharCalloftheVoid(nextBuffVars_TargetPos), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AlZaharCalloftheVoid : BuffScript
    {
        Vector3 targetPos;
        EffectEmitter particle3;
        EffectEmitter particle2;
        public AlZaharCalloftheVoid(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "voidflash.troy", default, TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            SpellEffectCreate(out particle3, out particle2, "voidportal_green.troy", "voidportal_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 targetPos = this.targetPos;
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellCast(attacker, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, owner.Position3D);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
            EffectEmitter nextBuffVars_Particle2 = particle2;
            EffectEmitter nextBuffVars_Particle3 = particle3;
            AddBuff(attacker, attacker, new Buffs.AlZaharCallR(nextBuffVars_Particle2, nextBuffVars_Particle3), 2, 1, 0.75f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
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