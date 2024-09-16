namespace Spells
{
    public class Starcall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.35f,
            SpellDamageRatio = 0.35f,
        };
        int[] effect0 = { 60, 85, 110, 135, 160 };
        int[] effect1 = { -8, -9, -10, -11, -12 };
        public override bool CanCast()
        {
            bool temp = false;
            foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 610, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                temp = true;
            }
            return temp;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int nextBuffVars_DamageToDeal = effect0[level - 1];
            int nextBuffVars_StarcallShred = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.StarcallDamage(nextBuffVars_DamageToDeal, nextBuffVars_StarcallShred), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Starcall : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Starcall",
            BuffTextureName = "Soraka_Starcall.dds",
        };
        float resistanceMod;
        public Starcall(float resistanceMod = default)
        {
            this.resistanceMod = resistanceMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.resistanceMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, resistanceMod);
        }
    }
}