namespace Buffs
{
    public class Soulsteal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, charVars.MagicDamageMod);
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                charVars.MagicDamageMod += 10;
                charVars.MagicDamageMod = Math.Min(charVars.MagicDamageMod, 70);
                SpellEffectCreate(out _, out _, "MejaisSoulstealer_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                float tempMana = GetPAR(target, PrimaryAbilityResourceType.MANA);
                IncPAR(owner, tempMana);
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (target is Champion)
            {
                charVars.MagicDamageMod += 5;
                charVars.MagicDamageMod = Math.Min(charVars.MagicDamageMod, 70);
                SpellEffectCreate(out _, out _, "MejaisSoulstealer_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}