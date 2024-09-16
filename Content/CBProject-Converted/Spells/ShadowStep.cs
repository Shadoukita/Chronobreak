namespace Spells
{
    public class ShadowStep : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 120, 160, 200, 240 };
        int[] effect1 = { 8, 12, 16, 20, 24 };
        float[] effect2 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "katarina_shadowStep_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            Vector3 castPos = GetUnitPosition(owner);
            int ownerskinid = GetSkinID(owner);
            if (ownerskinid == 6)
            {
                SpellEffectCreate(out _, out _, "katarina_shadowStep_Sand_return.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "katarina_shadowStep_return.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            FaceDirection(owner, target.Position3D);
            float distance = DistanceBetweenObjects(owner, target);
            float finalDistance = distance + 0;
            finalDistance += 250;
            Vector3 targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
            bool temp = IsPathable(targetPos);
            if (!temp)
            {
                finalDistance -= 200;
            }
            targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
            TeleportToPosition(owner, targetPos);
            level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float kIDamage = effect1[level - 1];
            float damageVar = effect0[level - 1];
            damageVar += kIDamage;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinct)) > 0)
            {
                float nextBuffVars_DamageReduction = effect2[level - 1];
                AddBuff(attacker, owner, new Buffs.ShadowStepDodge(nextBuffVars_DamageReduction), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.KillerInstinct), owner, 0);
            }
            if (target.Team != owner.Team)
            {
                SpellEffectCreate(out _, out _, "katarina_shadowStep_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, targetPos, target, default, default, true, false, false, false, false);
                ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                if (target is Champion)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}