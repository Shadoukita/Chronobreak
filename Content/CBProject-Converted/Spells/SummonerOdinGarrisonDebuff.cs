namespace Spells
{
    public class SummonerOdinGarrisonDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class SummonerOdinGarrisonDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        EffectEmitter particle1;
        EffectEmitter particle2;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "Summoner_enemy_capture_buf_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            SpellEffectCreate(out particle2, out _, "Summoner_enemy_capture_buf_02.troy ", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            SpellEffectCreate(out _, out _, "Summoner_Flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.2f;
            }
        }
    }
}