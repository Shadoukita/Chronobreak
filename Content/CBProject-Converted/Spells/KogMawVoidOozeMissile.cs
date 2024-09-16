namespace Spells
{
    public class KogMawVoidOozeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { -0.28f, -0.36f, -0.44f, -0.52f, -0.6f };
        int[] effect1 = { 60, 110, 160, 210, 260 };
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_SlowPercent = effect0[level - 1];
            Vector3 groundHeight = GetGroundHeight(missilePosition);
            groundHeight.Y += 10;
            Vector3 nextBuffVars_targetPos = groundHeight;
            AddBuff(owner, owner, new Buffs.KogMawVoidOozeMissile(nextBuffVars_SlowPercent, nextBuffVars_targetPos), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
        /*
        //TODO: Uncomment and fix
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(owner.Team != target.Team)
            {
                TeamId casterID2; // UNITIALIZED
                ApplyDamage(attacker, target, this.effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "KogMawVoidOoze_tar.troy", default, casterID2, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
        */
    }
}
namespace Buffs
{
    public class KogMawVoidOozeMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "KogMaw_VoidOoze.dds",
        };
        float slowPercent;
        Vector3 targetPos;
        EffectEmitter particle;
        EffectEmitter particle2;
        float lastTimeExecuted;
        public KogMawVoidOozeMissile(float slowPercent = default, Vector3 targetPos = default)
        {
            this.slowPercent = slowPercent;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
            //RequireVar(this.targetPos);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out particle, out particle2, "KogMawVoidOoze_green.troy", "KogMawVoidOoze_red.troy", teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos = this.targetPos;
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_SlowPercent = slowPercent;
                    AddBuff(attacker, unit, new Buffs.KogMawVoidOozeSlow(nextBuffVars_SlowPercent), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}