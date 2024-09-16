namespace Spells
{
    public class ScoutsBounty : SpellScript
    {
        int[] effect0 = { 100, 150, 200 };
        int[] effect1 = { -30, -45, -60 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_BonusGold = effect0[level - 1];
            float nextBuffVars_ArmorReduction = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.ScoutsBounty(nextBuffVars_ArmorReduction, nextBuffVars_BonusGold), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class ScoutsBounty : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ArchersMark_tar.troy", },
            BuffName = "Scout's Bounty",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        float armorReduction;
        float bonusGold;
        Region bubbleID;
        public ScoutsBounty(float armorReduction = default, float bonusGold = default)
        {
            this.armorReduction = armorReduction;
            this.bonusGold = bonusGold;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorReduction);
            //RequireVar(this.bonusGold);
            TeamId casterID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(casterID, 1200, owner, 60, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorReduction);
            if (IsDead(owner))
            {
                IncGold(attacker, bonusGold);
                SpellBuffRemove(owner, nameof(Buffs.ScoutsBounty), attacker);
            }
        }
    }
}