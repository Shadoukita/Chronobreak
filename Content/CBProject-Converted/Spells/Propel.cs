namespace Spells
{
    public class Propel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, target, new Buffs.Propel(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0);
        }
    }
}
namespace Buffs
{
    public class Propel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Propelled",
            BuffTextureName = "Minotaur_Pulverize.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            Vector3 bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
            Move(target, bouncePos, 100, 25, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage(attacker, owner, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            bool temp = IsMoving(owner);
            if (!temp)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}