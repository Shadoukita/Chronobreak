namespace Spells
{
    public class BrandScorchAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            Vector3 targetPos = GetUnitPosition(target);
            Minion other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, default, true, (Champion)attacker);
            AddBuff(attacker, other3, new Buffs.BrandScorchGround(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float damageAmount = GetTotalAttackDamage(owner);
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.BrandScorch), owner);
        }
    }
}