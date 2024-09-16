namespace Spells
{
    public class RumbleGrenadeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 55, 85, 115, 145, 175 };
        int[] effect1 = { 3, 3, 3, 3, 3 };
        float[] effect2 = { -0.3f, -0.4f, -0.5f, -0.6f, -0.7f };
        float[] effect3 = { -0.15f, -0.2f, -0.25f, -0.3f, -0.35f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_SlowAmount;
            TeamId teamID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float dmg = effect0[level - 1];
            float disable = effect1[level - 1];
            float aP = 0.5f;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeDZ)) > 0)
            {
                dmg *= 1.3f;
                aP *= 1.3f;
            }
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.RumbleGrenadeDebuff)) > 0)
            {
                AddBuff(attacker, target, new Buffs.RumbleGrenadeZapEffect(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                nextBuffVars_SlowAmount = effect2[level - 1];
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeDZ)) > 0)
                {
                    nextBuffVars_SlowAmount *= 1.3f;
                }
                AddBuff(attacker, target, new Buffs.RumbleGrenadeSlow(nextBuffVars_SlowAmount), 1, 1, disable, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.RumbleGrenadeZapEffect(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                nextBuffVars_SlowAmount = effect3[level - 1];
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeDZ)) > 0)
                {
                    nextBuffVars_SlowAmount *= 1.3f;
                }
                AddBuff(attacker, target, new Buffs.RumbleGrenadeDebuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.RumbleGrenadeSlow(nextBuffVars_SlowAmount), 1, 1, disable, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            if (target is Champion)
            {
                SpellEffectCreate(out _, out _, "rumble_taze_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, aP, 0, false, false, attacker);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                bool isStealthed = GetStealthed(target);
                if (!isStealthed)
                {
                    SpellEffectCreate(out _, out _, "rumble_taze_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                    ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, aP, 0, false, false, attacker);
                    DestroyMissile(missileNetworkID);
                    AddBuff(attacker, target, new Buffs.RumbleGrenadeZapEffect(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class RumbleGrenadeMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "RumbleGrenadeDebuff",
            BuffTextureName = "Soraka_Starcall.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}