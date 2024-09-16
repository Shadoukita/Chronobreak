namespace Spells
{
    public class GalioRighteousGustMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 0.2f, 0.28f, 0.36f, 0.44f, 0.52f };
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSpellLevelPlusOne(spell);
            Vector3 targetPos = GetSpellTargetPos(spell);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            Minion other3 = SpawnMinion("RighteousGust", "TestCube", "idle.lua", missilePosition, teamID, false, false, false, false, false, true, 100, false, true);
            FaceDirection(other3, targetPos);
            AddBuff(owner, other3, new Buffs.GalioRighteousGustMissile(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GalioRighteousGustMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GalioRighteousGustMissile",
            BuffTextureName = "",
        };
        float moveSpeedMod;
        EffectEmitter windVFXAlly;
        EffectEmitter windVFXEnemy;
        int level; // UNUSED
        float lastTimeExecuted;
        public GalioRighteousGustMissile(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            SetNoRender(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetStealthed(owner, true);
            //RequireVar(this.moveSpeedMod);
            float moveSpeedMod = this.moveSpeedMod; // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 orientationPoint = GetPointByUnitFacingOffset(owner, 10000, 0);
            SpellEffectCreate(out windVFXAlly, out windVFXEnemy, "galio_windTunnel_rune.troy", "galio_windTunnel_rune_team_red.troy", teamID, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "head", owner.Position3D, owner, default, default, false, default, default, false, false, orientationPoint);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.ExtraSlots);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(windVFXAlly);
            SpellEffectRemove(windVFXEnemy);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetStealthed(owner, true);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            TeamId teamID = GetTeamID_CS(owner);
            Champion other1 = GetChampionBySkinName("Galio", teamID);
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 175, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.GalioRighteousGustMissile), false))
                {
                    if (unit != attacker)
                    {
                        if (IsInFront(attacker, unit))
                        {
                            if (IsBehind(unit, attacker))
                            {
                                AddBuff(other1, unit, new Buffs.GalioRighteousGustHaste(nextBuffVars_MoveSpeedMod), 1, 1, 0.3f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                ApplyAssistMarker(other1, unit, 10);
                            }
                            else
                            {
                                SpellBuffRemove(unit, nameof(Buffs.GalioRighteousGustHaste), other1, 0);
                            }
                        }
                        else
                        {
                            SpellBuffRemove(unit, nameof(Buffs.GalioRighteousGustHaste), other1, 0);
                        }
                    }
                }
            }
        }
    }
}