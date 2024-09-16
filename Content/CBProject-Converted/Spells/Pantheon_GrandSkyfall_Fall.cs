namespace Spells
{
    public class PantheonRFall : Pantheon_GrandSkyfall_Fall { }
    public class Pantheon_GrandSkyfall_Fall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChannelDuration = 1.5f,
            DoesntBreakShields = true,
        };
        int[] effect0 = { 400, 700, 1000 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            SetCameraPosition((Champion)owner, targetPos);
            Vector3 nextBuffVars_TargetPos = targetPos;
            int nextBuffVars_DamageRank = effect0[level - 1]; // UNUSED
            AddBuff(owner, owner, new Buffs.Pantheon_GrandSkyfall_Fall(nextBuffVars_TargetPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 2, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GrandSkyfall_Fall), owner, 0);
        }
    }
}
namespace Buffs
{
    public class Pantheon_GrandSkyfall_Fall : BuffScript
    {
        Vector3 targetPos;
        int _1ce;
        public Pantheon_GrandSkyfall_Fall(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.damageRank);
            SetCanAttack(owner, false);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetNoRender(owner, true);
            _1ce = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_GS_ParticleRed)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_ParticleRed), (ObjAIBase)owner, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_GS_Particle)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_Particle), (ObjAIBase)owner, 0);
            }
            Vector3 targetPos = charVars.TargetPos;
            targetPos = GetUnitPosition(target);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "pantheon_grandskyfall_land.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, targetPos, target, default, targetPos, true, default, default, false, false);
            targetPos = this.targetPos;
            int nextBuffVars_AttackSpeedMod = 0; // UNUSED
            float nextBuffVars_MoveSpeedMod = -0.35f; // UNUSED
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            SetCanAttack(owner, true);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            SetNoRender(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GrandSkyfall_FallD(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            if (_1ce == 0)
            {
                Vector3 targetPos = this.targetPos;
                TeleportToPosition(owner, targetPos);
                _1ce = 1;
            }
        }
    }
}