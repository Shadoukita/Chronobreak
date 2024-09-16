namespace Spells
{
    public class KennenShurikenHurlMissile1 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 75, 115, 155, 195, 235 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            float properDamage = effect0[level - 1];
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "Kennen_ts_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if (target is Champion)
                {
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "Kennen_ts_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                        SpellEffectCreate(out _, out _, "Kennen_ts_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}