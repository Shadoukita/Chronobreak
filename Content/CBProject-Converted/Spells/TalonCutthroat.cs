namespace Spells
{
    public class TalonCutthroat : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 120, 160, 200, 240 };
        int[] effect1 = { 1, 1, 1, 1, 1 };
        int[] effect2 = { 8, 12, 16, 20, 24 };
        float[] effect3 = { 1, 1.5f, 2, 2.5f, 3 };
        float[] effect4 = { 1.03f, 1.06f, 1.09f, 1.12f, 1.15f };
        public override void SelfExecute()
        {
            TeamId ownerTeam = GetTeamID_CS(owner);
            Vector3 castPos = GetUnitPosition(owner);
            SpellEffectCreate(out _, out _, "talon_E_cast.troy", default, ownerTeam, 1, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            FaceDirection(owner, target.Position3D);
            float distance = DistanceBetweenObjects(owner, target);
            float finalDistance = distance + 175;
            Vector3 targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
            TeleportToPosition(owner, targetPos);
            float damageVar = effect0[level - 1];
            float silenceDur = effect1[level - 1];
            level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float kIDamage = effect2[level - 1];
            damageVar += kIDamage;
            kIDamage = effect3[level - 1];
            if (target.Team != owner.Team)
            {
                SpellEffectCreate(out _, out _, "talon_E_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, targetPos, target, default, default, true, false, false, false, false);
                ApplySilence(attacker, target, silenceDur);
                float nextBuffVars_AmpValue = effect4[level - 1];
                AddBuff(attacker, target, new Buffs.TalonDamageAmp(nextBuffVars_AmpValue), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                if (target is Champion)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
            }
        }
    }
}