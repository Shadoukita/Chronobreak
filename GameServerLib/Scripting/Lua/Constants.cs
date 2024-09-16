using System;
using MoonSharp.Interpreter;
using GameServerCore.Enums;

namespace Chronobreak.GameServer.Scripting.Lua
{
    public static class Constants
    {
        public static void RegisterConstants(Table globals)
        {
            globals["True"] = true;
            globals["False"] = false;

            globals["AI_ORDER_NONE"] = OrderType.OrderNone;
            globals["AI_HOLD"] = OrderType.Hold;
            globals["AI_MOVETO"] = OrderType.MoveTo;
            globals["AI_ATTACKTO"] = OrderType.AttackTo;

            globals["CANCEL_ORDER"] = ForceMovementOrdersType.CANCEL_ORDER;
            globals["POSTPONE_CURRENT_ORDER"] = ForceMovementOrdersType.POSTPONE_CURRENT_ORDER;

            globals["NoTargetYet"] = null; //?
            globals["NoValidTarget"] = null; //?

            globals["BUFF_Internal"] = BuffType.INTERNAL;
            globals["BUFF_Aura"] = BuffType.AURA;
            globals["BUFF_CombatEnchancer"] = BuffType.COMBAT_ENCHANCER;
            globals["BUFF_CombatDehancer"] = BuffType.COMBAT_DEHANCER;
            //...
            globals["BUFF_Stun"] = BuffType.STUN;
            globals["BUFF_Invisibility"] = BuffType.INVISIBILITY;
            globals["BUFF_Silence"] = BuffType.SILENCE;
            globals["BUFF_Taunt"] = BuffType.TAUNT;
            globals["BUFF_Polymorph"] = BuffType.POLYMORPH;
            globals["BUFF_Slow"] = BuffType.SLOW;
            globals["BUFF_Snare"] = BuffType.SNARE;
            globals["BUFF_Damage"] = BuffType.DAMAGE;
            globals["BUFF_Heal"] = BuffType.HEAL;
            globals["BUFF_Haste"] = BuffType.HASTE;
            globals["BUFF_SpellImmunity"] = BuffType.SPELL_IMMUNITY;
            globals["BUFF_PhysicalImmunity"] = BuffType.PHYSICAL_IMMUNITY;
            globals["BUFF_Invulnerability"] = BuffType.INVULNERABILITY;
            globals["BUFF_Sleep"] = BuffType.SLEEP;
            //...
            globals["BUFF_Fear"] = BuffType.FEAR;
            //...
            globals["BUFF_Poison"] = BuffType.POISON;
            globals["BUFF_Suppression"] = BuffType.SUPPRESSION;
            globals["BUFF_Blind"] = BuffType.BLIND;
            //...
            globals["BUFF_Shred"] = BuffType.SHRED;
            //...
            globals["BUFF_AmmoStack"] = BuffType.COUNTER; //?
            globals["BUFF_Net"] = BuffType.CHARM; //?

            globals["BUFF_REPLACE_EXISTING"] = BuffAddType.REPLACE_EXISTING;
            globals["BUFF_RENEW_EXISTING"] = BuffAddType.RENEW_EXISTING;
            globals["BUFF_STACKS_AND_RENEWS"] = BuffAddType.STACKS_AND_RENEWS;
            globals["BUFF_STACKS_AND_CONTINUE"] = BuffAddType.STACKS_AND_CONTINUE;
            globals["BUFF_STACKS_AND_OVERLAPS"] = BuffAddType.STACKS_AND_OVERLAPS;

            globals["ChannelingStopCondition_NotCancelled"] = ChannelingStopCondition.NotCancelled;
            globals["ChannelingStopCondition_Success"] = ChannelingStopCondition.Success;
            globals["ChannelingStopCondition_Cancel"] = ChannelingStopCondition.Cancel;

            globals["ChannelingStopSource_NotCancelled"] = ChannelingStopSource.NotCancelled;
            globals["ChannelingStopSource_TimeCompleted"] = ChannelingStopSource.TimeCompleted;
            //...
            globals["ChannelingStopSource_LostTarget"] = ChannelingStopSource.LostTarget;
            globals["ChannelingStopSource_StunnedOrSilencedOrTaunted"] = ChannelingStopSource.StunnedOrSilencedOrTaunted;
            //...
            globals["ChannelingStopSource_Die"] = ChannelingStopSource.Die;
            //...
            globals["ChannelingStopSource_Move"] = ChannelingStopSource.Move;

            globals["DAMAGESOURCE_RAW"] = DamageSource.DAMAGE_SOURCE_RAW;
            globals["DAMAGESOURCE_INTERNALRAW"] = DamageSource.DAMAGE_SOURCE_INTERNALRAW;
            globals["DAMAGESOURCE_PERIODIC"] = DamageSource.DAMAGE_SOURCE_PERIODIC;
            globals["DAMAGESOURCE_PROC"] = DamageSource.DAMAGE_SOURCE_PROC;
            globals["DAMAGESOURCE_REACTIVE"] = DamageSource.DAMAGE_SOURCE_REACTIVE;
            //...
            globals["DAMAGESOURCE_SPELL"] = DamageSource.DAMAGE_SOURCE_SPELL;
            globals["DAMAGESOURCE_ATTACK"] = DamageSource.DAMAGE_SOURCE_ATTACK;
            globals["DAMAGESOURCE_DEFAULT"] = DamageSource.DAMAGE_SOURCE_DEFAULT;
            globals["DAMAGESOURCE_SPELLAOE"] = DamageSource.DAMAGE_SOURCE_SPELLAOE;
            globals["DAMAGESOURCE_SPELLPERSIST"] = DamageSource.DAMAGE_SOURCE_SPELLPERSIST;
            //...

            globals["TRUE_DAMAGE"] = DamageType.DAMAGE_TYPE_TRUE; //?
            globals["PHYSICAL_DAMAGE"] = DamageType.DAMAGE_TYPE_PHYSICAL; //?
            globals["MAGIC_DAMAGE"] = DamageType.DAMAGE_TYPE_MAGICAL; //?
            //...

            globals["PAR_MANA"] = PrimaryAbilityResourceType.MANA;
            globals["PAR_ENERGY"] = PrimaryAbilityResourceType.Energy;
            //...
            globals["PAR_SHIELD"] = PrimaryAbilityResourceType.Shield;
            globals["PAR_OTHER"] = PrimaryAbilityResourceType.Other;
            //...

            globals["SPELLBOOK_CHAMPION"] = SpellbookType.SPELLBOOK_CHAMPION;
            globals["SPELLBOOK_SUMMONER"] = SpellbookType.SPELLBOOK_SUMMONER;
            //...

            globals["SpellSlots"] = SpellSlotType.SpellSlots;
            //...
            globals["InventorySlots"] = SpellSlotType.InventorySlots;
            //...
            globals["ExtraSlots"] = SpellSlotType.ExtraSlots;

            globals["FACE_MOVEMENT_DIRECTION"] = ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION;
            globals["KEEP_CURRENT_FACING"] = ForceMovementOrdersFacing.KEEP_CURRENT_FACING;

            globals["FURTHEST_WITHIN_RANGE"] = ForceMovementType.FURTHEST_WITHIN_RANGE;
            globals["FIRST_COLLISION_HIT"] = ForceMovementType.FIRST_COLLISION_HIT;
            //...
            globals["FIRST_WALL_HIT"] = ForceMovementType.FIRST_WALL_HIT;

            globals["HAS_SUNGLASSES"] = ExtraAttributeFlag.HAS_SUNGLASSES;

            globals["HIT_Normal"] = HitResult.HIT_Normal;
            globals["HIT_Critical"] = HitResult.HIT_Critical;
            globals["HIT_Dodge"] = HitResult.HIT_Dodge;
            globals["HIT_Miss"] = HitResult.HIT_Miss;

            globals["TEAM_UNKNOWN"] = TeamId.TEAM_UNKNOWN;
            globals["TEAM_ORDER"] = TeamId.TEAM_ORDER;
            globals["TEAM_CHAOS"] = TeamId.TEAM_CHAOS;
            globals["TEAM_NEUTRAL"] = TeamId.TEAM_NEUTRAL;
            globals["TEAM_CASTER"] = null; //?

            globals["TTYPE_Self"] = TargetingType.Self;
            globals["TTYPE_Target"] = TargetingType.Target;
            globals["TTYPE_Area"] = TargetingType.Area;
            //...
            globals["TTYPE_SelfAOE"] = TargetingType.SelfAOE;
            //...
            globals["TTYPE_Location"] = TargetingType.Location;

            globals["UNITSCAN_Friends"] = null; //?
            globals["SPAWN_LOCATION"] = SpawnType.SPAWN_LOCATION; //?

            globals["EFFCREATE_UPDATE_ORIENTATION"] = EffCreate.UPDATE_ORIENTATION;

            globals["DampenerRegenerationState"] = DampenerState.RegenerationState;

            globals["INPUT_LOCK_CAMERALOCKING"] = InputLockFlags.CameraLocking;
            globals["INPUT_LOCK_CAMERAMOVEMENT"] = InputLockFlags.CameraMovement;
            globals["INPUT_LOCK_ABILITIES"] = InputLockFlags.Abilities;
            globals["INPUT_LOCK_SUMMONERSPELLS"] = InputLockFlags.SummonerSpells;
            globals["INPUT_LOCK_MOVEMENT"] = InputLockFlags.Movement;
            globals["INPUT_LOCK_SHOP"] = InputLockFlags.Shop;
            globals["INPUT_LOCK_MINIMAPMOVEMENT"] = InputLockFlags.MinimapMovement;
            globals["INPUT_LOCK_CHAT"] = InputLockFlags.Chat;

            //Anything below is for AIScripts
            var aiStates = Enum.GetValues<AIState>();
            foreach (var val in aiStates)
            {
                globals[val.ToString().ToUpper()] = val;
            }

            var orders = Enum.GetValues<OrderType>();
            foreach (var val in orders)
            {
                globals[$"ORDER_{val.ToString().ToUpper()}"] = val;
            }

            globals["ORDER_ATTACKTERRAIN_SUSTAINED"] = OrderType.AttackTerrainSustained;
            globals["ORDER_ATTACKTERRAIN_ONCE"] = OrderType.AttackTerrainOnce;

            var roamStates = Enum.GetValues<MinionRoamState>();
            foreach (var val in roamStates)
            {
                globals[val.ToString().ToUpper()] = val;
            }

            globals["STOPREASON_IMMEDIATELY"] = StopReason.IMMEDIATELY;
            globals["STOPREASON_TARGET_LOST"] = StopReason.TARGET_LOST;
            globals["STOPREASON_MOVING"] = StopReason.MOVING;

            globals["LOST_VISIBILITY"] = LostTargetEvent.LOST_VISIBILITY;
        }
    }
}