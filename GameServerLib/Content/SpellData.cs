using System;
using System.Numerics;
//using System.Text.Json.Serialization;
using GameServerCore.Enums;

namespace Chronobreak.GameServer.Content
{
    public class SpellData
    {
        public string AfterEffectName { get; set; } = "";
        //AIEndOnly
        //AILifetime
        //AIRadius
        //AIRange
        //AISendEvent
        //AISpeed
        public string AlternateName { get; set; } = "";
        public bool AlwaysSnapFacing { get; set; }
        //AmmoCountHiddenInUI
        public float[] AmmoRechargeTime { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public int[] AmmoUsed { get; set; } = { 1, 1, 1, 1, 1, 1, 1 };
        public string AnimationLeadOutName { get; set; } = "";
        public string AnimationLoopName { get; set; } = "";
        public string AnimationName { get; set; } = "";
        public string AnimationWinddownName { get; set; } = "";
        //ApplyAttackDamage
        //ApplyAttackEffect
        //ApplyMaterialOnHitSound
        public bool BelongsToAvatar { get; set; }
        public float BounceRadius { get; set; } = 450;
        public bool CanCastWhileDisabled { get; set; }
        public float CancelChargeOnRecastTime { get; set; }
        public bool CanMoveWhileChanneling { get; set; }
        public bool CannotBeSuppressed { get; set; }
        public bool CanOnlyCastWhileDead { get; set; }
        public bool CanOnlyCastWhileDisabled { get; set; }
        public bool CantCancelWhileChanneling { get; set; }
        public bool CantCancelWhileWindingUp { get; set; }
        public bool CantCastWhileRooted { get; set; }
        public float CastConeAngle { get; set; } = 45;
        public float CastConeDistance { get; set; } = 400;
        public float CastFrame { get; set; }
        public float[] CastRadius { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public float[] CastRadiusSecondary { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        //CastRadiusSecondaryTexture
        //CastRadiusTexture
        public float[] CastRange { get; set; } = { 400, 400, 400, 400, 400, 400, 400 };
        public float CastRangeDisplayOverride { get; set; }
        public float[] CastRangeGrowthDuration { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public float[] CastRangeGrowthMax { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        //CastRangeTextureOverrideName
        public bool CastRangeUseBoundingBoxes { get; set; }
        public float CastTargetAdditionalUnitsRadius { get; set; }
        public CastType CastType { get; set; }
        public float[] ChannelDuration { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public float ChargeUpdateInterval { get; set; }
        public float CircleMissileAngularVelocity { get; set; }
        public float CircleMissileRadialVelocity { get; set; }
        public float Coefficient { get; set; }
        public float Coefficient2 { get; set; }
        //ClientOnlyMissileTargetBoneName
        public bool ConsideredAsAutoAttack { get; set; }
        public float[] Cooldown { get; set; } = { 10, 10, 10, 10, 10, 10, 10 };
        //CursorChangesInGrass
        //CursorChangesInTerrain
        public float DeathRecapPriority { get; set; }
        public float DelayCastOffsetPercent { get; set; }
        public float DelayTotalTimePercent { get; set; }
        //Description
        //DisableCastBar
        //DisplayName
        public bool DoesntBreakChannels { get; set; }
        public bool DoNotNeedToFaceTarget { get; set; }
        //DrawSecondaryLineIndicator
        //DynamicExtended
        //string DynamicTooltip
        public float[] Effect1Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect2Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect3Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect4Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect5Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect6Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect7Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public float[] Effect8Amount { get; set; } = { 0, 0, 0, 0, 0, 0 };
        public SpellDataFlags Flags { get; set; }
        //FloatStaticsDecimalsX
        //FloatVarsDecimalsX
        public bool HaveAfterEffect { get; set; }
        public bool HaveHitBone { get; set; }
        public bool HaveHitEffect { get; set; }
        public bool HavePointEffect { get; set; }
        //HideRangeIndicatorWhenCasting
        public string HitBoneName { get; set; } = "";
        public string HitEffectName { get; set; } = "";
        public int HitEffectOrientType { get; set; } = 1;
        //HitEffectPlayerName
        public bool IgnoreAnimContinueUntilCastFrame { get; set; }
        public bool IgnoreRangeCheck { get; set; }
        //InventoryIconX
        public bool IsDisabledWhileDead { get; set; } = true;
        public bool IsToggleSpell { get; set; }
        //KeywordWhenAcquired
        //LevelXDesc
        public float LineDragLength { get; set; }
        public int LineMissileBounces { get; set; }
        public bool LineMissileCollisionFromStartPoint { get; set; }
        public float LineMissileDelayDestroyAtEndSeconds { get; set; } // Always 0 in 4.20
        public bool LineMissileEndsAtTargetPoint { get; set; }
        public bool LineMissileFollowsTerrainHeight { get; set; }
        public float LineMissileTargetHeightAugment { get; set; } = 100;
        public float LineMissileTimePulseBetweenCollisionSpellHits { get; set; } // Always 0 in 4.20
        public bool LineMissileTrackUnits { get; set; }
        public bool LineMissileUsesAccelerationForBounce { get; set; }
        //LineTargetingBaseTextureOverrideName
        //LineTargetingBaseTextureOverrideName
        public float LineWidth { get; set; }
        public float[] LocationTargettingLength { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public float[] LocationTargettingWidth { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        public bool LockConeToPlayer { get; set; }
        //LookAtPolicy
        public float LuaOnMissileUpdateDistanceInterval { get; set; }
        public float[] ManaCost { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        //Map_X_EffectYLevelZAmmount
        public int MaxAmmo { get; set; } = 1;
        //MaxGrowthRangeTextureName
        //MinimapIcon
        //MinimapIconDisplayFlag
        //MinimapIconRotation
        public float MissileAccel { get; set; }
        public string MissileBoneName { get; set; } = "";
        public string MissileEffect { get; set; } = "";
        public string MissileEffectPlayer { get; set; } = "";
        public float MissileFixedTravelTime { get; set; }
        public bool MissileFollowsTerrainHeight { get; set; }
        public float MissileGravity { get; set; }
        public float MissileLifetime { get; set; }
        public float MissileMaxSpeed { get; set; }
        public float MissileMinSpeed { get; set; }
        public float MissileMinTravelTime { get; set; }
        public float MissilePerceptionBubbleRadius { get; set; }
        public bool MissilePerceptionBubbleRevealsStealth { get; set; } // Always 0 in 4.20
        public float MissileSpeed { get; set; } = 500;
        public float MissileTargetHeightAugment { get; set; } = 100;
        public bool MissileUnblockable { get; set; }
        public bool NoWinddownIfCancelled { get; set; }
        //NumSpellTargeters
        //OrientRadiusTextureFromPlayer
        //OrientRangeIndicatorToCursor
        //OrientRangeIndicatorToFacing
        public float OverrideCastTime { get; set; }
        public Vector3 ParticleStartOffset { get; set; } = new(0, 0, 0);
        //PlatformEnabled
        public string PointEffectName { get; set; } = "";
        //RangeIndicatorTextureName
        public string RequiredUnitTags { get; set; } = "";
        public string SelectionPreference { get; set; } = "";
        //Sound_CastName
        //Sound_HitName
        //Sound_VOEventCategory
        public float SpellCastTime { get; set; }
        public float SpellRevealsChampion { get; set; }
        public float SpellTotalTime { get; set; }
        public float StartCooldown { get; set; }
        public bool SubjectToGlobalCooldown { get; set; } = true;
        //TargeterConstrainedToRange
        public TargetingType TargetingType { get; set; } = TargetingType.Target;
        public string TextFlags { get; set; } = "";
        public bool TriggersGlobalCooldown { get; set; } = true;
        public bool UpdateRotationWhenCasting { get; set; } = true;
        public bool UseAnimatorFramerate { get; set; }
        public bool UseAutoattackCastTime { get; set; }
        public bool UseChargeChanneling { get; set; }
        public bool UseChargeTargeting { get; set; }
        public bool UseGlobalLineIndicator { get; set; }
        public bool UseMinimapTargeting { get; set; }
        //Version
        //x1,x2,x3,x4,x5

        public float GetCastTime()
        {
            return (1.0f + DelayCastOffsetPercent) * 0.5f;
        }

        // TODO: Implement this (where it is verified to be needed)
        public float GetCastTimeTotal()
        {
            return (1.0f + DelayTotalTimePercent) * 2.0f;
        }

        // TODO: read Global Character Data constants from constants.var (gcd_AttackDelay = 1.600f, gcd_AttackDelayCastPercent = 0.300f)
        public float GetCharacterAttackDelay
        (
            float attackSpeedMod,
            float attackDelayOffsetPercent,
            float attackMinimumDelay = 0.4f,
            float attackMaximumDelay = 5.0f
        )
        {
            float result = ((attackDelayOffsetPercent + 1.0f) * 1.600f) / attackSpeedMod;
            return Math.Clamp(result, attackMinimumDelay, attackMaximumDelay);
        }

        public float GetCharacterAttackCastDelay
        (
            float attackSpeedMod,
            float attackDelayOffsetPercent,
            float attackDelayCastOffsetPercent,
            float attackDelayCastOffsetPercentAttackSpeedRatio,
            float attackMinimumDelay = 0.4f,
            float attackMaximumDelay = 5.0f
        )
        {
            float castPercent = Math.Min(0.300f + attackDelayCastOffsetPercent, 0.0f);
            float percentDelay = GetCharacterAttackDelay(1.0f, attackDelayOffsetPercent, attackMinimumDelay, attackMaximumDelay) * castPercent;
            float attackDelay = GetCharacterAttackDelay(attackSpeedMod, attackDelayCastOffsetPercent, attackMinimumDelay, attackMaximumDelay);
            float result = (((attackDelay * castPercent) - percentDelay) * attackDelayCastOffsetPercentAttackSpeedRatio) + percentDelay;
            return Math.Min(result, attackDelay);
        }

        public SpellData()
        {
        }

        public SpellData(INIContentFile file)
        {
            string name = file.Name;

            AfterEffectName = file.GetValue("SpellData", "AfterEffectName", AfterEffectName);
            //AIEndOnly
            //AILifetime
            //AIRadius
            //AIRange
            //AISendEvent
            //AISpeed
            AlternateName = file.GetValue("SpellData", "AlternateName", name);
            AlwaysSnapFacing = file.GetValue("SpellData", "AlwaysSnapFacing", AlwaysSnapFacing);
            //AmmoCountHiddenInUI
            float lastValidTime = 0;
            for (var i = 1; i <= 6 + 1; i++)
            {
                float time = file.GetValue("SpellData", $"AmmoRechargeTime{i}", 0f);

                if (time > 0)
                {
                    AmmoRechargeTime[i - 1] = time;
                    lastValidTime = time;
                }
                else
                {
                    AmmoRechargeTime[i - 1] = lastValidTime;
                }
            }
            AmmoUsed = file.GetMultiInt("SpellData", "AmmoUsed", 6, AmmoUsed[0]);
            AnimationLeadOutName = file.GetValue("SpellData", "AnimationLeadOutName", name);
            AnimationLoopName = file.GetValue("SpellData", "AnimationLoopName", name);
            AnimationName = file.GetValue("SpellData", "AnimationName", name);
            AnimationWinddownName = file.GetValue("SpellData", "AnimationWinddownName", name);
            Coefficient = file.GetValue("SpellData", "Coefficient", Coefficient);
            //ApplyAttackDamage
            //ApplyAttackEffect
            //ApplyMaterialOnHitSound
            BelongsToAvatar = file.GetValue("SpellData", "BelongsToAvatar", BelongsToAvatar);
            BounceRadius = file.GetValue("SpellData", "BounceRadius", BounceRadius);
            CanCastWhileDisabled = file.GetValue("SpellData", "CanCastWhileDisabled", CanCastWhileDisabled);
            CancelChargeOnRecastTime = file.GetValue("SpellData", "CancelChargeOnRecastTime", CancelChargeOnRecastTime);
            CanMoveWhileChanneling = file.GetValue("SpellData", "CanMoveWhileChanneling", CanMoveWhileChanneling);
            CannotBeSuppressed = file.GetValue("SpellData", "CannotBeSuppressed", CannotBeSuppressed);
            CanOnlyCastWhileDead = file.GetValue("SpellData", "CanOnlyCastWhileDead", CanOnlyCastWhileDead);
            CanOnlyCastWhileDisabled = file.GetValue("SpellData", "CanOnlyCastWhileDisabled", CanOnlyCastWhileDisabled);
            CantCancelWhileChanneling = file.GetValue("SpellData", "CantCancelWhileChanneling", CantCancelWhileChanneling);
            CantCancelWhileWindingUp = file.GetValue("SpellData", "CantCancelWhileWindingUp", CantCancelWhileWindingUp);
            CantCastWhileRooted = file.GetValue("SpellData", "CantCastWhileRooted", CantCastWhileRooted);
            CastConeAngle = file.GetValue("SpellData", "CastConeAngle", CastConeAngle);
            CastConeDistance = file.GetValue("SpellData", "CastConeDistance", CastConeDistance);
            CastFrame = file.GetValue("SpellData", "CastFrame", CastFrame);
            CastRadius = file.GetMultiFloat("SpellData", "CastRadius", 6, CastRadius[0]);
            CastRadiusSecondary = file.GetMultiFloat("SpellData", "CastRadiusSecondary", 6, CastRadiusSecondary[0]);
            //CastRadiusSecondaryTexture
            //CastRadiusTexture
            CastRange = file.GetMultiFloat("SpellData", "CastRange", 6, CastRange[0]);
            CastRangeDisplayOverride = file.GetValue("SpellData", "CastRangeDisplayOverride", CastRangeDisplayOverride);
            CastRangeGrowthDuration = file.GetMultiFloat("SpellData", "CastRangeGrowthDuration", 6, CastRangeGrowthDuration[0]);
            CastRangeGrowthMax = file.GetMultiFloat("SpellData", "CastRangeGrowthMax", 6, CastRangeGrowthMax[0]);
            //CastRangeTextureOverrideName
            CastRangeUseBoundingBoxes = file.GetValue("SpellData", "CastRangeUseBoundingBoxes", CastRangeUseBoundingBoxes);
            CastTargetAdditionalUnitsRadius = file.GetValue("SpellData", "CastTargetAdditionalUnitsRadius", CastTargetAdditionalUnitsRadius);
            CastType = (CastType)file.GetValue("SpellData", "CastType", (int)CastType);
            ChannelDuration = file.GetMultiFloat("SpellData", "ChannelDuration", 6, ChannelDuration[0]);
            ChargeUpdateInterval = file.GetValue("SpellData", "ChargeUpdateInterval", ChargeUpdateInterval);
            CircleMissileAngularVelocity = file.GetValue("SpellData", "CircleMissileAngularVelocity", CircleMissileAngularVelocity);
            CircleMissileRadialVelocity = file.GetValue("SpellData", "CircleMissileRadialVelocity", CircleMissileRadialVelocity);
            //ClientOnlyMissileTargetBoneName
            ConsideredAsAutoAttack = file.GetValue("SpellData", "ConsideredAsAutoAttack", ConsideredAsAutoAttack);
            Cooldown = file.GetMultiFloat("SpellData", "Cooldown", 6, Cooldown[0]);
            //CursorChangesInGrass
            //CursorChangesInTerrain
            DeathRecapPriority = file.GetValue("SpellData", "DeathRecapPriority", DeathRecapPriority);
            DelayCastOffsetPercent = file.GetValue("SpellData", "DelayCastOffsetPercent", DelayCastOffsetPercent);
            DelayTotalTimePercent = file.GetValue("SpellData", "DelayTotalTimePercent", DelayTotalTimePercent);
            //Description
            //DisableCastBar
            //DisplayName
            DoesntBreakChannels = file.GetValue("SpellData", "DoesntBreakChannels", DoesntBreakChannels);
            DoNotNeedToFaceTarget = file.GetValue("SpellData", "DoNotNeedToFaceTarget", DoNotNeedToFaceTarget);
            //DrawSecondaryLineIndicator
            //DynamicExtended
            //string DynamicTooltip
            //EffectXLevelYAmount
            Effect1Amount = file.GetMultiFloat("SpellData", "Effect1Level", 6, Effect1Amount[0], "Amount");
            Effect2Amount = file.GetMultiFloat("SpellData", "Effect2Level", 6, Effect2Amount[0], "Amount");
            Effect3Amount = file.GetMultiFloat("SpellData", "Effect3Level", 6, Effect3Amount[0], "Amount");
            Effect4Amount = file.GetMultiFloat("SpellData", "Effect4Level", 6, Effect4Amount[0], "Amount");
            Effect5Amount = file.GetMultiFloat("SpellData", "Effect5Level", 6, Effect5Amount[0], "Amount");
            Effect6Amount = file.GetMultiFloat("SpellData", "Effect6Level", 6, Effect6Amount[0], "Amount");
            Effect7Amount = file.GetMultiFloat("SpellData", "Effect7Level", 6, Effect7Amount[0], "Amount");
            Effect8Amount = file.GetMultiFloat("SpellData", "Effect8Level", 6, Effect8Amount[0], "Amount");
            Flags = (SpellDataFlags)file.GetValue("SpellData", "Flags", (int)Flags);
            //FloatStaticsDecimalsX
            //FloatVarsDecimalsX
            HaveAfterEffect = file.GetValue("SpellData", "HaveAfterEffect", HaveAfterEffect);
            HaveHitBone = file.GetValue("SpellData", "HaveHitBone", HaveHitBone);
            HaveHitEffect = file.GetValue("SpellData", "HaveHitEffect", HaveHitEffect);
            HavePointEffect = file.GetValue("SpellData", "HavePointEffect", HavePointEffect);
            //HideRangeIndicatorWhenCasting
            HitBoneName = file.GetValue("SpellData", "HitBoneName", HitBoneName);
            HitEffectName = file.GetValue("SpellData", "HitEffectName", HitEffectName);
            HitEffectOrientType = file.GetValue("SpellData", "HitEffectOrientType", HitEffectOrientType);
            //HitEffectPlayerName
            IgnoreAnimContinueUntilCastFrame = file.GetValue("SpellData", "IgnoreAnimContinueUntilCastFrame", IgnoreAnimContinueUntilCastFrame);
            IgnoreRangeCheck = file.GetValue("SpellData", "IgnoreRangeCheck", IgnoreRangeCheck);
            //InventoryIconX
            IsDisabledWhileDead = file.GetValue("SpellData", "IsDisabledWhileDead", IsDisabledWhileDead);
            IsToggleSpell = file.GetValue("SpellData", "IsToggleSpell", IsToggleSpell);
            //KeywordWhenAcquired
            //LevelXDesc
            LineDragLength = file.GetValue("SpellData", "LineDragLength", LineDragLength);
            LineMissileBounces = file.GetValue("SpellData", "LineMissileBounces", LineMissileBounces);
            LineMissileCollisionFromStartPoint = file.GetValue("SpellData", "LineMissileCollisionFromStartPoint", LineMissileCollisionFromStartPoint);
            LineMissileDelayDestroyAtEndSeconds = file.GetValue("SpellData", "LineMissileDelayDestroyAtEndSeconds", LineMissileDelayDestroyAtEndSeconds);
            LineMissileEndsAtTargetPoint = file.GetValue("SpellData", "LineMissileEndsAtTargetPoint", LineMissileEndsAtTargetPoint);
            LineMissileFollowsTerrainHeight = file.GetValue("SpellData", "LineMissileFollowsTerrainHeight", LineMissileFollowsTerrainHeight);
            LineMissileTargetHeightAugment = file.GetValue("SpellData", "LineMissileTargetHeightAugment", LineMissileTargetHeightAugment);
            LineMissileTimePulseBetweenCollisionSpellHits = file.GetValue("SpellData", "LineMissileTimePulseBetweenCollisionSpellHits", LineMissileTimePulseBetweenCollisionSpellHits);
            LineMissileTrackUnits = file.GetValue("SpellData", "LineMissileTrackUnits", LineMissileTrackUnits);
            LineMissileUsesAccelerationForBounce = file.GetValue("SpellData", "LineMissileUsesAccelerationForBounce", LineMissileUsesAccelerationForBounce);
            //LineTargetingBaseTextureOverrideName
            //LineTargetingBaseTextureOverrideName
            LineWidth = file.GetValue("SpellData", "LineWidth", LineWidth);
            LocationTargettingLength = file.GetMultiFloat("SpellData", "LocationTargettingLength", 6, LocationTargettingLength[0]);
            LocationTargettingWidth = file.GetMultiFloat("SpellData", "LocationTargettingWidth", 6, LocationTargettingWidth[0]);
            LockConeToPlayer = file.GetValue("SpellData", "LockConeToPlayer", LockConeToPlayer);
            //LookAtPolicy
            LuaOnMissileUpdateDistanceInterval = file.GetValue("SpellData", "LuaOnMissileUpdateDistanceInterval", LuaOnMissileUpdateDistanceInterval);
            Coefficient2 = file.GetValue("SpellData", "Coefficient2", Coefficient2);
            ManaCost = file.GetMultiFloat("SpellData", "ManaCost", 6, ManaCost[0]);
            //Map_X_EffectYLevelZAmmount
            MaxAmmo = file.GetValue("SpellData", "MaxAmmo", MaxAmmo);
            //MaxGrowthRangeTextureName
            //MinimapIcon
            //MinimapIconDisplayFlag
            //MinimapIconRotation
            MissileSpeed = file.GetValue("SpellData", "MissileSpeed", MissileSpeed);
            MissileAccel = file.GetValue("SpellData", "MissileAccel", MissileAccel);
            MissileBoneName = file.GetValue("SpellData", "MissileBoneName", MissileBoneName);
            MissileEffect = file.GetValue("SpellData", "MissileEffect", MissileEffect);
            MissileEffectPlayer = file.GetValue("SpellData", "MissileEffectPlayer", MissileEffectPlayer);
            MissileFixedTravelTime = file.GetValue("SpellData", "MissileFixedTravelTime", MissileFixedTravelTime);
            MissileFollowsTerrainHeight = file.GetValue("SpellData", "MissileFollowsTerrainHeight", MissileFollowsTerrainHeight);
            MissileGravity = file.GetValue("SpellData", "MissileGravity", MissileGravity);
            MissileLifetime = file.GetValue("SpellData", "MissileLifetime", MissileLifetime);
            MissileMaxSpeed = file.GetValue("SpellData", "MissileMaxSpeed", MissileSpeed);
            MissileMinSpeed = file.GetValue("SpellData", "MissileMinSpeed", MissileSpeed);
            MissileMinTravelTime = file.GetValue("SpellData", "MissileMinTravelTime", MissileMinTravelTime);
            MissilePerceptionBubbleRadius = file.GetValue("SpellData", "MissilePerceptionBubbleRadius", MissilePerceptionBubbleRadius);
            MissilePerceptionBubbleRevealsStealth = file.GetValue("SpellData", "MissilePerceptionBubbleRevealsStealth", MissilePerceptionBubbleRevealsStealth);
            //MissileSpeed = file.GetFloat("SpellData", "MissileSpeed", MissileSpeed);
            MissileTargetHeightAugment = file.GetValue("SpellData", "MissileTargetHeightAugment", MissileTargetHeightAugment);
            MissileUnblockable = file.GetValue("SpellData", "MissileUnblockable", MissileUnblockable);
            NoWinddownIfCancelled = file.GetValue("SpellData", "NoWinddownIfCancelled", NoWinddownIfCancelled);
            //NumSpellTargeters
            //OrientRadiusTextureFromPlayer
            //OrientRangeIndicatorToCursor
            //OrientRangeIndicatorToFacing
            OverrideCastTime = file.GetValue("SpellData", "OverrideCastTime", OverrideCastTime);
            //public Vector3 ParticleStartOffset { get; set; } = new Vector3(0, 0, 0);
            var particleStartOffset = file.GetFloatArray("SpellData", "ParticleStartOffset", new[] { ParticleStartOffset.X, ParticleStartOffset.Y, ParticleStartOffset.Z });
            ParticleStartOffset = new Vector3(particleStartOffset[0], particleStartOffset[1], particleStartOffset[2]);
            //PlatformEnabled
            PointEffectName = file.GetValue("SpellData", "PointEffectName", PointEffectName);
            //RangeIndicatorTextureName
            RequiredUnitTags = file.GetValue("SpellData", "RequiredUnitTags", RequiredUnitTags);
            SelectionPreference = file.GetValue("SpellData", "SelectionPreference", SelectionPreference);
            //Sound_CastName
            //Sound_HitName
            //Sound_VOEventCategory
            SpellCastTime = file.GetValue("SpellData", "SpellCastTime", SpellCastTime);
            SpellRevealsChampion = file.GetValue("SpellData", "SpellRevealsChampion", SpellRevealsChampion);
            SpellTotalTime = file.GetValue("SpellData", "SpellTotalTime", SpellTotalTime);
            StartCooldown = file.GetValue("SpellData", "StartCooldown", StartCooldown);
            SubjectToGlobalCooldown = file.GetValue("SpellData", "SubjectToGlobalCooldown", SubjectToGlobalCooldown);
            //TargeterConstrainedToRange
            TargetingType = (TargetingType)file.GetValue("SpellData", "TargettingType", (int)TargetingType);
            TextFlags = file.GetValue("SpellData", "TextFlags", TextFlags);
            TriggersGlobalCooldown = file.GetValue("SpellData", "TriggersGlobalCooldown", TriggersGlobalCooldown);
            UpdateRotationWhenCasting = file.GetValue("SpellData", "UpdateRotationWhenCasting", UpdateRotationWhenCasting);
            UseAnimatorFramerate = file.GetValue("SpellData", "UseAnimatorFramerate", UseAnimatorFramerate);
            UseAutoattackCastTime = file.GetValue("SpellData", "UseAutoattackCastTime", UseAutoattackCastTime);
            UseChargeChanneling = file.GetValue("SpellData", "UseChargeChanneling", UseChargeChanneling);
            UseChargeTargeting = file.GetValue("SpellData", "UseChargeTargeting", UseChargeTargeting);
            UseGlobalLineIndicator = file.GetValue("SpellData", "UseGlobalLineIndicator", UseGlobalLineIndicator);
            UseMinimapTargeting = file.GetValue("SpellData", "UseMinimapTargeting", UseMinimapTargeting);
        }
    }
}
