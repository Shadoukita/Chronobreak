namespace Spells
{
    public class Landslide : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "ReefMalphite", },
        };
        EffectEmitter partname; // UNUSED
        int[] effect0 = { 60, 100, 140, 180, 220 };
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_Level = level;
            float armorAmount = GetArmor(owner);
            float baseDamage = effect0[level - 1];
            armorAmount *= 0.5f;
            float armorDamage = armorAmount + baseDamage;
            int malphiteSkinID = GetSkinID(owner);
            if (malphiteSkinID == 2)
            {
                SpellEffectCreate(out partname, out _, "landslide_blue_nova.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out partname, out _, "landslide_nova.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, armorDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.LandslideDebuff(nextBuffVars_Level), 1, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class Landslide : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}