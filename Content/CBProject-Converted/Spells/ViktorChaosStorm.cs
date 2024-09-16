namespace Spells
{
    public class ViktorChaosStorm : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        Fade blah; // UNUSED
        int[] effect0 = { 0, 400, 800, 600, 800 };
        int[] effect1 = { 0, 25, 50 };
        int[] effect2 = { 150, 250, 350 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Pet other1 = SpawnPet("Tibbers", "TempMovableChar", nameof(Buffs.InfernalGuardian), "StormIdle.lua", 7, targetPos, effect0[level - 1], effect1[level - 1]);
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                AddBuff(owner, unit, new Buffs.ViktorChaosStormGuide(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            SetTargetable(other1, false);
            SetInvulnerable(other1, true);
            StopMove(other1);
            blah = PushCharacterFade(other1, 0, 0);
            AddBuff(owner, other1, new Buffs.ViktorChaosStormAOE(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.ViktorChaosStormTimer(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.ViktorExpirationTimer(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ViktorChaosStormGuide));
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 spellTargetPos = GetSpellTargetPos(spell); // UNUSED
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect2[level - 1];
            float aPPreMod = GetFlatMagicDamageMod(owner);
            float aPPostMod = 0.55f * aPPreMod;
            float finalDamage = baseDamage + aPPostMod;
            BreakSpellShields(target);
            ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            ApplySilence(owner, target, 0.5f);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 2000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormAOE), true))
            {
                SpellEffectCreate(out _, out _, "Viktor_ChaosStorm_hit.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "Head", default, target, "Spine", default, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class ViktorChaosStorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsPetDurationBuff = true,
        };
    }
}