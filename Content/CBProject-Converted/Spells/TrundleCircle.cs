namespace Spells
{
    public class TrundleCircle : SpellScript
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
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Crystallize)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 targetPos = GetSpellTargetPos(spell);
                TeamId teamID = GetTeamID_CS(owner);
                Minion other3 = SpawnMinion("PlagueBlock", "TrundleWall", "idle.lua", targetPos, teamID, true, true, true, true, false, true, 0, false, false);
                int nextBuffVars_ID = 1; // UNUSED
                AddBuff(owner, other3, new Buffs.TrundleCircle(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                FaceDirection(other3, owner.Position3D);
            }
        }
    }
}
namespace Buffs
{
    public class TrundleCircle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleCircle",
            BuffTextureName = "XinZhao_CrescentSweep.dds",
        };
        EffectEmitter particle;
        EffectEmitter particle2;
        float[] effect0 = { -0.25f, -0.3f, -0.35f, -0.4f, -0.45f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out particle2, "trundle_PlagueBlock_green.troy", "trundle_PlagueBlock_red.troy", teamID, 10, 0, TeamId.TEAM_ORDER, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.iD);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhostProof(owner, true);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 180, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.GlobalWallPush), false))
            {
                ApplyAssistMarker(attacker, unit, 10);
                float offsetAngle = GetOffsetAngle(owner, unit.Position3D);
                Vector3 targetPos = GetPointByUnitFacingOffset(owner, 200, offsetAngle);
                Vector3 nextBuffVars_TargetPos = targetPos;
                AddBuff(attacker, unit, new Buffs.GlobalWallPush(nextBuffVars_TargetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (attacker.Team != unit.Team)
                {
                    ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
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
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 360, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}