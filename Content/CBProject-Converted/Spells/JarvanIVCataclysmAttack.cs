namespace Spells
{
    public class JarvanIVCataclysmAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            CastTime = 0.4f,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 200, 325, 450 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float physPreMod = GetFlatPhysicalDamageMod(owner);
            float physPostMod = 1.5f * physPreMod;
            float damageToDeal = physPostMod + baseDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, attacker);
            AddBuff(attacker, attacker, new Buffs.JarvanIVCataclysmCheck(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), owner, 0);
        }
    }
}
namespace Buffs
{
    public class JarvanIVCataclysmAttack : BuffScript
    {
        public override void OnActivate()
        {
            ShowHealthBar(attacker, true);
            SetTargetable(attacker, false);
            SetInvulnerable(attacker, true);
            SetCanMove(attacker, false);
            SetIgnoreCallForHelp(attacker, true);
            SetCallForHelpSuppresser(attacker, true);
            SetForceRenderParticles(attacker, true);
            SetNoRender(attacker, true);
            SetSuppressCallForHelp(attacker, true);
            SetCanAttack(attacker, false);
            SetGhostProof(attacker, true);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID; // UNITIALIZED
            teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
            Vector3 ownerPos = GetUnitPosition(attacker);
            SpellEffectCreate(out _, out _, "JarvanWallCrumble.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "root", ownerPos, owner, default, default, true, false, false, false, false);
            SetInvulnerable(attacker, false);
            ApplyDamage(attacker, attacker, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}