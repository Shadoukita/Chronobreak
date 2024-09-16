namespace CharScripts
{
    public class CharScriptLux : CharScript
    {
        int[] effect0 = { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190 };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.LuxLightBinding))
            {
                charVars.FirstTargetHit = false;
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.LuxDeath(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int level = GetLevel(owner);
            charVars.IlluminateDamage = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.LuxIlluminationPassive(), 1, 1, 250000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            charVars.IlluminateDamage = effect0[level - 1];
        }
        public override void OnResurrect()
        {
            SpellBuffRemove(owner, nameof(Buffs.LuxDeathParticle), owner, 0);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}