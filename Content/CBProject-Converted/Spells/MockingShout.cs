namespace Spells
{
    public class MockingShout : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { -0.3f, -0.375f, -0.45f, -0.525f, -0.6f };
        int[] effect1 = { -20, -35, -50, -65, -80 };
        public override bool CanCast()
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    return true;
                }
                return false;
            }
            return true; //TODO: Verify
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_DamageMod;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FacingMe)) > 0)
            {
                nextBuffVars_MoveSpeedMod = effect0[level - 1];
                nextBuffVars_DamageMod = effect1[level - 1];
                AddBuff(attacker, target, new Buffs.MockingShoutSlow(nextBuffVars_MoveSpeedMod), 1, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.MockingShout(nextBuffVars_DamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                if (IsBehind(target, owner))
                {
                    nextBuffVars_MoveSpeedMod = effect0[level - 1];
                    nextBuffVars_DamageMod = effect1[level - 1];
                    AddBuff(attacker, target, new Buffs.MockingShoutSlow(nextBuffVars_MoveSpeedMod), 1, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.MockingShout(nextBuffVars_DamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    nextBuffVars_DamageMod = effect1[level - 1];
                    AddBuff(attacker, target, new Buffs.MockingShout(nextBuffVars_DamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class MockingShout : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Mocking Shout",
            BuffTextureName = "48thSlave_Pacify.dds",
        };
        float damageMod;
        public MockingShout(float damageMod = default)
        {
            this.damageMod = damageMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.damageMod);
            ApplyAssistMarker(attacker, target, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
        }
    }
}