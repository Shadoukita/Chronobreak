namespace Spells
{
    public class FiddlesticksDarkWind : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
            SpellFXOverrideSkins = new[] { "SurprisePartyFiddlesticks", },
        };
        EffectEmitter a; // UNUSED
        int[] effect0 = { 65, 85, 105, 125, 145 };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellBuffClear(owner, nameof(Buffs.FiddleSticksDarkWindMissile));
            TeamId teamID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float damageToDeal = effect0[level - 1];
            foreach (AttackableUnit unit in GetRandomUnitsInArea(attacker, target.Position3D, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 10, default, false))
            {
                if (unit != target && (!GetStealthed(unit) || CanSeeTarget(attacker, unit)))
                {
                    Vector3 attackerPos = GetUnitPosition(target);
                    level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                    break;
                }
            }
            AddBuff(attacker, attacker, new Buffs.FiddleSticksDarkWindMissile(), 5, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.DarkWind(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.SILENCE, 0, true, false, false);
            ApplyDamage(attacker, target, damageToDeal + effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.45f, 0, false, false, attacker);
            teamID = GetTeamID_CS(attacker);
            int fiddlesticksSkinID = GetSkinID(attacker);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out a, out _, "Party_DarkWind_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "DarkWind_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class FiddlesticksDarkWind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Silence.troy", },
            BuffName = "Silence",
            BuffTextureName = "Fiddlesticks_DarkWind.dds",
        };
    }
}