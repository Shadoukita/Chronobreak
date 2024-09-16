namespace Spells
{
    public class GalioRighteousGust : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.2f, 0.28f, 0.36f, 0.44f, 0.52f };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos); // UNUSED
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 1100, 0);
            Vector3 nextBuffVars_TargetPos = targetPos;
            Minion other3 = SpawnMinion("RighteousGustLauncher", "TestCubeRender", "idle.lua", ownerPos, teamID, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            AddBuff(owner, other3, new Buffs.GalioRighteousGust(nextBuffVars_TargetPos), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            AddBuff(attacker, attacker, new Buffs.GalioRighteousGustHaste(nextBuffVars_MoveSpeedMod), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GalioRighteousGust : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GalioRighteousGust",
            BuffTextureName = "",
        };
        Vector3 targetPos;
        int level;
        int[] effect0 = { 60, 105, 150, 195, 240 };
        public GalioRighteousGust(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetStealthed(owner, true);
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos;
            FaceDirection(owner, targetPos);
            float aPMod = GetFlatMagicDamageMod(attacker);
            IncPermanentFlatMagicDamageMod(owner, aPMod);
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GalioRighteousGustMissile));
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetStealthed(owner, true);
        }
        public override void OnSpellHit(AttackableUnit target)
        {
            int level = this.level;
            TeamId teamID = GetTeamID_CS(owner);
            Champion other1 = GetChampionBySkinName("Galio", teamID);
            BreakSpellShields(target);
            ApplyDamage(other1, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, other1);
            SpellEffectCreate(out _, out _, "galio_windTunnel_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
        }
    }
}