namespace Spells
{
    public class Incinerate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 80, 130, 180, 230, 280 };
        public override void SelfExecute()
        {
            charVars.SpellWillStun = false;
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle));
            if (count >= 1)
            {
                charVars.SpellWillStun = true;
                SpellBuffRemove(owner, nameof(Buffs.Pyromania_particle), owner, 0);
            }
            AddBuff(owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 0, false, false, attacker);
            if (charVars.SpellWillStun)
            {
                ApplyStun(attacker, target, charVars.StunDuration);
            }
            TeamId teamID = GetTeamID_CS(owner);
            int annieSkinID = GetSkinID(owner);
            if (annieSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "Incinerate_buf_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "Incinerate_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}