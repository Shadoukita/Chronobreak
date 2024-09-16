namespace Spells
{
    public class TwitchSprayandPrayAttack : SprayAndPrayAttack { }
    public class SprayAndPrayAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "GangsterTwitch", "PunkTwitch", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId twitchTeamId = GetTeamID_CS(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            int twitchSkinID = GetSkinID(attacker);
            if (target is ObjAIBase)
            {
                if (twitchSkinID == 4)
                {
                    SpellEffectCreate(out _, out _, "twitch_gangster_sprayandPray_tar.troy", default, twitchTeamId, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                else if (twitchSkinID == 5)
                {
                    SpellEffectCreate(out _, out _, "twitch_punk_sprayandPray_tar.troy", default, twitchTeamId, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "twitch_sprayandPray_tar.troy", default, twitchTeamId, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
            }
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, owner);
        }
    }
}