namespace Spells
{
    public class AlphaStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 100, 150, 200, 250, 300 };
        float[] effect1 = { 0.2f, 0.3f, 0.4f, 0.5f, 0.6f };
        int[] effect2 = { 5, 5, 5, 5, 5 };
        public override bool CanCast()
        {
            return GetCanMove(owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int targetNum = GetSpellTargetsHitPlusOne(spell);
            if (targetNum == 1)
            {
                AddBuff(owner, target, new Buffs.AlphaStrikeMarker(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            int nextBuffVars_BaseDamage = effect0[level - 1];
            float nextBuffVars_ChanceToKill = effect1[level - 1];
            AddBuff(owner, target, new Buffs.AlphaStrikeTarget(nextBuffVars_BaseDamage, nextBuffVars_ChanceToKill), 1, 1, effect2[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AlphaStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Alpha Striking",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        bool alphaStrikeLaunched;
        public override void OnActivate()
        {
            alphaStrikeLaunched = false;
        }
        public override void OnDeactivate(bool expired)
        {
            if (alphaStrikeLaunched)
            {
                SetCanAttack(owner, true);
                SetCanMove(owner, true);
                SetGhosted(owner, false);
                SetNoRender(owner, false);
                SetTargetable(owner, true);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            if (!IsDead(owner))
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position3D, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            if (alphaStrikeLaunched)
            {
                SetGhosted(owner, true);
                SetNoRender(owner, true);
                SetCanAttack(owner, false);
                SetCanMove(owner, false);
                SetTargetable(owner, false);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if (spellName == nameof(Spells.AlphaStrike))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            alphaStrikeLaunched = true;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetCanMove(owner, false);
        }
    }
}