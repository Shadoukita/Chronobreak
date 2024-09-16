namespace Spells
{
    public class KennenBringTheLight : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particleID; // UNUSED
        int[] effect0 = { 65, 95, 125, 155, 185 };
        public override bool CanCast()
        {
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KennenMarkofStorm), true))
            {
                return true;
            }
            return false;
        }
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 925, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.KennenMarkofStorm)) > 0)
                {
                    BreakSpellShields(unit);
                    AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    float baseDamage = effect0[level - 1];
                    SpellEffectCreate(out particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, attacker, "head", default, unit, "root", default, true);
                    ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.55f, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "kennen_btl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                }
            }
        }
    }
}