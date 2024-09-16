namespace Buffs
{
    public class Camouflage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Camouflage",
            BuffTextureName = "Teemo_Camouflage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Vector3 lastPosition;
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            lastPosition = GetUnitPosition(owner);
        }
        public override void OnUpdateActions()
        {
            Vector3 curPosition = GetUnitPosition(owner);
            Vector3 lastPosition = this.lastPosition;
            float distance = DistanceBetweenPoints(curPosition, lastPosition);
            if (distance != 0)
            {
                this.lastPosition = curPosition;
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            bool isInvuln = GetInvulnerable(owner);
            if (isInvuln)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (IsDead(owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannel)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CamouflageCheck)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.CamouflageStealth)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageStealth(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0.1f, true, false, false);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else if (!spellVars.CastingBreaksStealth)
            {
            }
            else
            {
                if (!spellVars.DoesntTriggerSpellCasts)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not ObjAIBase)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnLaunchAttack(AttackableUnit target)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}