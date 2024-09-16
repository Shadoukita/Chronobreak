namespace Spells
{
    public class AstralBlessing : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 70, 140, 210, 280, 350 };
        int[] effect1 = { 25, 50, 75, 100, 125 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float tempAbilityPower = GetFlatMagicDamageMod(owner);
            float healthToRestore = effect0[level - 1];
            float nextBuffVars_AstralArmor = effect1[level - 1];
            float healingBonus = tempAbilityPower * 0.45f;
            healthToRestore += healingBonus;
            AddBuff(attacker, target, new Buffs.AstralBlessing(nextBuffVars_AstralArmor), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncHealth(target, healthToRestore, owner);
        }
    }
}
namespace Buffs
{
    public class AstralBlessing : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "soraka_astralBless_buf.troy", },
            BuffName = "Astral Blessing",
            BuffTextureName = "Soraka_Bless.dds",
        };
        float astralArmor;
        public AstralBlessing(float astralArmor = default)
        {
            this.astralArmor = astralArmor;
        }
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
            //RequireVar(this.astralArmor);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, astralArmor);
        }
    }
}