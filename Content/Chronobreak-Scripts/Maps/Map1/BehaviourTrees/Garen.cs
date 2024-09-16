using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;

namespace BehaviourTrees.Map1;

class Garen: BehaviourTree
{
    float TotalUnitStrength;
    IEnumerable<AttackableUnit> TargetCollection;
    AttackableUnit Self;
    bool ValueChanged;
    Vector3 SelfPosition;
    float CurrentClosestDistance;
    AttackableUnit CurrentClosestTarget;
    bool LostAggro;
    AttackableUnit AggroTarget;
    Vector3 AggroPosition;
    float DeaggroDistance;
    float AccumulatedDamage;
    float PrevHealth;
    float PrevTime;
    float StrengthRatioOverTime;
    bool AggressiveKillMode;
    bool LowThreatMode;
    int PotionsToBuy;
    bool TeleportHome;
    Vector3 AssistPosition;
    AttackableUnit PreviousTarget;

    public override void Update()
    {
        base.Update();
        GarenBehavior();
    }

    bool GarenBehavior()
    {
        return
        (
            GarenInit() &&
            (
                GarenAtBaseHealAndBuy() ||
                GarenLevelUp() ||
                GarenGameNotStarted() ||
                ReduceDamageTaken() ||
                GarenHighThreatManagement() ||
                GarenReturnToBase() ||
                GarenKillChampion() ||
                GarenLowThreatManagement() ||
                GarenHeal() ||
                GarenAttack() ||
                GarenPushLane()
            )
        );
    }
    
    bool GarenStrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
        return
        (
            SetVarFloat(out TotalUnitStrength, 1) &&
            ForEach(TargetCollection, Unit => (
                TestUnitIsVisible(Self, Unit) &&
                (
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == MINION_UNIT &&
                        AddFloat(out TotalUnitStrength, TotalUnitStrength, 20)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == HERO_UNIT &&
                        AddFloat(out TotalUnitStrength, TotalUnitStrength, 30)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == TURRET_UNIT &&
                        AddFloat(out TotalUnitStrength, TotalUnitStrength, 90)
                    )
                )
            ))
        );
    }
    
    bool GarenFindClosestTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return
        (
            SetVarBool(out ValueChanged, false) &&
            ForEach(TargetCollection, Attacker => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, SelfPosition) &&
                Distance < CurrentClosestDistance &&
                SetVarFloat(out CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out CurrentClosestTarget, Attacker) &&
                SetVarBool(out ValueChanged, true)
            ))
        );
    }
    
    bool GarenDeaggroChecker()
    {
        float Distance;
        return
        ((
            SetVarBool(out LostAggro, false) &&
            DistanceBetweenObjectAndPoint(out Distance, AggroTarget, AggroPosition) &&
            Distance > 800 &&
            Distance < 1200 &&
            SetVarBool(out LostAggro, true)
        ) || true);
    }
    
    bool GarenInit()
    {
        float CurrentTime;
        float TimeDiff;
        float EnemyStrength;
        float FriendStrength;
        float StrRatio;
        AttackableUnit Unit;
        UnitType UnitType;
        float CurrentHealth;
        float NewDamage;
        return
        (
            GetUnitAISelf(out Self) &&
            GetUnitPosition(out SelfPosition, Self) &&
            SetVarFloat(out DeaggroDistance, 1200) &&
            (
                (
                    TestUnitAIFirstTime() &&
                    SetVarFloat(out AccumulatedDamage, 0) &&
                    GetUnitCurrentHealth(out PrevHealth, Self) &&
                    GetGameTime(out PrevTime) &&
                    SetVarBool(out LostAggro, false) &&
                    SetVarFloat(out StrengthRatioOverTime, 1) &&
                    SetVarBool(out AggressiveKillMode, false) &&
                    SetVarBool(out LowThreatMode, false) &&
                    SetVarInt(out PotionsToBuy, 4) &&
                    SetVarBool(out TeleportHome, false)
                ) ||
                (
                    ((
                        GetGameTime(out CurrentTime) &&
                        SubtractFloat(out TimeDiff, CurrentTime, PrevTime) &&
                        (
                            TimeDiff > 1 ||
                            TimeDiff < 0
                        ) &&
                        (
                            MultiplyFloat(out AccumulatedDamage, AccumulatedDamage, 0.8f) &&
                            MultiplyFloat(out StrengthRatioOverTime, StrengthRatioOverTime, 0.8f)
                        ) &&
                        (
                            GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 1000, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                            GarenStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, TotalUnitStrength) &&
                            GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 900, AffectFriends|AffectHeroes|AffectMinions|AffectTurrets) &&
                            GarenStrengthEvaluator() &&
                            SetVarFloat(out FriendStrength, TotalUnitStrength) &&
                            DivideFloat(out StrRatio, EnemyStrength, FriendStrength) &&
                            AddFloat(out StrengthRatioOverTime, StrengthRatioOverTime, StrRatio) &&
                            GetUnitAIAttackers(out TargetCollection, Self) &&
                            (TargetCollection.Any(Unit => (
                                GetUnitType(out UnitType, Unit) &&
                                UnitType == TURRET_UNIT &&
                                AddFloat(out StrengthRatioOverTime, StrengthRatioOverTime, 8)
                            )) || true)
                        ) &&
                        GetGameTime(out PrevTime)
                    ) || true) &&
                    ((
                        GetUnitCurrentHealth(out CurrentHealth, Self) &&
                        SubtractFloat(out NewDamage, PrevHealth, CurrentHealth) &&
                        NewDamage > 0 &&
                        AddFloat(out AccumulatedDamage, AccumulatedDamage, NewDamage)
                    ) || true) &&
                    GetUnitCurrentHealth(out PrevHealth, Self)
                )
            )
        );
    }
    
    bool GarenAtBaseHealAndBuy()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        float temp;
        return
        (
            GetUnitAIBasePosition(out BaseLocation, Self) &&
            DistanceBetweenObjectAndPoint(out Distance, Self, BaseLocation) &&
            Distance <= 450 &&
            SetVarBool(out TeleportHome, false) &&
            (
                (
                    DebugAction("Start ----- Heal -----") &&
                    GetUnitMaxHealth(out MaxHealth, Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f &&
                    DebugAction("Success ----- Heal -----")
                ) ||
                (
                    (
                        !TestChampionHasItem(Self, 1054) &&
                        TestUnitAICanBuyItem(1054) &&
                        UnitAIBuyItem(1054)
                    ) ||
                    (
                        !TestChampionHasItem(Self, 1001) &&
                        !TestChampionHasItem(Self, 3009) &&
                        !TestChampionHasItem(Self, 3117) &&
                        !TestChampionHasItem(Self, 3020) &&
                        !TestChampionHasItem(Self, 3006) &&
                        TestUnitAICanBuyItem(3111) &&
                        UnitAIBuyItem(1001)
                    ) ||
                    (
                        !TestChampionHasItem(Self, 3105) &&
                        (
                            (
                                !TestChampionHasItem(Self, 1028) &&
                                TestUnitAICanBuyItem(1028) &&
                                UnitAIBuyItem(1028)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 1029) &&
                                TestUnitAICanBuyItem(1029) &&
                                UnitAIBuyItem(1029)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 1033) &&
                                TestUnitAICanBuyItem(1033) &&
                                UnitAIBuyItem(1033)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 3105) &&
                                TestUnitAICanBuyItem(3105) &&
                                UnitAIBuyItem(3105)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(Self, 3105) &&
                        TestChampionHasItem(Self, 1001) &&
                        !TestChampionHasItem(Self, 3009) &&
                        TestUnitAICanBuyItem(3009) &&
                        UnitAIBuyItem(3009)
                    ) ||
                    (
                        TestChampionHasItem(Self, 3105) &&
                        TestChampionHasItem(Self, 3009) &&
                        !TestChampionHasItem(Self, 3068) &&
                        (
                            (
                                !TestChampionHasItem(Self, 1011) &&
                                TestUnitAICanBuyItem(1011) &&
                                UnitAIBuyItem(1011)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 1031) &&
                                TestUnitAICanBuyItem(1031) &&
                                UnitAIBuyItem(1031)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 3068) &&
                                TestUnitAICanBuyItem(3068) &&
                                UnitAIBuyItem(3068)
                            )
                        )
                    ) ||
                    (
                        GetUnitGold(out temp, Self) &&
                        temp > 0 &&
                        TestChampionHasItem(Self, 3105) &&
                        TestChampionHasItem(Self, 3009) &&
                        TestChampionHasItem(Self, 3068) &&
                        !TestChampionHasItem(Self, 3026) &&
                        (
                            (
                                !TestChampionHasItem(Self, 1029) &&
                                TestUnitAICanBuyItem(1029) &&
                                UnitAIBuyItem(1029)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 1033) &&
                                TestUnitAICanBuyItem(1033) &&
                                UnitAIBuyItem(1033)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 1031) &&
                                TestUnitAICanBuyItem(1031) &&
                                UnitAIBuyItem(1031)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 3026) &&
                                TestUnitAICanBuyItem(3026) &&
                                UnitAIBuyItem(3026)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(Self, 3105) &&
                        TestChampionHasItem(Self, 3009) &&
                        TestChampionHasItem(Self, 3068) &&
                        TestChampionHasItem(Self, 3026) &&
                        !TestChampionHasItem(Self, 3142) &&
                        (
                            (
                                !TestChampionHasItem(Self, 1036) &&
                                !TestChampionHasItem(Self, 3134) &&
                                !TestChampionHasItem(Self, 3142) &&
                                TestUnitAICanBuyItem(1036) &&
                                UnitAIBuyItem(1036)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 3134) &&
                                !TestChampionHasItem(Self, 3142) &&
                                TestUnitAICanBuyItem(3134) &&
                                UnitAIBuyItem(3134)
                            ) ||
                            (
                                !TestChampionHasItem(Self, 3142) &&
                                TestUnitAICanBuyItem(3142) &&
                                UnitAIBuyItem(3142)
                            )
                        )
                    ) ||
                    (
                        PotionsToBuy > 0 &&
                        !TestChampionHasItem(Self, 2003) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out PotionsToBuy, PotionsToBuy, 1)
                    )
                )
            ) &&
            DebugAction("++++ At Base Heal & Buy +++")
        );
    }
    
    bool GarenLevelUp()
    {
        int SkillPoints;
        int Ability0Level;
        int Ability1Level;
        int Ability2Level;
        return
        (
            GetUnitSkillPoints(out SkillPoints, Self) &&
            SkillPoints > 0 &&
            GetUnitSpellLevel(out Ability0Level, Self, SPELLBOOK_UNKNOWN, 0) &&
            GetUnitSpellLevel(out Ability1Level, Self, SPELLBOOK_UNKNOWN, 1) &&
            GetUnitSpellLevel(out Ability2Level, Self, SPELLBOOK_UNKNOWN, 2) &&
            (
                (
                    TestUnitCanLevelUpSpell(Self, 3) &&
                    LevelUpUnitSpell(Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("levelup 3")
                ) ||
                (
                    TestUnitCanLevelUpSpell(Self, 1) &&
                    (
                        (
                            Ability0Level >= 1 &&
                            Ability2Level >= 1 &&
                            Ability1Level <= 0
                        ) ||
                        (
                            Ability0Level >= 3 &&
                            Ability2Level >= 3 &&
                            Ability1Level <= 1
                        )
                    ) &&
                    LevelUpUnitSpell(Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 0")
                ) ||
                (
                    (
                        TestUnitCanLevelUpSpell(Self, 2) &&
                        Ability2Level <= Ability0Level &&
                        LevelUpUnitSpell(Self, SPELLBOOK_CHAMPION, 2) &&
                        DebugAction("levelup 0")
                    ) ||
                    (
                        TestUnitCanLevelUpSpell(Self, 0) &&
                        LevelUpUnitSpell(Self, SPELLBOOK_CHAMPION, 0) &&
                        DebugAction("levelup 0")
                    )
                ) ||
                (
                    TestUnitCanLevelUpSpell(Self, 1) &&
                    LevelUpUnitSpell(Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 0")
                )
            ) &&
            DebugAction("++++ Level up ++++")
        );
    }
    
    bool GarenGameNotStarted()
    {
        return
        (
            !TestGameStarted() &&
            DebugAction("+++ Game Not Started +++")
        );
    }
    
    bool GarenAttack()
    {
        return
        (
            GarenAcquireTarget() &&
            GarenAttackTarget() &&
            DebugAction("++++ Attack ++++")
        );
    }
    
    bool GarenAcquireTarget()
    {
        IEnumerable<AttackableUnit> FriendlyUnits;
        AttackableUnit unit;
        int Count;
        float Distance;
        return
        (
            (
                SetVarBool(out LostAggro, false) &&
                TestUnitAIAttackTargetValid() &&
                GetUnitAIAttackTarget(out AggroTarget) &&
                SetVarVector(out AggroPosition, AssistPosition) &&
                TestUnitIsVisible(Self, AggroTarget) &&
                GarenDeaggroChecker() &&
                LostAggro == false &&
                DebugAction("+++ Use Previous Target +++")
            ) ||
            (
                DebugAction("EnableOrDisableAllyAggro") &&
                SetVarFloat(out CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, Self, SelfPosition, 800, AffectFriends|AffectHeroes|AlwaysSelf) &&
                SetVarBool(out ValueChanged, false) &&
                ForEach(FriendlyUnits, unit => (
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out TargetCollection, unit) &&
                    GarenFindClosestVisibleTarget() &&
                    ValueChanged == true &&
                    SetUnitAIAssistTarget(Self) &&
                    SetUnitAIAttackTarget(CurrentClosestTarget) &&
                    unit == Self &&
                    SetVarVector(out AssistPosition, SelfPosition)
                )) &&
                ValueChanged == true &&
                DebugAction("+++ Acquired Ally under attack +++")
            ) ||
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 900, AffectBuildings|AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out ValueChanged, false) &&
                    ForEach(TargetCollection, unit => (
                        DistanceBetweenObjectAndPoint(out Distance, unit, SelfPosition) &&
                        Distance < CurrentClosestDistance &&
                        TestUnitIsVisible(Self, unit) &&
                        (
                            (
                                LostAggro == true &&
                                GetUnitAIAttackTarget(out AggroTarget) &&
                                AggroTarget != unit
                            ) ||
                            LostAggro == false
                        ) &&
                        SetVarFloat(out CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out CurrentClosestTarget, unit) &&
                        SetVarBool(out ValueChanged, true)
                    ))
                ) &&
                ValueChanged == true &&
                SetUnitAIAssistTarget(Self) &&
                SetUnitAIAttackTarget(CurrentClosestTarget) &&
                SetVarVector(out AssistPosition, SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        );
    }
    
    bool GarenAttackTarget()
    {
        AttackableUnit Target;
        TeamId SelfTeam;
        TeamId TargetTeam;
        UnitType UnitType;
        float currentHealth;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            GetUnitAIAttackTarget(out Target) &&
            GetUnitTeam(out SelfTeam, Self) &&
            GetUnitTeam(out TargetTeam, Target) &&
            SelfTeam != TargetTeam &&
            (
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == MINION_UNIT &&
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    HP_Ratio < 0.2f &&
                    (
                        (
                            StrengthRatioOverTime > 2 &&
                            GarenCanCastAbility2() &&
                            SetUnitAIAttackTarget(Self) &&
                            GarenCastAbility2()
                        ) ||
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(Self) &&
                            CastUnitSpell(Self, SPELLBOOK_CHAMPION, 0)
                        )
                    )
                ) ||
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == HERO_UNIT &&
                    (
                        (
                            GarenCanCastAbility0() &&
                            SetUnitAIAttackTarget(Self) &&
                            CastUnitSpell(Self, SPELLBOOK_CHAMPION, 0)
                        ) ||
                        (
                            GarenCanCastAbility2() &&
                            GarenCastAbility2()
                        )
                    )
                ) ||
                GarenAutoAttackTarget()
            ) &&
            DebugAction("++ Attack Success ++")
        );
    }
    
    bool GarenReturnToBase()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        Vector3 TeleportPosition;
        float DistanceToTeleportPosition;
        return
        (
            GetUnitAIBasePosition(out BaseLocation, Self) &&
            DistanceBetweenObjectAndPoint(out Distance, Self, BaseLocation) &&
            Distance > 300 &&
            (
                (
                    GetUnitMaxHealth(out MaxHealth, Self) &&
                    GetUnitCurrentHealth(out Health, Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    (
                        (
                            TeleportHome == true &&
                            Health_Ratio <= 0.35f
                        ) ||
                        (
                            TeleportHome == false &&
                            Health_Ratio <= 0.25f &&
                            SetVarBool(out TeleportHome, true)
                        )
                    )
                ) ||
                (
                    !DebugAction("EmptyNode: HighGold")
                )
            ) &&
            (
                (
                    SetVarFloat(out CurrentClosestDistance, 30000) &&
                    GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 30000, AffectFriends|AffectTurrets) &&
                    GarenFindClosestTarget() &&
                    ValueChanged == true &&
                    (
                        (
                            GetDistanceBetweenUnits(out Distance, CurrentClosestTarget, Self) &&
                            Distance < 125 &&
                            (
                                (
                                    TestUnitAISpellPositionValid() &&
                                    GetUnitAISpellPosition(out TeleportPosition) &&
                                    DistanceBetweenObjectAndPoint(out DistanceToTeleportPosition, Self, TeleportPosition) &&
                                    DistanceToTeleportPosition < 50
                                ) ||
                                !TestUnitAISpellPositionValid()
                            ) &&
                            IssueTeleportToBaseOrder() &&
                            ClearUnitAISpellPosition() &&
                            DebugAction("Yo")
                        ) ||
                        (
                            (
                                (
                                    !TestUnitAISpellPositionValid() &&
                                    ComputeUnitAISpellPosition(CurrentClosestTarget, Self, 150, false)
                                ) ||
                                TestUnitAISpellPositionValid()
                            ) &&
                            GetUnitAISpellPosition(out TeleportPosition) &&
                            IssueMoveToPositionOrder(TeleportPosition) &&
                            DebugAction("Yo")
                        )
                    )
                ) ||
                (
                    GetUnitAIBasePosition(out BaseLocation, Self) &&
                    IssueMoveToPositionOrder(BaseLocation)
                )
            ) &&
            DebugAction("+++ Teleport Home +++")
        );
    }
    
    bool GarenHighThreatManagement()
    {
        bool SuperHighThreat;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        return
        (
            (
                (
                    SetVarBool(out SuperHighThreat, false) &&
                    TestUnitUnderAttack(Self) &&
                    GetUnitMaxHealth(out MaxHealth, Self) &&
                    GetUnitCurrentHealth(out Health, Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f &&
                    DebugAction("+++ LowHealthUnderAttack +++") &&
                    SetVarBool(out SuperHighThreat, true)
                ) ||
                (
                    GetUnitMaxHealth(out MaxHealth, Self) &&
                    DivideFloat(out Damage_Ratio, AccumulatedDamage, MaxHealth) &&
                    (
                        (
                            AggressiveKillMode == true &&
                            Damage_Ratio > 0.15f
                        ) ||
                        (
                            AggressiveKillMode == false &&
                            Damage_Ratio > 0.02f
                        )
                    ) &&
                    DebugAction("+++ BurstDamage +++")
                )
            ) &&
            DebugAction("+++ High Threat +++") &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    SuperHighThreat == true &&
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(Self) &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    SuperHighThreat == true &&
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(Self) &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                GarenMicroRetreat()
            ) &&
            DebugAction("+++ High Threat +++")
        );
    }
    
    bool GarenLowThreatManagement()
    {
        return
        (
            (
                (
                    StrengthRatioOverTime > 6 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out LowThreatMode, true)
                ) ||
                (
                    LowThreatMode == true &&
                    SetVarBool(out LowThreatMode, false) &&
                    StrengthRatioOverTime > 4 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out LowThreatMode, true)
                ) ||
                (
                    ClearUnitAISafePosition() &&
                    !DebugAction("DoNotRemoveForcedFail")
                )
            ) &&
            GarenMicroRetreat() &&
            DebugAction("++++ Low Threat +++")
        );
    }
    
    bool GarenKillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        bool Aggressive;
        float MyHealthRatio;
        return
        (
            SetVarBool(out AggressiveKillMode, false) &&
            (
                (
                    StrengthRatioOverTime < 3 &&
                    GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 900, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out ValueChanged, false) &&
                    ForEach(TargetCollection, unit => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        TestUnitIsVisible(Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out CurrentClosestTarget, unit) &&
                        SetVarBool(out ValueChanged, true)
                    )) &&
                    ValueChanged == true &&
                    SetUnitAIAssistTarget(Self) &&
                    SetUnitAIAttackTarget(CurrentClosestTarget) &&
                    SetVarVector(out AssistPosition, SelfPosition) &&
                    SetVarBool(out Aggressive, false) &&
                    DebugAction("PassiveKillChampion")
                ) ||
                (
                    StrengthRatioOverTime < 5.1f &&
                    GetUnitMaxHealth(out MaxHealth, Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 1000, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.4f) &&
                    SetVarBool(out ValueChanged, false) &&
                    ForEach(TargetCollection, unit => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        TestUnitIsVisible(Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out CurrentClosestTarget, unit) &&
                        SetVarBool(out ValueChanged, true)
                    )) &&
                    ValueChanged == true &&
                    SetUnitAIAssistTarget(Self) &&
                    SetUnitAIAttackTarget(CurrentClosestTarget) &&
                    SetVarVector(out AssistPosition, SelfPosition) &&
                    SetVarBool(out Aggressive, true) &&
                    SetVarBool(out AggressiveKillMode, true) &&
                    DebugAction("+++ AggressiveMode +++")
                )
            ) &&
            (
                (
                    GarenCanCastAbility0() &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    Aggressive == true &&
                    GarenCanCastAbility3() &&
                    GarenCastAbility3() &&
                    DebugAction("+++ Use Ultiamte +++")
                ) ||
                (
                    !TestUnitHasBuff(Self, null, "GarenBladestorm") &&
                    GarenCanCastAbility2() &&
                    GarenCastAbility2()
                ) ||
                GarenAutoAttackTarget() ||
                DebugAction("+++ Attack Champion+++")
            ) &&
            DebugAction("++++ Success: Kill  +++")
        );
    }
    
    bool GarenLastHitMinion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        AttackableUnit Target;
        return
        (
            (
                GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 800, AffectEnemies|AffectMinions) &&
                SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
                SetVarBool(out ValueChanged, false) &&
                ForEach(TargetCollection, unit => (
                    GetUnitCurrentHealth(out CurrentHealth, unit) &&
                    GetUnitMaxHealth(out MaxHealth, unit) &&
                    DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                    HP_Ratio < CurrentLowestHealthRatio &&
                    SetVarBool(out ValueChanged, true) &&
                    SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                    SetVarAttackableUnit(out CurrentClosestTarget, unit)
                )) &&
                ValueChanged == true &&
                SetUnitAIAssistTarget(Self) &&
                SetUnitAIAttackTarget(CurrentClosestTarget) &&
                SetVarVector(out AssistPosition, SelfPosition) &&
                SetVarAttackableUnit(out Target, CurrentClosestTarget)
            ) &&
            GarenAutoAttackTarget() &&
            DebugAction("+++++++ Last Hit ++++++++")
        );
    }
    
    bool GarenMicroRetreat()
    {
        Vector3 SafePosition;
        float Distance;
        return
        (
            (
                TestUnitAISafePositionValid() &&
                GetUnitAISafePosition(out SafePosition) &&
                (
                    (
                        DistanceBetweenObjectAndPoint(out Distance, Self, SafePosition) &&
                        Distance < 50 &&
                        ComputeUnitAISafePosition(800, false, false) &&
                        DebugAction("------- At location computed new position --------------")
                    ) ||
                    (
                        IssueMoveToPositionOrder(SafePosition) &&
                        DebugAction("------------ Success: Move to safe position ----------")
                    )
                )
            ) ||
            ComputeUnitAISafePosition(600, false, false)
        );
    }
    
    bool GarenAutoAttackTarget()
    {
        AttackableUnit Target;
        float Distance;
        float AttackRange;
        return
        (
            GetUnitAIAttackTarget(out Target) &&
            TestUnitAIAttackTargetValid() &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, Self) &&
                    GetUnitAttackRange(out AttackRange, Self) &&
                    MultiplyFloat(out AttackRange, AttackRange, 0.9f) &&
                    Distance <= AttackRange &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(Target) &&
                    IssueAttackOrder()
                ) ||
                IssueMoveToUnitOrder(Target)
            )
        );
    }
    
    bool GarenCanCastAbility0()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(Self, SPELLBOOK_CHAMPION, 0)
        );
    }
    
    bool GarenCanCastAbility1()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&
            TestCanCastSpell(Self, SPELLBOOK_CHAMPION, 1)
        );
    }
    
    bool GarenCanCastAbility2()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&
            TestCanCastSpell(Self, SPELLBOOK_CHAMPION, 2)
        );
    }
    
    bool GarenCanCastAbility3()
    {
        float Cooldown;
        return
        (
            GetSpellSlotCooldown(out Cooldown, Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(Self, SPELLBOOK_CHAMPION, 3)
        );
    }
    
    bool GarenCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        );
    }
    
    bool GarenCastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, Self, SPELLBOOK_CHAMPION, 1) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        );
    }
    
    bool GarenCastAbility2()
    {
        AttackableUnit Target;
        float Range;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    TestUnitHasBuff(Self, null, "GarenBladestorm") &&
                    IssueMoveToUnitOrder(Target)
                ) ||
                (
                    GetUnitSpellCastRange(out Range, Self, SPELLBOOK_CHAMPION, 2) &&
                    SetVarFloat(out Range, 200) &&
                    (
                        (
                            DebugAction("Pareparing to cast ability 2") &&
                            GetDistanceBetweenUnits(out Distance, Target, Self) &&
                            DebugAction("GoingToRangeCheck") &&
                            Distance <= Range &&
                            DebugAction("Range Check Succses") &&
                            CastUnitSpell(Self, SPELLBOOK_CHAMPION, 2) &&
                            DebugAction("Ability 2 Success ----------------")
                        ) ||
                        (
                            DebugAction("MoveIntoRangeSequence------------------") &&
                            IssueMoveToUnitOrder(Target) &&
                            DebugAction("Moving To Cast")
                        )
                    )
                )
            )
        );
    }
    
    bool GarenCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, Self, SPELLBOOK_CHAMPION, 3) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        );
    }
    
    bool GarenPushLane()
    {
        return
        (
            ClearUnitAIAttackTarget() &&
            IssueMoveOrder() &&
            DebugAction("+++ Move To Lane +++")
        );
    }
    
    bool GarenMisc()
    {
        TeamId SelfTeam;
        TeamId UnitTeam;
        AttackableUnit Assist;
        float Distance;
        Vector3 AssistPosition;
        int Count;
        AttackableUnit Attacker;
        return
        (
            (
                !DebugAction("??? EnableOrDisablePreviousTarget ???") &&
                TestUnitAIAttackTargetValid() &&
                SetVarBool(out LostAggro, false) &&
                GetUnitAIAttackTarget(out PreviousTarget) &&
                GetUnitTeam(out SelfTeam, Self) &&
                GetUnitTeam(out UnitTeam, PreviousTarget) &&
                UnitTeam != SelfTeam &&
                GetUnitAIAssistTarget(out Assist) &&
                (
                    (
                        Assist == Self &&
                        DistanceBetweenObjectAndPoint(out Distance, Self, this.AssistPosition) &&
                        ((
                            Distance >= DeaggroDistance &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out LostAggro, true) &&
                            DebugAction("+++ Lost Aggro +++")
                        ) || true) &&
                        Distance < DeaggroDistance &&
                        DebugAction("+++ In Aggro Range, Use Previous")
                    ) ||
                    (
                        Self != Assist &&
                        GetUnitPosition(out AssistPosition, Assist) &&
                        DistanceBetweenObjectAndPoint(out Distance, PreviousTarget, SelfPosition) &&
                        ((
                            Distance >= 1000 &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out LostAggro, true) &&
                            DebugAction("------- Losing aggro from assist ----------")
                        ) || true) &&
                        Distance < 1000 &&
                        DebugAction("============= Use Previous Target: Still close to assist -----------")
                    )
                ) &&
                SetVarBool(out LostAggro, false) &&
                DebugAction("++ Use Previous Target ++")
            ) &&
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out TargetCollection, Self, SelfPosition, 900, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out ValueChanged, false) &&
                    ForEach(TargetCollection, Attacker => (
                        (
                            (
                                LostAggro == true &&
                                Attacker != PreviousTarget
                            ) ||
                            LostAggro == false
                        ) &&
                        DistanceBetweenObjectAndPoint(out Distance, Attacker, SelfPosition) &&
                        Distance < CurrentClosestDistance &&
                        SetVarFloat(out CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out CurrentClosestTarget, Attacker) &&
                        SetVarBool(out ValueChanged, true)
                    ))
                ) &&
                ValueChanged == true &&
                SetUnitAIAssistTarget(Self) &&
                SetUnitAIAttackTarget(CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        );
    }
    
    bool GarenHeal()
    {
        float Health;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            GetUnitCurrentHealth(out Health, Self) &&
            GetUnitMaxHealth(out MaxHealth, Self) &&
            DivideFloat(out HP_Ratio, Health, MaxHealth) &&
            (
                HP_Ratio < 0.5f &&
                TestUnitAICanUseItem(2003) &&
                IssueUseItemOrder(2003)
            )
        );
    }
    
    bool ReduceDamageTaken()
    {
        float MaxHealth;
        float Damage_Ratio;
        return
        (
            GetUnitMaxHealth(out MaxHealth, Self) &&
            DivideFloat(out Damage_Ratio, AccumulatedDamage, MaxHealth) &&
            Damage_Ratio >= 0.1f &&
            (
                (
                    GarenCanCastAbility1() &&
                    SetUnitAIAttackTarget(Self) &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    GarenCanCastAbility0() &&
                    SetUnitAIAttackTarget(Self) &&
                    CastUnitSpell(Self, SPELLBOOK_CHAMPION, 0)
                )
            )
        );
    }
    
    bool GarenFindClosestVisibleTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return
        (
            SetVarBool(out ValueChanged, false) &&
            ForEach(TargetCollection, Attacker => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, SelfPosition) &&
                Distance < CurrentClosestDistance &&
                TestUnitIsVisible(Self, Attacker) &&
                SetVarFloat(out CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out CurrentClosestTarget, Attacker) &&
                SetVarBool(out ValueChanged, true)
            ))
        );
    }
}