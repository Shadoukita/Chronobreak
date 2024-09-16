namespace Spells
{
    public class Disintegrate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 85, 125, 165, 205, 245 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle));
            if (count >= 1)
            {
                ApplyStun(attacker, target, charVars.StunDuration);
                SpellBuffRemove(owner, nameof(Buffs.Pyromania_particle), owner, 0);
            }
            float tempManaCost = GetPARCost(spell);
            float nextBuffVars_ManaCost = tempManaCost;
            AddBuff(attacker, target, new Buffs.Disintegrate(nextBuffVars_ManaCost), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 0, false, false, attacker);
            AddBuff(owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            TeamId teamID = GetTeamID_CS(owner);
            int annieSkinID = GetSkinID(owner);
            if (annieSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "DisintegrateHit_tar_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "Disintegrate_hit_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "DisintegrateHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "Disintegrate_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class Disintegrate : BuffScript
    {
        float manaCost;
        public Disintegrate(float manaCost = default)
        {
            this.manaCost = manaCost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaCost);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (!IsDead(attacker))
            {
                IncPAR(attacker, manaCost, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}