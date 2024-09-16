namespace Spells
{
    public class Hallucinate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 300, 450, 600 };
        float[] effect1 = { 0.7f, 0.7f, 0.7f };
        float[] effect2 = { 1.35f, 1.35f, 1.35f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.SHRED);
            bool isStealthed = GetStealthed(owner);
            DestroyMissileForTarget(owner);
            if (!isStealthed)
            {
                SpellCast(owner, owner, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            else
            {
                Vector3 pos = GetRandomPointInAreaUnit(owner, 100, 0);
                Pet other1 = CloneUnitPet(owner, nameof(Buffs.Hallucinate), 18, pos, 0, 0, true);
                int nextBuffVars_DamageAmount = effect0[level - 1];
                float nextBuffVars_DamageDealt = effect1[level - 1];
                float nextBuffVars_DamageTaken = effect2[level - 1];
                AddBuff(attacker, other1, new Buffs.HallucinateFull(nextBuffVars_DamageAmount, nextBuffVars_DamageDealt, nextBuffVars_DamageTaken), 1, 1, 18, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, other1, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, other1, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, other1, new Buffs.Backstab(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SetStealthed(other1, false);
            }
        }
    }
}
namespace Buffs
{
    public class Hallucinate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hallucinate",
            BuffTextureName = "Jester_HallucinogenBomb.dds",
            IsPetDurationBuff = true,
        };
    }
}