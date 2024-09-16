namespace Spells
{
    public class YorickRavenous : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = { 0.4f, 0.4f, 0.4f, 0.4f, 0.4f };
        int[] effect1 = { 55, 85, 115, 145, 175 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonRavenous)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickSummonRavenous));
            }
            AddBuff(owner, target, new Buffs.YorickRavenousPrimaryTarget(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            BreakSpellShields(target);
            float distance = DistanceBetweenObjects(owner, target);
            distance += 250;
            Vector3 targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            SpellCast(owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            float nextBuffVars_DrainPercent = effect0[level - 1];
            bool nextBuffVars_DrainedBool = false;
            AddBuff(owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float baseDamage = effect1[level - 1];
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            float damageToDeal = baseDamage + bonusAD;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "yorick_ravenousGhoul_activeHeal.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            SpellEffectCreate(out _, out _, "yorick_ravenousGhoul_cas_tar.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
        }
    }
}