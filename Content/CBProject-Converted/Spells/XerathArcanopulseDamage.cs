namespace Spells
{
    public class XerathArcanopulseDamage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particleID; // UNUSED
        EffectEmitter particleID2; // UNUSED
        int[] effect0 = { 75, 115, 155, 195, 235 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 beam1 = GetPointByUnitFacingOffset(owner, 145, 0);
            Vector3 beam3 = GetPointByUnitFacingOffset(owner, 1100, 0);
            Minion other1 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam1, teamOfOwner, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            Minion other3 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam3, teamOfOwner, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            FaceDirection(other1, other3.Position3D);
            LinkVisibility(other1, other3);
            AddBuff(other3, other1, new Buffs.XerathArcanopulsePartFix(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other1, new Buffs.XerathArcanopulsePartFix2(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetForceRenderParticles(other1, true);
            SetForceRenderParticles(other3, true);
            SpellEffectCreate(out particleID, out particleID2, "XerathR_beam.troy", "XerathR_beam.troy", teamOfOwner, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other3, "top", default, other1, "top", default, true, false, false, false, false);
            Vector3 damagePoint = GetPointByUnitFacingOffset(owner, 500, 0);
            foreach (AttackableUnit unit in GetUnitsInRectangle(owner, damagePoint, 95, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out _, out _, "Xerath_beam_hit.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.XerathMageChains)) > 0)
                {
                    SpellEffectCreate(out _, out _, "Xerath_MageChains_consume.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff(owner, unit, new Buffs.XerathMageChainsRoot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, true, false);
                    SpellBuffRemove(unit, nameof(Buffs.XerathMageChains), owner, 0);
                }
            }
        }
    }
}