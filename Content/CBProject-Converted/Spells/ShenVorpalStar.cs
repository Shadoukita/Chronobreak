namespace Spells
{
    public class ShenVorpalStar : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 70, 115, 140, 175, 210 };
        float[] effect1 = { 6, 8.66f, 11.33f, 14, 16.66f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 0, false, false, attacker);
            float nextBuffVars_LifeTapMod = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.ShenVorpalStar(nextBuffVars_LifeTapMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            SpellEffectCreate(out _, out _, "shen_vorpalStar_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
        }
    }
}
namespace Buffs
{
    public class ShenVorpalStar : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Vorpal Star",
            BuffTextureName = "Shen_VorpalBlade.dds",
        };
        float lifeTapMod;
        EffectEmitter slow;
        public ShenVorpalStar(float lifeTapMod = default)
        {
            this.lifeTapMod = lifeTapMod;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            //RequireVar(this.lifeTapMod);
            SpellEffectCreate(out slow, out _, "shen_life_tap_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(slow);
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (attacker is Champion)
            {
                ObjAIBase caster = GetBuffCasterUnit();
                float nextBuffVars_LifeTapMod = lifeTapMod;
                AddBuff(caster, attacker, new Buffs.ShenVorpalStarHeal(nextBuffVars_LifeTapMod), 1, 1, 2.9f, BuffAddType.REPLACE_EXISTING, BuffType.HEAL, 0, true, false, false);
            }
        }
    }
}