namespace CharScripts
{
    public class CharScriptSion : CharScript
    {
        float blockAmount;
        float finalDamage;
        float[] effect0 = { 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f };
        int[] effect1 = { 30, 30, 30, 30, 30, 30, 40, 40, 40, 40, 40, 40, 50, 50, 50, 50, 50, 50 };
        public override void SetVarsByLevel()
        {
            charVars.BlockChance = effect0[level - 1];
            charVars.BaseBlockAmount = effect1[level - 1];
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (damageSource == DamageSource.DAMAGE_SOURCE_ATTACK && RandomChance() < charVars.BlockChance)
            {
                blockAmount = Math.Min(charVars.BaseBlockAmount, damageAmount);
                finalDamage = damageAmount - blockAmount;
                SpellEffectCreate(out _, out _, "FeelNoPain_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                damageAmount = finalDamage;
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.FeelNoPain(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, default, false);
        }
    }
}