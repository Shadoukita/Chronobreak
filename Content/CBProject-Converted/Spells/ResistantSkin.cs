namespace Buffs
{
    public class ResistantSkin : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Resistant Skin",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
        };
        float lastTimeExecuted;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if (type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SHRED)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathTimer)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SweepTimer)) == 0)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PropelTimer)) == 0)
                        {
                            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer2)) == 0)
                            {
                                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer)) == 0)
                                {
                                    float distance = DistanceBetweenObjects(attacker, owner);
                                    if (distance > 950)
                                    {
                                        returnValue = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SetFeared(owner, false);
            SetNearSight(owner, false);
            SetSilenced(owner, false);
            SetSleep(owner, false);
            SetStunned(owner, false);
            SetNetted(owner, false);
            SetFeared(owner, false);
            SetDisarmed(owner, false);
            SetTaunted(owner, false);
            SetCharmed(owner, false);
            SetFeared(owner, false);
            float gameTime = GetGameTime();
            float bonusHealth = gameTime * 2.083f;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            float bonusRegen = gameTime * 0.00625f;
            IncPermanentFlatHPRegenMod(owner, bonusRegen);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectWards, nameof(Buffs.SharedWardBuff), true))
            {
                MoveAway(unit, owner.Position3D, 1000, 50, 300, 300, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 300, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            }
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(60, ref lastTimeExecuted, false))
            {
                IncPermanentFlatHPPoolMod(owner, 125);
                IncPermanentFlatHPRegenMod(owner, 0.375f);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            foreach (Champion unit in GetChampions(teamID, default, true))
            {
                if (!IsDead(unit))
                {
                    AddBuff(unit, unit, new Buffs.ExaltedWithBaronNashor(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}