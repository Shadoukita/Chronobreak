namespace Spells
{
    public class WrathoftheAncients : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.35f,
            SpellDamageRatio = 0.35f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, attacker, new Buffs.WrathoftheAncients(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class WrathoftheAncients : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_jaw", "L_jaw", },
            AutoBuffActivateEffect = new[] { "MouthFoam.troy", "MouthFoam.troy", },
            BuffName = "WrathoftheAncients",
            BuffTextureName = "Averdrian_AstralBeam.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.4f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.Stealth), false))
                {
                    AddBuff(attacker, unit, new Buffs.WrathDamage(), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}