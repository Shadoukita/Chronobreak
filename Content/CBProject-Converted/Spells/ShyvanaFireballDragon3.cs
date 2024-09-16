namespace Spells
{
    public class ShyvanaFireballDragon3 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 70, 110, 150, 190, 230 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "Incinerate_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            float spellBaseDamage = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 pointToSpawn = GetPointByUnitFacingOffset(owner, 25, 0);
            Vector3 pointToFace = GetPointByUnitFacingOffset(owner, -100, 0);
            Minion other1 = SpawnMinion("ConeBreathMarker", "TestCubeRender10Vision", "idle.lua", pointToSpawn, teamID, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            FaceDirection(other1, pointToFace);
            AddBuff(attacker, other1, new Buffs.ShyvanaFireballDragonMinion(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}