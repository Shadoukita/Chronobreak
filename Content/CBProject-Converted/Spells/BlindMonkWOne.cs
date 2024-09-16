namespace Spells
{
    public class BlindMonkWOne : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 40, 80, 120, 160, 200 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            float shieldAbsorb = effect0[level - 1];
            float bonusAP = GetFlatMagicDamageMod(owner);
            float bonusAP80 = bonusAP * 0.8f;
            shieldAbsorb += bonusAP80;
            float nextBuffVars_ShieldAbsorb = shieldAbsorb;
            if (target != attacker)
            {
                Vector3 ownerPos = GetUnitPosition(owner);
                SpellEffectCreate(out _, out _, "blindMonk_W_cas_01.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false, false);
                Vector3 targetPos = GetUnitPosition(target);
                float moveSpeed = GetMovementSpeed(owner);
                float dashSpeed = moveSpeed + 1350;
                float distance = DistanceBetweenObjects(owner, target);
                nextBuffVars_TargetPos = targetPos;
                nextBuffVars_Distance = distance;
                nextBuffVars_dashSpeed = dashSpeed;
                AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkWOneDash(nextBuffVars_ShieldAbsorb, nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                AddBuff(owner, owner, new Buffs.BlindMonkWManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                AddBuff(owner, owner, new Buffs.BlindMonkWOneShield(nextBuffVars_ShieldAbsorb), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.BlindMonkWManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkWOne : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlindMonkSafeguard",
        };
    }
}