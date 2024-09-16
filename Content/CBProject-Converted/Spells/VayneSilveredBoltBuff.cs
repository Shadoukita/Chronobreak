namespace Spells
{
    public class VayneSilveredBoltBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "", },
        };
    }
}
namespace Buffs
{
    public class VayneSilveredBoltBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "VayneSilverBolts",
            BuffTextureName = "Vayne_SilveredBolts.dds",
            IsDeathRecapSource = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        /*
        //TODO: Uncomment and fix
        public override void OnHitUnit(AttackableUnit target, float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            bool isBlinded; // UNITIALIZED
            bool canMove = GetCanMove(owner); // UNUSED
            if(!isBlinded)
            {
                returnValue = true;
            }
            else
            {
                if(target is ObjAIBase && target is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
                {
                    int count = GetBuffCountFromCaster(target, attacker, nameof(Buffs.VayneSilveredDebuff));
                    if(count == 2)
                    {
                        TeamId teamID = GetTeamID(attacker);
                        SpellEffectCreate(out gragas, out _, "vayne_W_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, target.Position3D, target, default, default, true, false, false, false, false);
                    }
                    AddBuff(attacker, target, new Buffs.VayneSilveredDebuff(), 3, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    if(count == 2)
                    {
                        ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                        damageAmount = 0;
                    }
                }
            }
        }
        */
    }
}