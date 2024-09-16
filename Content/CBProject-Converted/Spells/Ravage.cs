namespace Spells
{
    public class EvelynnE : Ravage { }
    public class Ravage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 135, 190, 255, 320 };
        int[] effect1 = { -10, -14, -18, -22, -26 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
            float nextBuffVars_ArmorMod = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.Ravage(nextBuffVars_ArmorMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class EvelynnE : Ravage { }
    public class Ravage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "RendingShot_buf.troy", },
            BuffName = "Ravage",
            BuffTextureName = "Evelynn_Ravage.dds",
        };
        float armorMod;
        public Ravage(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
            IncFlatSpellBlockMod(owner, armorMod);
        }
    }
}