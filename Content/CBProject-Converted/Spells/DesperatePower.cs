namespace Spells
{
    public class DesperatePower : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.15f, 0.2f, 0.25f };
        int[] effect1 = { 5, 6, 7 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_Vamp = effect0[level - 1];
            int nextBuffVars_Level = level;
            AddBuff(attacker, target, new Buffs.DesperatePower(nextBuffVars_Vamp, nextBuffVars_Level), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class DesperatePower : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "DesperatePower",
            BuffTextureName = "Ryze_DesperatePower.dds",
            NonDispellable = true,
        };
        float vamp;
        int level;
        EffectEmitter asdf;
        public DesperatePower(float vamp = default, int level = default)
        {
            this.vamp = vamp;
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.vamp);
            //RequireVar(this.level);
            int level = this.level; // UNUSED
            IncPercentSpellVampMod(owner, vamp);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out asdf, out _, "ManaLeach_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf);
            int level = this.level; // UNUSED
        }
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, vamp);
        }
    }
}