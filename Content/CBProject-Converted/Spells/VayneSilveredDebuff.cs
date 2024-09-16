namespace Spells
{
    public class VayneSilveredDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
    }
}
namespace Buffs
{
    public class VayneSilveredDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneSilverDebuff",
            BuffTextureName = "Vayne_SilveredBolts.dds",
        };
        bool doOnce2;
        EffectEmitter globeTwo;
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            TeamId teamID = GetTeamID_CS(caster);
            TeamId teamIDTarget = GetTeamID_CS(target); // UNUSED
            int count = GetBuffCountFromCaster(owner, caster, nameof(Buffs.VayneSilveredDebuff));
            if (count == 1)
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, caster.Position3D, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, nameof(Buffs.VayneSilveredDebuff), true))
                {
                    SpellBuffRemove(unit, nameof(Buffs.VayneSilveredDebuff), caster, 0);
                    SpellBuffRemove(unit, nameof(Buffs.VayneSilveredDebuff), caster, 0);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.VayneSilverParticle1(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else if (count == 2)
            {
                SpellBuffRemove(owner, nameof(Buffs.VayneSilverParticle1), (ObjAIBase)owner, 0);
                doOnce2 = true;
                SpellEffectCreate(out globeTwo, out _, "vayne_W_ring2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.VayneSilverParticle1), (ObjAIBase)owner, 0);
            if (doOnce2)
            {
                SpellEffectRemove(globeTwo);
                doOnce2 = false;
            }
        }
    }
}