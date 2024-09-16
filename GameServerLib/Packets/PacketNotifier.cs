using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using GameServerCore.NetInfo;
using GameServerCore.Packets.Enums;
using GameServerLib.Services;
using LeaguePackets;
using LeaguePackets.Common;
using LeaguePackets.Game;
using LeaguePackets.Game.Common;
using LeaguePackets.Game.Events;
using LeaguePackets.LoadScreen;
using Chronobreak.GameServer;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Inventory;
using LENet;
using static GameServerCore.Content.HashFunctions;
using CastInfo = LeaguePackets.Game.Common.CastInfo;
using ChangeSlotSpellDataType = GameServerCore.Enums.ChangeSlotSpellDataType;
using Channel = GameServerCore.Packets.Enums.Channel;
using Color = GameServerCore.Content.Color;
using DeathData = Chronobreak.GameServer.GameObjects.AttackableUnits.DeathData;
using ItemData = LeaguePackets.Game.Common.ItemData;
using Talent = LeaguePackets.Game.Common.Talent;
using TalentGS = Chronobreak.GameServer.GameObjects.Talent;

namespace PacketDefinitions420
{
    /// <summary>
    /// Class containing all function related packets (except handshake) which are sent by the server to game clients.
    /// </summary>
    public class PacketNotifier
    {
        private readonly PacketHandlerManager _packetHandlerManager;
        private Dictionary<int, List<MovementDataNormal>> _heldMovementData = [];
        private Dictionary<int, List<ReplicationData>> _heldReplicationData = [];

        /// <summary>
        /// Instantiation which preps PacketNotifier for packet sending.
        /// </summary>
        /// <param name="packetHandlerManager"></param>
        /// <param name="navGrid"></param>
        public PacketNotifier(PacketHandlerManager packetHandlerManager)
        {
            _packetHandlerManager = packetHandlerManager;
        }

        private static void Log(object packet) =>
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented));

        public void NotifyS2C_SetFadeOut(GameObject o, float value, float time, int userId)
        {
            time /= 1000f;

            var packet = new S2C_SetFadeOut()
            {
                SenderNetID = o.NetId,
                FadeTime = time,
                FadeTargetValue = value
            };
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        public void NotifyAddRegion(Region region, int userId, TeamId team)
        {
            var regionPacket = new AddRegion
            {
                TeamID = (uint)region.Team,
                // TODO: Find out what values this can be and make an enum for it (so far: -2 & -1 for turrets)
                RegionType = region.Type,
                ClientID = region.OwnerClientID,
                // TODO: Verify (usually 0 for vision only?)
                UnitNetID = 0,
                // TODO: Verify (is usually different from UnitNetID in packets, may also be a remnant or for internal use)
                BubbleNetID = region.NetId,
                VisionTargetNetID = 0,
                Position = region.Position,
                // For turrets, usually 25000.0 is used
                TimeToLive = region.Lifetime,
                // 88.4 for turrets
                ColisionRadius = region.PathfindingRadius,
                // 130.0 for turrets
                GrassRadius = region.GrassRadius,
                SizeMultiplier = region.Scale,
                SizeAdditive = region.AdditionalSize,

                HasCollision = region.HasCollision,
                GrantVision = region.GrantVision,
                RevealStealth = region.RevealsStealth,

                BaseRadius = region.VisionRadius // 800.0 for turrets
            };

            if (region.CollisionUnit != null)
            {
                regionPacket.UnitNetID = region.CollisionUnit.NetId;
            }

            if (region.VisionTarget != null)
            {
                regionPacket.VisionTargetNetID = region.VisionTarget.NetId;
            }

            _packetHandlerManager.SendPacket(userId, regionPacket, Channel.CHL_S2C);
        }

        private void SendSpawnPacket(int userId, AttackableUnit u, GamePacket packet, bool wrapIntoVision)
        {
            if (wrapIntoVision)
            {
                NotifyEnterTeamVision(u, userId, packet);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }
        }

        public void NotifyCreateNeutral(NeutralMinion monster, float time, int userId, TeamId team, bool doVision)
        {
            var packet = new S2C_CreateNeutral
            {
                SenderNetID = monster.NetId,
                UniqueName = monster.Name,
                Name = monster.Name,
                SkinName = monster.Model,
                FaceDirectionPosition = monster.Direction,
                DamageBonus = monster.DamageBonus,
                HealthBonus = monster.HealthBonus,
                InitialLevel = monster.MinionLevel,
                NetID = monster.NetId,
                GroupPosition = monster.Camp.CampPosition,
                BuffSideTeamID = (uint)monster.Camp.BuffSide,
                Position = monster.GetPosition3D(),
                SpawnAnimationName = monster.SpawnAnimation,
                AIscript = "",
                //Seems to be the time it is supposed to spawn, not the time when it spawned, check this later
                SpawnTime = time / 1000,
                BehaviorTree = monster.AIScript.AIScriptMetaData.BehaviorTree,
                RevealEvent = (byte)monster.Camp.RevealEvent,
                GroupNumber = monster.Camp.CampIndex,
                MinionRoamState = (uint)monster.RoamState,
                SpawnDuration = monster.Camp.SpawnDuration,
                TeamID = (uint)monster.Team,
                NetNodeID = (byte)NetNodeID.Spawned
            };
            SendSpawnPacket(userId, monster, packet, doVision);
        }

        public void NotifyCreateTurret(LaneTurret turret, int userId, bool doVision)
        {
            var packet = new S2C_CreateTurret
            {
                SenderNetID = turret.NetId,
                NetID = turret.NetId,
                // Verify, taken from packets (does not seem to change)
                NetNodeID = 64,
                Name = turret.Name,
                IsTargetable = turret.IsTargetable,
                IsTargetableToTeamSpellFlags = (uint)turret.Stats.IsTargetableToTeam
            };
            SendSpawnPacket(userId, turret, packet, doVision);
        }

        public void NotifyLaneMinionSpawned(LaneMinion m, int userId, bool doVision)
        {
            var packet = new Barrack_SpawnUnit
            {
                SenderNetID = m.NetId,
                ObjectID = m.NetId,
                ObjectNodeID = 0x40,
                BarracksNetID = m.BarrackSpawn.NetId,
                WaveCount = (byte)m.BarrackSpawn.WaveCount,
                MinionType = (byte)m.MinionSpawnType,
                DamageBonus = (short)m.DamageBonus,
                HealthBonus = (short)m.HealthBonus,
                MinionLevel = (byte)m.MinionLevel
            };
            SendSpawnPacket(userId, m, packet, doVision);
        }

        public void NotifyMarkOrSweepForSoftReconnect(int userId, bool sweep)
        {
            var packet = new S2C_MarkOrSweepForSoftReconnect();
            packet.Unknown1 = sweep;
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        public void NotifyMinionSpawned(Minion minion, int userId, TeamId team, bool doVision)
        {
            var spawnPacket = new SpawnMinionS2C
            {
                SenderNetID = minion.NetId,
                NetID = minion.NetId,
                OwnerNetID = minion.Owner?.NetId ?? 0,
                NetNodeID = (byte)NetNodeID.Spawned,
                Position = minion.GetPosition3D(),
                SkinID = minion.SkinID,
                TeamID = (ushort)minion.Team,
                IgnoreCollision = minion.Stats.ActionState.HasFlag(ActionState.IS_GHOSTED),
                IsWard = minion.IsWard,
                IsLaneMinion = minion.IsLaneMinion,
                IsBot = minion.IsBot,
                IsTargetable = minion.Stats.ActionState.HasFlag(ActionState.TARGETABLE),

                IsTargetableToTeamSpellFlags = (uint)minion.Stats.IsTargetableToTeam,
                VisibilitySize = minion.VisionRadius,
                Name = minion.Name,
                SkinName = minion.Model,
                InitialLevel = (ushort)minion.MinionLevel,
                OnlyVisibleToNetID = minion.VisibilityOwner?.NetId ?? 0
            };
            SendSpawnPacket(userId, minion, spawnPacket, doVision);
        }

        private CastInfo ConvertCastInfo(Chronobreak.GameServer.GameObjects.SpellNS.CastInfo ci)
        {
            return new CastInfo
            {
                SpellHash = HashString(ci.Spell.Name),
                SpellNetID = ci.NetId,

                SpellLevel = (byte)ci.SpellLevel,
                AttackSpeedModifier = ci.AttackSpeedModifier,
                CasterNetID = ci.Caster.NetId,

                SpellChainOwnerNetID = ci.SpellChainOwner.NetId,
                PackageHash = ci.Caster.GetObjHash(),
                MissileNetID = ci.Missile?.NetId ?? 0,

                Targets = ci.Targets.Select(
                    t => new CastInfo.Target()
                    {
                        UnitNetID = t.Unit?.NetId ?? 0,
                        HitResult = (byte)t.HitResult
                    }
                ).ToList(),

                TargetPosition = ci.TargetPosition,
                TargetPositionEnd = ci.TargetPositionEnd,
                DesignerCastTime = ci.DesignerCastTime,
                ExtraCastTime = ci.ExtraCastTime,
                DesignerTotalTime = ci.DesignerTotalTime,

                Cooldown = ci.Cooldown,
                StartCastTime = ci.StartCastTime,

                IsAutoAttack = ci.IsBasicAttack,
                IsSecondAutoAttack = ci.IsSecondAutoAttack,
                IsForceCastingOrChannel = ci.IsForceCastingOrChannel,
                IsOverrideCastPosition = ci.IsOverrideCastPosition,
                IsClickCasted = ci.IsClickCasted,

                SpellSlot = (byte)ci.Spell.Slot,
                ManaCost = ci.ManaCost,
                SpellCastLaunchPosition = ci.SpellCastLaunchPosition,
                AmmoUsed = ci.AmmoUsed,
                AmmoRechargeTime = ci.AmmoRechargeTime
            };
        }

        public void NotifyMissileReplication(SpellMissile m, int userId = -1)
        {
            var misPacket = new MissileReplication
            {
                SenderNetID = m.CastInfo.Missile.NetId,
                Position = m.GetPosition3D(),
                CasterPosition = m.CastInfo.Caster.GetPosition3D(),
                Direction = m.Direction,
                Velocity = m.Direction * m.Speed,
                StartPoint = m.StartPoint,
                EndPoint = m.EndPoint,
                UnitPosition = m.CastInfo.Caster.GetPosition3D(), //TODO: Verify
                TimeFromCreation = m.TimeSinceCreation,
                Speed = m.SpellOrigin.Data.MissileSpeed, //TODO: Send angular or radial speed for circle missiles?

                LifePercentage = (m.Lifetime > 0) ? m.TimeSinceCreation / m.Lifetime : 0,
                TimedSpeedDelta = 0f,             //TODO: What should be here?
                TimedSpeedDeltaTime = 0x7F7FFFFF, //TODO: What should be here?

                Bounced = (m as SpellLineMissile)?.Bounced ?? false,

                CastInfo = ConvertCastInfo(m.CastInfo)
            };

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, misPacket, Channel.CHL_S2C);
            }
            else
            {
                VisionService.BroadCastVisionPacket(m, misPacket);
            }
        }
        public void NotifyS2C_ChangeMissileSpeed(SpellMissile m, float speed, float delay)
        {
            var misPacket = new S2C_ChangeMissileSpeed()
            {
                SenderNetID = m.NetId,
                Speed = speed,
                Delay = delay
            };
            _packetHandlerManager.BroadcastPacket(misPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_ChangeMissileTarget(SpellMissile m, Vector3 pos)
        {
            var misPacket = new S2C_ChangeMissileTarget()
            {
                SenderNetID = m.NetId,
                TargetPosition = pos,
            };
            _packetHandlerManager.BroadcastPacket(misPacket, Channel.CHL_S2C);
        }

        public void NotifySpawnLevelProp(LevelProp levelProp, int userId, TeamId team)
        {
            var packet = new SpawnLevelPropS2C
            {
                NetID = levelProp.NetId,
                NetNodeID = levelProp.NetNodeID,
                SkinID = levelProp.SkinID,
                Position = levelProp.Position.ToVector3(levelProp.Height),
                FacingDirection = levelProp.Direction,
                PositionOffset = levelProp.PositionOffset,
                Scale = levelProp.Scale,
                TeamID = (ushort)team,
                SkillLevel = levelProp.SkillLevel,
                Rank = levelProp.Rank,
                Type = levelProp.Type,
                Name = levelProp.Name,
                PropName = levelProp.Model
            };
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        public void NotifySpawnPet(Pet pet, int userId, TeamId team, bool doVision)
        {
            var packet = new CHAR_SpawnPet
            {
                OwnerNetID = pet.Owner.NetId,
                NetNodeID = (byte)NetNodeID.Spawned,
                Position = pet.GetPosition3D(),

                //TODO: The clone can be spawned from CharScript
                CastSpellLevelPlusOne = pet.SourceSpell?.Level ?? 1,

                Duration = pet.LifeTime,
                TeamID = (uint)pet.Team,
                DamageBonus = pet.DamageBonus,
                HealthBonus = pet.HealthBonus,
                Name = pet.Name,
                Skin = pet.Model,
                SkinID = pet.SkinID,
                BuffName = pet.CloneBuffName,
                CloneInventory = pet.CloneInventory,
                ShowMinimapIconIfClone = pet.ShowMinimapIconIfClone,
                DisallowPlayerControl = pet.DisallowPlayerControl,
                DoFade = pet.DoFade,
                CloneID = pet.ClonedUnit?.NetId ?? 0,
                SenderNetID = pet.NetId
            };

            SendSpawnPacket(userId, pet, packet, doVision);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified attacker detailing that they have targeted the specified target.
        /// </summary>
        /// <param name="attacker">AI that is targeting an AttackableUnit.</param>
        /// <param name="target">AttackableUnit that is being targeted by the attacker.</param>
        public void NotifyAI_TargetS2C(ObjAIBase attacker, AttackableUnit? target)
        {
            var targetPacket = new AI_TargetS2C
            {
                SenderNetID = attacker.NetId,
                TargetNetID = 0
            };

            if (target != null)
            {
                targetPacket.TargetNetID = target.NetId;
            }

            // TODO: Verify if we need to account for other cases.
            if (attacker is BaseTurret)
            {
                _packetHandlerManager.BroadcastPacket(targetPacket, Channel.CHL_S2C);
            }
            else
            {
                VisionService.BroadCastVisionPacket(attacker, targetPacket);
            }
        }

        /// <summary>
        /// Sends a packet to all players that a specific unit has changed it's AIState, functionality unknown (if even needed), found in Lua AIScripts as NetSetState
        /// </summary>
        /// <param name="owner">Target unit</param>
        /// <param name="state">New AI state</param>
        public void NotifyS2C_AIState(ObjAIBase owner, AIState state)
        {
            var targetPacket = new S2C_AI_State
            {
                SenderNetID = owner.NetId,
                AIState = (uint)state
            };
            _packetHandlerManager.BroadcastPacket(targetPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified attacker detailing that they have targeted the specified champion.
        /// </summary>
        /// <param name="attacker">AI that is targeting a champion.</param>
        /// <param name="target">Champion that is being targeted by the attacker.</param>
        public void NotifyAI_TargetHeroS2C(ObjAIBase attacker, AttackableUnit? target)
        {
            var targetPacket = new AI_TargetHeroS2C
            {
                SenderNetID = attacker.NetId,
                TargetNetID = 0
            };

            if (target != null)
            {
                targetPacket.TargetNetID = target.NetId;
            }

            VisionService.BroadCastVisionPacket(attacker, targetPacket);
        }

        /// <summary>
        /// Sends a packet to the specified user or all users informing them of the given client's summoner data such as runes, summoner spells, masteries (or talents as named internally), etc.
        /// </summary>
        /// <param name="client">Info about the player's summoner data.</param>
        /// <param name="userId">User to send the packet to. Set to -1 to broadcast.</param>
        public void NotifyAvatarInfo(ClientInfo client, int userId = -1)
        {
            AvatarInfo_Server avatar = new()
            {
                SenderNetID = client.Champion.NetId
            };

            avatar.SummonerIDs[0] = HashString(client.SummonerSkills[0]);
            avatar.SummonerIDs[1] = HashString(client.SummonerSkills[1]);

            for (int i = 0; i < avatar.ItemIDs.Length; ++i)
            {
                if (client.Champion.RuneList.Runes.TryGetValue(i, out var runeValue))
                {
                    avatar.ItemIDs[i] = (uint)runeValue;
                    continue;
                }
                avatar.ItemIDs[i] = 0;
            }

            for (int i = 0; i < avatar.Talents.Length; i++)
            {
                TalentGS talent = i >= client.Champion.TalentInventory.Talents.Count ? TalentGS.EmptyTalent : client.Champion.TalentInventory.Talents.ElementAt(i).Value;
                avatar.Talents[i] = new Talent
                {
                    Hash = HashString(talent.Id),
                    Level = talent.Rank
                };
            }

            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(avatar, Channel.CHL_S2C);
                return;
            }

            _packetHandlerManager.SendPacket(userId, avatar, Channel.CHL_S2C);
        }

        private BasicAttackData CreateBasicAttackData(Chronobreak.GameServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            return new BasicAttackData
            {
                TargetNetID = castInfo.Target?.Unit.NetId ?? 0,

                // Based on DesignerCastTime. Always negative. Value range from replays: [-0.14, 0].
                ExtraTime = 0, //TODO: Verify, maybe related to CastInfo.ExtraCastTime?

                MissileNextID = castInfo.Missile?.NetId ?? 0,
                AttackSlot = (byte)castInfo.Spell.Slot,

                TargetPosition = MovementVector.ToCenteredScaledCoordinates(
                    castInfo.TargetPosition.ToVector2(), Game.Map.NavigationGrid
                ).ToVector3(castInfo.TargetPosition.Y),
            };
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified  unit is starting their next auto attack.
        /// </summary>
        public void NotifyBasic_Attack(Chronobreak.GameServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            var attacker = castInfo.Caster;
            var basicAttackPacket = new Basic_Attack
            {
                SenderNetID = attacker.NetId,
                Attack = CreateBasicAttackData(castInfo)
            };
            VisionService.BroadCastVisionPacket(attacker, basicAttackPacket);
        }

        /// <summary>
        /// Sends a packet to all players that the specified attacker is starting their first auto attack.
        /// </summary>
        public void NotifyBasic_Attack_Pos(Chronobreak.GameServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            var attacker = castInfo.Caster;
            var basicAttackPacket = new Basic_Attack_Pos
            {
                SenderNetID = attacker.NetId,
                Attack = CreateBasicAttackData(castInfo),
                Position = attacker.Position // TODO: Verify
            };

            VisionService.BroadCastVisionPacket(attacker, basicAttackPacket);
        }

        public void NotifyS2C_ForceCreateMissile(SpellMissile m)
        {
            var packet = new S2C_ForceCreateMissile()
            {
                SenderNetID = m.CastInfo.Caster.NetId,
                MissileNetID = m.NetId
            };

            VisionService.BroadCastVisionPacket(m, packet);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified building has died.
        /// </summary>
        /// <param name="deathData"></param>
        public void NotifyBuilding_Die(DeathData? deathData)
        {
            if (deathData is null)
            {
                return;
            }

            Building_Die buildingDie = new()
            {
                SenderNetID = deathData.Unit.NetId,
                AttackerNetID = deathData.Killer.NetId
            };

            if (deathData?.Unit is ObjAIBase obj)
            {
                AssistMarker lastAssist = obj.EnemyAssistMarkers.First();
                if (lastAssist is not null)
                {
                    buildingDie.LastHeroNetID = lastAssist.Source.NetId;
                }
            }

            _packetHandlerManager.BroadcastPacket(buildingDie, Channel.CHL_S2C);
        }
        /// <summary>
        /// Sends a packet to the player attempting to buy an item that their purchase was successful.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="gameObject">GameObject of type ObjAIBase that can buy items.</param>
        /// <param name="itemInstance">Item instance housing all information about the item that has been bought.</param>
        public void NotifyBuyItem(ObjAIBase gameObject, Item itemInstance)
        {
            var itemData = new ItemData
            {
                ItemID = (uint)itemInstance.ItemData.Id,
                Slot = gameObject.ItemInventory.GetItemSlot(itemInstance),
                ItemsInSlot = (byte)itemInstance.StackCount,
                SpellCharges = 0 // TODO: Unhardcode
            };

            var buyItemPacket = new BuyItemAns
            {
                SenderNetID = gameObject.NetId,
                Item = itemData,
                Bitfield = 0 //TODO: find out what this does, currently unknown
            };

            VisionService.BroadCastVisionPacket(gameObject, buyItemPacket);
        }

        /// <summary>
        /// Sends a packet to the player letting them know how many times they can undo
        /// </summary>
        /// <param name="gameObject">User to send the packet to.</param>
        /// <param name="stackSize">How many undo actions they can make.</param>
        public void NotifyS2C_SetUndoEnabled(ObjAIBase gameObject, int stackSize)
        {
            var s2CUndoEnabled = new S2C_SetUndoEnabled
            {
                SenderNetID = (uint)gameObject.NetId,
                UndoStackSize = (byte)stackSize,
            };

            VisionService.BroadCastVisionPacket(gameObject, s2CUndoEnabled);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing that the specified owner unit's spell in the specified slot has been changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="owner">Unit that owns the spell being changed.</param>
        /// <param name="slot">Slot of the spell being changed.</param>
        /// <param name="changeType">Type of change being made.</param>
        /// <param name="isSummonerSpell">Whether or not the spell being changed is a summoner spell.</param>
        /// <param name="targetingType">New targeting type to set.</param>
        /// <param name="newName">New internal name of a spell to set.</param>
        /// <param name="newRange">New cast range for the spell to set.</param>
        /// <param name="newMaxCastRange">New max cast range for the spell to set.</param>
        /// <param name="newDisplayRange">New max display range for the spell to set.</param>
        /// <param name="newIconIndex">New index of an icon for the spell to set.</param>
        /// <param name="offsetTargets">New target netids for the spell to set.</param>
        public void NotifyChangeSlotSpellData(int userId, ObjAIBase owner, int slot, ChangeSlotSpellDataType changeType, bool isSummonerSpell = false, TargetingType targetingType = TargetingType.Invalid, string newName = "", float newRange = 0, float newMaxCastRange = 0, float newDisplayRange = 0, int newIconIndex = 0x0, List<uint> offsetTargets = null)
        {
            ChangeSpellData spellData = new ChangeSpellDataUnknown()
            {
                SpellSlot = (byte)slot,
                IsSummonerSpell = isSummonerSpell
            };

            switch (changeType)
            {
                case ChangeSlotSpellDataType.TargetingType:
                    {
                        if (targetingType != TargetingType.Invalid)
                        {
                            spellData = new ChangeSpellDataTargetingType()
                            {
                                SpellSlot = (byte)slot,
                                IsSummonerSpell = isSummonerSpell,
                                TargetingType = (byte)targetingType
                            };
                        }
                        break;
                    }
                case ChangeSlotSpellDataType.SpellName:
                    {
                        spellData = new ChangeSpellDataSpellName()
                        {
                            SpellSlot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            SpellName = newName
                        };
                        break;
                    }
                case ChangeSlotSpellDataType.Range:
                    {
                        spellData = new ChangeSpellDataRange()
                        {
                            SpellSlot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            CastRange = newRange
                        };
                        break;
                    }
                case ChangeSlotSpellDataType.MaxGrowthRange:
                    {
                        spellData = new ChangeSpellDataMaxGrowthRange()
                        {
                            SpellSlot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            OverrideMaxCastRange = newMaxCastRange
                        };
                        break;
                    }
                case ChangeSlotSpellDataType.RangeDisplay:
                    {
                        spellData = new ChangeSpellDataRangeDisplay()
                        {
                            SpellSlot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            OverrideCastRangeDisplay = newDisplayRange
                        };
                        break;
                    }
                case ChangeSlotSpellDataType.IconIndex:
                    {
                        spellData = new ChangeSpellDataIconIndex()
                        {
                            SpellSlot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            IconIndex = (byte)newIconIndex
                        };
                        break;
                    }
                case ChangeSlotSpellDataType.OffsetTarget:
                    {
                        if (offsetTargets != null)
                        {
                            spellData = new ChangeSpellDataOffsetTarget()
                            {
                                SpellSlot = (byte)slot,
                                IsSummonerSpell = isSummonerSpell,
                                Targets = offsetTargets
                            };
                        }
                        break;
                    }
            }

            var changePacket = new ChangeSlotSpellData()
            {
                SenderNetID = owner.NetId,
                ChangeSpellData = spellData
            };

            _packetHandlerManager.SendPacket(userId, changePacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of a specified ObjAIBase explaining that their specified spell's cooldown has been set.
        /// </summary>
        /// <param name="u">ObjAIBase who owns the spell going on cooldown.</param>
        /// <param name="slotId">Slot of the spell.</param>
        /// <param name="currentCd">Amount of time the spell has already been on cooldown (if applicable).</param>
        /// <param name="totalCd">Maximum amount of time the spell's cooldown can be.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        public void NotifyCHAR_SetCooldown(ObjAIBase u, int slotId, float currentCd, float totalCd, int userId = -1)
        {
            var cdPacket = new CHAR_SetCooldown
            {
                SenderNetID = u.NetId,
                Slot = (byte)slotId,
                PlayVOWhenCooldownReady = false, // TODO: Unhardcode
                IsSummonerSpell = u is Champion && (slotId is 4 or 5),
                Cooldown = currentCd,
                MaxCooldownForDisplay = 0 // TODO: Verify (packet loses functionality otherwise)
            };
            if (userId < 0)
            {
                VisionService.BroadCastVisionPacket(u, cdPacket);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, cdPacket, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to the specified user that the mana or cooldown values has change
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="unit">Unit to update the cost</param>
        /// <param name="spell">Spell to update the cost</param>
        /// <param name="costType">1 To Cooldown - 2 To Mana</param>
        public void NotifyS2C_UnitSetSpellPARCost(ObjAIBase unit, Spell spell, int costType, int userId = -1)
        {
            float cooldownDiff = spell.CurrentCooldown - spell.Cooldown;
            float manaDiff = spell.ManaCostBase > 0 ? (spell.ManaCost / spell.ManaCostBase) - 1f : 0;

            var packet = new S2C_UnitSetSpellPARCost()
            {
                SenderNetID = unit.NetId,
                SpellSlot = spell.Slot,
                CostType = (byte)costType,
                Amount = (costType == 1) ? cooldownDiff : manaDiff
            };

            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified user that highlights the specified GameObject.
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="unit">GameObject to highlght.</param>
        public void NotifyCreateUnitHighlight(int userId, GameObject unit)
        {
            var highlightPacket = new S2C_CreateUnitHighlight
            {
                SenderNetID = unit.NetId,
                TargetNetID = unit.NetId
            };

            _packetHandlerManager.SendPacket(userId, highlightPacket, Channel.CHL_S2C);
        }

        public void NotifyDampenerSwitchStates(Inhibitor inhibitor)
        {
            var inhibState = new DampenerSwitchStates
            {
                SenderNetID = inhibitor.NetId,
                State = (byte)inhibitor.State,
                Duration = (ushort)inhibitor.RespawnTime
            };
            _packetHandlerManager.BroadcastPacket(inhibState, Channel.CHL_S2C);
        }

        public void NotifyDeath(DeathData deathData)
        {
            switch (deathData.Unit)
            {
                case Champion ch:
                    NotifyNPC_Hero_Die(deathData);
                    break;
                case Minion minion:
                    if (minion is Pet || minion is LaneMinion)
                    {
                        NotifyS2C_NPC_Die_MapView(deathData);
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case ObjBuilding building:
                    NotifyBuilding_Die(deathData);
                    break;
                default:
                    NotifyNPC_Die_Broadcast(deathData);
                    break;
            }
        }

        /// <summary>
        /// Sends a packet to the specified user which is intended for debugging.
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="data">Array of bytes representing the packet's data.</param>
        public void NotifyDebugPacket(int userId, byte[] data)
        {
            _packetHandlerManager.SendPacket(userId, data, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the destruction of (usually) an auto attack missile.
        /// </summary>
        /// <param name="p">Projectile that is being destroyed.</param>
        public void NotifyDestroyClientMissile(SpellMissile p)
        {
            var misPacket = new S2C_DestroyClientMissile
            {
                SenderNetID = p.NetId
            };
            _packetHandlerManager.BroadcastPacket(misPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to either all players with vision of a target, or the specified player.
        /// The packet displays the specified message of the specified type as floating text over a target.
        /// </summary>
        /// <param name="target">Target to display on.</param>
        /// <param name="message">Message to display.</param>
        /// <param name="textType">Type of text to display. Refer to FloatTextType</param>
        /// <param name="userId">User to send to. 0 = sends to all in vision.</param>
        /// <param name="param">Optional parameters for the text. Untested, function unknown.</param>
        public void NotifyDisplayFloatingText(FloatingTextData floatTextData, TeamId team = 0, int userId = -1)
        {
            var textPacket = new DisplayFloatingText
            {
                TargetNetID = floatTextData.Target.NetId,
                FloatTextType = (uint)floatTextData.FloatTextType,
                Param = floatTextData.Param,
                Message = floatTextData.Message
            };

            if (userId < 0)
            {
                if (team != TeamId.TEAM_UNKNOWN)
                {
                    _packetHandlerManager.BroadcastPacketTeam(team, textPacket, Channel.CHL_S2C);
                }
                else
                {
                    VisionService.BroadCastVisionPacket(floatTextData.Target, textPacket);
                }
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, textPacket, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to either all players with vision of the specified object or the specified user. The packet details the data surrounding the specified GameObject that is required by players when a GameObject enters vision such as items, shields, skin, and movements.
        /// </summary>
        /// <param name="o">GameObject entering vision.</param>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="isChampion">Whether or not the GameObject entering vision is a Champion.</param>
        /// <param name="ignoreVision">Optionally ignore vision checks when sending this packet.</param>
        /// <param name="packets">Takes in a list of packets to send alongside this vision packet.</param>
        /// TODO: Incomplete implementation.
        public void NotifyEnterVisibilityClient(GameObject o, int userId = -1, bool ignoreVision = false, List<GamePacket> packets = null)
        {

            var enterVis = VisionService.ConstructEnterVisibilityClientPacket(o, packets);

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, enterVis, Channel.CHL_S2C);
            }
            else
            {
                if (ignoreVision)
                {
                    _packetHandlerManager.BroadcastPacket(enterVis, Channel.CHL_S2C);
                }
                else
                {
                    VisionService.BroadCastVisionPacket(o, enterVis);
                }
            }
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified unit detailing that the unit has begun facing the specified direction.
        /// </summary>
        /// <param name="obj">GameObject that is changing their orientation.</param>
        /// <param name="direction">3D direction the unit will face.</param>
        /// <param name="isInstant">Whether or not the unit should instantly turn to the direction.</param>
        /// <param name="turnTime">The amount of time (seconds) the turn should take.</param>
        public void NotifyFaceDirection(GameObject obj, Vector3 direction, bool isInstant = true, float turnTime = 0.0833f)
        {
            var facePacket = new S2C_FaceDirection()
            {
                SenderNetID = obj.NetId,
                Direction = direction,
                DoLerpTime = !isInstant,
                LerpTime = turnTime
            };

            _packetHandlerManager.BroadcastPacket(facePacket, Channel.CHL_S2C);
        }

        public void NotifyFXEnterTeamVisibility(uint particleNetId, uint senderNetId, TeamId team, int userId)
        {
            var fxVisPacket = new S2C_FX_OnEnterTeamVisibility
            {
                SenderNetID = senderNetId,
                NetID = particleNetId
            };

            fxVisPacket.VisibilityTeam = 0;
            //TODO: Provide support for more than 2 teams
            if (team is TeamId.TEAM_CHAOS or TeamId.TEAM_NEUTRAL)
            {
                fxVisPacket.VisibilityTeam = 1;
            }

            _packetHandlerManager.SendPacket(userId, fxVisPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified particle has been destroyed.
        /// </summary>
        /// <param name="particle">Particle that is being destroyed.</param>
        /// TODO: Change to only broadcast to players who have vision of the particle (maybe?).
        /// TODO: SenderNetId is not always the particle Netid
        public void NotifyFXKill(uint particleNetId, uint senderNetId)
        {
            var fxKill = new FX_Kill
            {
                SenderNetID = senderNetId,
                NetID = particleNetId
            };
            _packetHandlerManager.BroadcastPacket(fxKill, Channel.CHL_S2C);
        }

        public void NotifyFXLeaveTeamVisibility(uint particleNetId, uint senderNetId, TeamId team, int userId = -1)
        {
            var fxVisPacket = new S2C_FX_OnLeaveTeamVisibility
            {
                SenderNetID = senderNetId,
                NetID = particleNetId
            };

            fxVisPacket.VisibilityTeam = 0;
            //TODO: Provide support for more than 2 teams
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                fxVisPacket.VisibilityTeam = 1;
            }

            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacketTeam(team, fxVisPacket, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, fxVisPacket, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the game has started. Sent when all players have finished loading.
        /// </summary>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players.</param>
        public void NotifyGameStart(int userId = -1)
        {
            var start = new S2C_StartGame
            {
                EnablePause = true
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(start, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, start, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing the state (DEAD/ALIVE) of the specified inhibitor.
        /// </summary>
        /// <param name="inhibitor">Inhibitor to check.</param>
        /// <param name="killer">Killer of the inhibitor (if applicable).</param>
        /// <param name="assists">Assists of the killer (if applicable).</param>
        public void NotifyInhibitorState(Inhibitor inhibitor, DeathData? deathData = null)
        {
            switch (inhibitor.State)
            {
                case DampenerState.RegenerationState:
                    var annoucementDeath = new OnDampenerDie
                    {
                        //All mentions i found were 0, investigate further if we'd want to unhardcode this
                        GoldGiven = 0.0f,
                        AssistCount = 0
                    };
                    if (deathData is not null)
                    {
                        annoucementDeath.OtherNetID = deathData.Killer.NetId;
                    }
                    NotifyS2C_OnEventWorld(annoucementDeath, inhibitor);
                    NotifyBuilding_Die(deathData);

                    break;
                case DampenerState.RespawningState:
                    var annoucementRespawn = new OnDampenerRespawn
                    {
                        OtherNetID = inhibitor.NetId
                    };
                    NotifyS2C_OnEventWorld(annoucementRespawn, inhibitor);
                    break;
            }
            NotifyDampenerSwitchStates(inhibitor);
        }
        /// <summary>
        /// Sends a basic heartbeat packet to either the given player or all players.
        /// </summary>
        public void NotifyKeyCheck(int clientID, long playerId, uint version, ulong checkSum = 0, int action = 0, bool broadcast = false)
        {
            var keyCheck = new KeyCheckPacket
            {
                Action = (byte)action,
                ClientID = clientID,
                PlayerID = playerId,
                VersionNumber = version,
                CheckSum = checkSum,
                // Padding
                ExtraBytes = new byte[4]
            };

            if (broadcast)
            {
                _packetHandlerManager.BroadcastPacket(keyCheck, Channel.CHL_HANDSHAKE);
            }
            else
            {
                _packetHandlerManager.SendPacket(clientID, keyCheck, Channel.CHL_HANDSHAKE);
            }
        }

        /// <summary>
        /// Sends a packet to either the specified player or team detailing that the specified GameObject has left vision.
        /// </summary>
        /// <param name="o">GameObject that left vision.</param>
        /// <param name="team">TeamId to send the packet to; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="userId">User to send the packet to.</param>
        /// TODO: Verify where this should be used.
        public void NotifyLeaveLocalVisibilityClient(GameObject o, TeamId team, int userId = -1)
        {
            var leaveLocalVis = new OnLeaveLocalVisibilityClient
            {
                SenderNetID = o.NetId
            };

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, leaveLocalVis, Channel.CHL_S2C);
                return;
            }

            _packetHandlerManager.BroadcastPacketTeam(team, leaveLocalVis, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to either the specified user or team detailing that the specified GameObject has left vision.
        /// </summary>
        /// <param name="o">GameObject that left vision.</param>
        /// <param name="team">TeamId to send the packet to; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="userId">User to send the packet to (if applicable).</param>
        /// TODO: Verify where this should be used.
        public void NotifyLeaveVisibilityClient(GameObject o, TeamId team, int userId = -1)
        {
            var leaveVis = new OnLeaveVisibilityClient
            {
                SenderNetID = o.NetId
            };

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, leaveVis, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.BroadcastPacketTeam(team, leaveVis, Channel.CHL_S2C);
            }

            NotifyLeaveLocalVisibilityClient(o, team, userId);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the order and size of both teams on the loading screen.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="players">Client info of all players in the loading screen.</param>
        public void NotifyLoadScreenInfo(int userId, TeamId team, List<ClientInfo> players)
        {
            uint orderSizeCurrent = 0;
            uint chaosSizeCurrent = 0;

            var teamRoster = new TeamRosterUpdate
            {
                TeamSizeOrder = 6,
                TeamSizeChaos = 6
            };

            foreach (var player in players)
            {
                if (player.Team is TeamId.TEAM_ORDER)
                {
                    teamRoster.OrderMembers[orderSizeCurrent] = player.PlayerId;
                    orderSizeCurrent++;
                }
                // TODO: Verify if it is ok to allow neutral
                else
                {
                    teamRoster.ChaosMembers[chaosSizeCurrent] = player.PlayerId;
                    chaosSizeCurrent++;
                }
            }

            teamRoster.TeamSizeOrderCurrent = orderSizeCurrent;
            teamRoster.TeamSIzeChaosCurrent = chaosSizeCurrent;

            _packetHandlerManager.SendPacket(userId, teamRoster, Channel.CHL_LOADING_SCREEN);
        }


        public void NotifyS2C_CameraBehavior(Champion target, Vector3 position)
        {
            var packet = new S2C_CameraBehavior
            {
                SenderNetID = target.NetId,
                Position = position
            };

            _packetHandlerManager.SendPacket(target.ClientId, packet, Channel.CHL_S2C);
        }
        /// <summary>
        /// Sends a packet to all players that updates the specified unit's model.
        /// </summary>
        /// <param name="obj">AttackableUnit to update.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="skinID">Unit's skin ID after changing model.</param>
        /// <param name="modelOnly">Wether or not it's only the model that it's being changed(?). I don't really know what's this for</param>
        /// <param name="overrideSpells">Wether or not the user's spells should be overriden, i assume it would be used for things like Nidalee or Elise.</param>
        /// <param name="replaceCharacterPackage">Unknown.</param>
        public void NotifyS2C_ChangeCharacterData(ObjAIBase obj, string skinName, bool modelOnly = true, bool overrideSpells = false, bool replaceCharacterPackage = false)
        {
            var newCharData = new S2C_ChangeCharacterData
            {
                SenderNetID = obj.NetId,
                Data = new CharacterStackData
                {
                    SkinID = (uint)obj.SkinID,
                    SkinName = skinName,
                    OverrideSpells = overrideSpells,
                    ModelOnly = modelOnly,
                    ReplaceCharacterPackage = replaceCharacterPackage
                    // TODO: ID variable, acts like a character ID, used later on in PopCharacterData packet for unloading.
                    // Changes over time, or perhaps as new objects are added, does not have large values like NetID.
                }
            };

            VisionService.BroadCastVisionPacket(obj, newCharData);
        }

        /// <summary>
        /// Sends a packet to all players who have vision of the specified buff's target detailing that the buff has been added to the target.
        /// </summary>
        public void NotifyNPC_BuffAdd2(Buff b, int stacks)
        {
            var addPacket = new NPC_BuffAdd2
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                BuffType = (byte)b.BuffType,
                Count = (byte)stacks,
                IsHidden = b.IsHidden,
                BuffNameHash = HashString(b.Name),
                PackageHash = b.TargetUnit.GetObjHash(),
                RunningTime = b.TimeElapsed,
                Duration = b.Duration,
            };
            if (b.SourceUnit != null)
            {
                addPacket.CasterNetID = b.SourceUnit.NetId;
            }
            _packetHandlerManager.BroadcastPacket(addPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players who have vision of the target of the specified buff detailing that the buff was removed from its target.
        /// </summary>
        /// <param name="b">Buff that was removed.</param>
        public void NotifyNPC_BuffRemove2(Buff b)
        {
            NotifyNPC_BuffRemove2(b.TargetUnit, b.Slot, b.Name);
        }
        public void NotifyNPC_BuffRemove2(AttackableUnit owner, int slot, string name)
        {
            var removePacket = new NPC_BuffRemove2
            {
                SenderNetID = owner.NetId, //TODO: Verify if this should change depending on the removal source
                BuffSlot = (byte)slot,
                BuffNameHash = HashString(name),
                RunTimeRemove = 0
            };
            _packetHandlerManager.BroadcastPacket(removePacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the target of the specified buff detailing that the buff previously in the same slot was replaced by the newly specified buff.
        /// </summary>
        /// <param name="b">Buff that will replace the old buff in the same slot.</param>
        public void NotifyNPC_BuffReplace(Buff b)
        {
            var replacePacket = new NPC_BuffReplace
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                RunningTime = b.TimeElapsed,
                Duration = b.Duration,
                CasterNetID = b.SourceUnit?.NetId ?? 0
            };

            VisionService.BroadCastVisionPacket(b.TargetUnit, replacePacket);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the target of the specified buff detailing an update to the number of buffs in the specified buff's slot
        /// </summary>
        /// <param name="b">Buff who's count is being updated.</param>
        /// <param name="duration">Total time the buff should last.</param>
        /// <param name="runningTime">Time since the buff's creation.</param>
        public void NotifyNPC_BuffUpdateCount(Buff b, int stacks)
        {
            var updatePacket = new NPC_BuffUpdateCount
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                Count = (byte)stacks,
                Duration = b.Duration,
                RunningTime = b.TimeElapsed,
                CasterNetID = 0
            };
            if (b.SourceUnit != null)
            {
                updatePacket.CasterNetID = b.SourceUnit.NetId;
            }

            VisionService.BroadCastVisionPacket(b.TargetUnit, updatePacket);
        }

        /*
        /// <summary>
        /// Sends a packet to all players with vision of the specified target detailing an update to the number of buffs in each of the buff slots occupied by the specified group of buffs.
        /// </summary>
        /// <param name="target">Unit who's buffs will be updated.</param>
        /// <param name="buffs">Group of buffs to update.</param>
        /// <param name="duration">Total time the buff should last.</param>
        /// <param name="runningTime">Time since the buff's creation.</param>
        public void NotifyNPC_BuffUpdateCountGroup(AttackableUnit target, List<Buff> buffs, float duration, float runningTime)
        {
            var updateGroupPacket = new NPC_BuffUpdateCountGroup
            {
                Duration = duration,
                RunningTime = runningTime
            };
            var entries = new List<BuffUpdateCountGroupEntry>();
            for (int i = 0; i < buffs.Count; i++)
            {
                var entry = new BuffUpdateCountGroupEntry
                {
                    OwnerNetID = buffs[i].TargetUnit.NetId,
                    CasterNetID = 0,
                    BuffSlot = buffs[i].Slot,
                    Count = (byte)buffs[i].StackCount
                };

                if (buffs[i].OriginSpell != null)
                {
                    entry.CasterNetID = buffs[i].OriginSpell.Caster.NetId;
                }
                entries.Add(entry);
            }
            updateGroupPacket.Entries = entries;

            _packetHandlerManager.BroadcastPacketVision(target, updateGroupPacket, Channel.CHL_S2C);
        }
        */
        /*
        /// <summary>
        /// Sends a packet to all players with vision of the target of the specified buff detailing an update to the stack counter of the specified buff.
        /// </summary>
        /// <param name="b">Buff who's stacks will be updated.</param>
        public void NotifyNPC_BuffUpdateNumCounter(Buff b)
        {
            var updateNumPacket = new NPC_BuffUpdateNumCounter
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = b.Slot,
                Counter = b.TotalStackCount
            };
            _packetHandlerManager.BroadcastPacketVision(b.TargetUnit, updateNumPacket, Channel.CHL_S2C);
        }
        */

        /// <summary>
        /// Sends a packet to all players with vision of the owner of the specified shield detailing it's values.
        /// </summary>
        /// <param name="unit">Target Unit</param>
        /// <param name="physical">Physical shield value modified</param>
        /// <param name="magical">Magical shield value modified</param>
        /// <param name="amount">Shield value</param>
        /// <param name="stopShieldFade">True = Time Fade; False = Damage Fade</param>
        public void NotifyModifyShield(AttackableUnit unit, bool physical, bool magical, float amount, bool stopShieldFade = false)
        {
            var answer = new ModifyShield()
            {
                SenderNetID = unit.NetId,
                Physical = physical,
                Magical = magical,
                Amount = amount,
                StopShieldFade = stopShieldFade,
            };
            _packetHandlerManager.BroadcastPacket(answer, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the owner of the specified spell detailing that a spell has been cast.
        /// </summary>
        /// <param name="s">Spell being cast.</param>
        public void NotifyNPC_CastSpellAns(Chronobreak.GameServer.GameObjects.SpellNS.CastInfo ci)
        {
            var packet = new NPC_CastSpellAns
            {
                SenderNetID = ci.Caster.NetId,
                CasterPositionSyncID = Environment.TickCount, //TODO:
                Unknown1 = false, // TODO: Find what this is (if false, causes CasterPositionSyncID to be used)
                CastInfo = ConvertCastInfo(ci)
            };

            VisionService.BroadCastVisionPacket(ci.Caster, packet);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified unit has been killed by the specified killer.
        /// </summary>
        /// <param name="data">Data of the death.</param>
        public void NotifyNPC_Die_Broadcast(DeathData data)
        {
            var dieMapView = new NPC_Die_Broadcast
            {
                SenderNetID = data.Unit.NetId,
                DeathData = new LeaguePackets.Game.Common.DeathData
                {
                    BecomeZombie = data.BecomeZombie,
                    DieType = (byte)data.DieType,
                    KillerNetID = data.Killer.NetId,
                    DamageType = (byte)data.DamageType,
                    DamageSource = (byte)data.DamageSource,
                    DeathDuration = data.DeathDuration
                }
            };
            _packetHandlerManager.BroadcastPacket(dieMapView, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that a champion has died and calls a death timer update packet.
        /// </summary>
        /// <param name="champion">Champion that died.</param>
        /// <param name="killer">Unit that killed the Champion.</param>
        /// <param name="goldFromKill">Amount of gold the killer received.</param>
        public void NotifyNPC_Hero_Die(DeathData deathData)
        {
            if(deathData.Unit is not Champion ch)
            {
                return;
            }
            NotifyS2C_UpdateDeathTimer(ch);

            uint killerNetID = deathData.Killer.NetId;
            NPC_Hero_Die cd = new()
            {
                SenderNetID = deathData.Unit.NetId,
                DeathData = new LeaguePackets.Game.Common.DeathData
                {
                    KillerNetID = killerNetID,
                    DieType = (byte)deathData.DieType,
                    DamageType = (byte)deathData.DamageType,
                    DamageSource = (byte)deathData.DamageSource,
                    BecomeZombie = deathData.BecomeZombie,
                    DeathDuration = ch.Stats.RespawnTimer / 1000f
                }
            };
            _packetHandlerManager.BroadcastPacket(cd, Channel.CHL_S2C);

            NotifyNPC_Die_EventHistory(ch, killerNetID);
        }

        /// <summary>
        /// Sends a packet to all players that a champion has been forced to die.
        /// </summary>
        /// <param name="champion">Champion that died.</param>
        /// <param name="killer">Unit that killed the Champion.</param>
        /// <param name="goldFromKill">Amount of gold the killer received.</param>
        public void NotifyNPC_ForceDead(DeathData lastDeathData)
        {
            //league sends both packets
            var forceDead = new NPC_ForceDead
            {
                SenderNetID = lastDeathData.Unit.NetId,
                DeathDuration = lastDeathData.Unit.Stats.RespawnTimer / 1000f
            };
            var heroDie = new NPC_Hero_Die
            {
                SenderNetID = lastDeathData.Unit.NetId,
                DeathData = new LeaguePackets.Game.Common.DeathData
                {
                    KillerNetID = lastDeathData.Killer.NetId,
                    DieType = (byte)lastDeathData.DieType,
                    DamageType = (byte)lastDeathData.DamageType,
                    DamageSource = (byte)lastDeathData.DamageSource,
                    BecomeZombie = lastDeathData.BecomeZombie,
                    DeathDuration = lastDeathData.Unit.Stats.RespawnTimer / 1000f
                }
            };
            _packetHandlerManager.BroadcastPacket(heroDie, Channel.CHL_S2C);
            _packetHandlerManager.BroadcastPacket(forceDead, Channel.CHL_S2C);
        }

        public void NotifyNPC_Die_EventHistory(Champion ch, uint killerNetID = 0)
        {
            var history = new NPC_Die_EventHistory();
            history.SenderNetID = ch.NetId;
            history.KillerNetID = killerNetID;
            history.Duration = 0;
            if (ch.EventHistory.Count > 0)
            {
                float firstTimestamp = ch.EventHistory[0].Timestamp;
                float lastTimestamp = ch.EventHistory[ch.EventHistory.Count - 1].Timestamp;
                history.Duration = lastTimestamp - firstTimestamp;
            }
            history.EventSourceType = 0; //TODO: Confirm that it is always zero
            history.Entries = ch.EventHistory;

            _packetHandlerManager.SendPacket(ch.ClientId, history, Channel.CHL_S2C);
        }
        /// <summary>
        /// Sends a packet to all players with vision of the specified AttackableUnit detailing that the attacker has abrubtly stopped their attack (can be a spell or auto attack, although internally AAs are also spells).
        /// </summary>
        /// <param name="attacker">AttackableUnit that stopped their auto attack.</param>
        /// <param name="isSummonerSpell">Whether or not the spell is a summoner spell.</param>
        /// <param name="keepAnimating">Whether or not to continue the auto attack animation after the abrupt stop.</param>
        /// <param name="destroyMissile">Whether or not to destroy the missile which may have been created before stopping (client-side removal).</param>
        /// <param name="overrideVisibility">Whether or not stopping this auto attack overrides visibility checks.</param>
        /// <param name="forceClient">Whether or not this packet should be forcibly applied, regardless of if an auto attack is being performed client-side.</param>
        /// <param name="missileNetID">NetId of the missile that may have been spawned by the spell.</param>
        /// TODO: Find a better way to implement these parameters
        public void NotifyNPC_InstantStop_Attack(AttackableUnit attacker, bool isSummonerSpell,
            bool keepAnimating = false,
            bool destroyMissile = true,
            bool overrideVisibility = true,
            bool forceClient = false,
            uint missileNetID = 0)
        {
            var stopAttack = new NPC_InstantStop_Attack
            {
                SenderNetID = attacker.NetId,
                MissileNetID = missileNetID, //TODO: Fix MissileNetID, currently it only works when it is 0
                KeepAnimating = keepAnimating,
                DestroyMissile = destroyMissile,
                OverrideVisibility = overrideVisibility,
                IsSummonerSpell = isSummonerSpell,
                ForceDoClient = forceClient
            };

            VisionService.BroadCastVisionPacket(attacker, stopAttack);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified Champion has leveled up.
        /// </summary>
        /// <param name="c">Champion which leveled up.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        public void NotifyNPC_LevelUp(ObjAIBase obj)
        {
            byte level, trainingPoints = 0;
            switch (obj)
            {
                case Champion c:
                    level = (byte)c.Experience.Level;
                    trainingPoints = c.Experience.SpellTrainingPoints.TrainingPoints;
                    break;
                case Minion m:
                    level = (byte)m.MinionLevel;
                    break;
                default:
                    return;
            }

            var levelUp = new NPC_LevelUp()
            {
                SenderNetID = obj.NetId,
                Level = (byte)level,
                // TODO: Typo >>>:(
                AveliablePoints = trainingPoints
            };

            VisionService.BroadCastVisionPacket(obj, levelUp);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that they have leveled up the specified skill.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="netId">NetId of the GameObject whos skill is being leveled up.</param>
        /// <param name="slot">Slot of the skill being leveled up.</param>
        /// <param name="level">Current level of the skill.</param>
        /// <param name="points">Number of skill points available after the skill has been leveled up.</param>
        public void NotifyNPC_UpgradeSpellAns(int userId, uint netId, int slot, int level, int points)
        {
            var upgradeSpellPacket = new NPC_UpgradeSpellAns
            {
                SenderNetID = netId,
                Slot = (byte)slot,
                SpellLevel = (byte)level,
                SkillPoints = (byte)points
            };

            _packetHandlerManager.SendPacket(userId, upgradeSpellPacket, Channel.CHL_GAMEPLAY);
        }

        /// <summary>
        /// Sends a packet to all users with vision of the given caster detailing that the given spell has been set to auto cast (as well as the spell in the critSlot) for the given caster.
        /// </summary>
        /// <param name="caster">Unit responsible for the autocasting.</param>
        /// <param name="spell">Spell to auto cast.</param>
        /// // TODO: Verify critSlot functionality
        /// <param name="critSlot">Optional spell slot to cast when a crit is going to occur.</param>
        public void NotifyNPC_SetAutocast(ObjAIBase caster, Spell spell, int critSlot = 0)
        {
            var autoCast = new NPC_SetAutocast
            {
                SenderNetID = caster.NetId,
                Slot = (byte)spell.Slot,
                CritSlot = (byte)critSlot
            };

            if (critSlot <= 0)
            {
                autoCast.CritSlot = autoCast.Slot;
            }

            VisionService.BroadCastVisionPacket(caster, autoCast);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified unit detailing that the specified unit's stats have been updated.
        /// </summary>
        /// <param name="u">Unit who's stats have been updated.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="partial">Whether or not the packet should only include stats marked as changed.</param>
        /// TODO: Replace with LeaguePackets and preferably move all uses of this function to a central EventHandler class (if one is fully implemented).
        public void NotifyOnReplication(AttackableUnit u, int userId = -1, bool partial = true)
        {
            if (u.Replication != null)
            {
                var us = new OnReplication()
                {
                    SyncID = (uint)Environment.TickCount,
                    // TODO: Support multi-unit replication creation (perhaps via a separate function which takes in a list of units).
                    ReplicationData = new List<ReplicationData>(1){
                        u.Replication.GetData(partial)
                    }
                };
                var channel = Channel.CHL_LOW_PRIORITY;
                if (userId < 0)
                {
                    VisionService.BroadCastVisionPacket(u, us, channel, PacketFlags.UNSEQUENCED);
                }
                else
                {
                    _packetHandlerManager.SendPacket(userId, us, channel, PacketFlags.UNSEQUENCED);
                }
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the game has paused.
        /// </summary>
        /// <param name="seconds">Amount of time till the pause ends.</param>
        /// <param name="showWindow">Whether or not to show a pause window.</param>
        public void NotifyPausePacket(ClientInfo player, int seconds, bool isTournament)
        {
            var pg = new PausePacket
            {
                //Check if SenderNetID should be the person that requested the pause or just 0
                ClientID = player.ClientId,
                IsTournament = isTournament,
                PauseTimeRemaining = seconds
            };
            //I Assumed that, since the packet requires idividual client IDs, that it also sends the packets individually, by useing the SendPacket Channel, double check if that's valid.
            _packetHandlerManager.SendPacket(player.ClientId, pg, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the specified client's loading screen progress.
        /// </summary>
        /// <param name="request">Info of the target client given via the client who requested loading screen progress.</param>
        /// <param name="clientInfo">Client info of the client who's progress is being requested.</param>
        public void NotifyPingLoadInfo(ClientInfo client, C2S_Ping_Load_Info request)
        {
            var response = new S2C_Ping_Load_Info
            {
                ConnectionInfo = new ConnectionInfo
                {
                    ClientID = request.ConnectionInfo.ClientID,
                    Ping = request.ConnectionInfo.Ping,
                    PlayerID = client.PlayerId,
                    ETA = request.ConnectionInfo.ETA,
                    Ready = request.ConnectionInfo.Ready,
                    Percentage = request.ConnectionInfo.Percentage,
                    Count = request.ConnectionInfo.Count
                },
            };
            //Logging->writeLine("loaded: %f, ping: %f, %f", loadInfo->loaded, loadInfo->ping, loadInfo->f3);
            _packetHandlerManager.BroadcastPacket(response, Channel.CHL_LOW_PRIORITY, PacketFlags.NONE);
        }

        /// <summary>
        /// Sends a packet to all players that a champion has respawned.
        /// </summary>
        /// <param name="c">Champion that respawned.</param>
        public void NotifyHeroReincarnateAlive(Champion c, float parToRestore)
        {
            var cr = new HeroReincarnateAlive
            {
                SenderNetID = c.NetId,
                Position = c.Position,
                PARValue = parToRestore
            };
            _packetHandlerManager.BroadcastPacket(cr, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified AI detailing that item in the specified slot was removed (or the number of stacks of the item in that slot changed).
        /// </summary>
        /// <param name="ai">AI with the items.</param>
        /// <param name="slot">Slot of the item that was removed.</param>
        /// <param name="remaining">Number of stacks of the item left (0 if not applicable).</param>
        public void NotifyRemoveItem(ObjAIBase ai, int slot, int remaining)
        {
            var ria = new RemoveItemAns()
            {
                SenderNetID = ai.NetId,
                Slot = (byte)slot,
                ItemsInSlot = (byte)remaining,
                NotifyInventoryChange = true
            };
            VisionService.BroadCastVisionPacket(ai, ria);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified region was removed.
        /// </summary>
        /// <param name="region">Region to remove.</param>
        public void NotifyRemoveRegion(Region region)
        {
            var removeRegion = new RemoveRegion()
            {
                RegionNetID = region.NetId
            };

            _packetHandlerManager.BroadcastPacket(removeRegion, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the highlight of the specified GameObject was removed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="unit">GameObject that had the highlight.</param>
        public void NotifyRemoveUnitHighlight(int userId, GameObject unit)
        {
            var highlightPacket = new S2C_RemoveUnitHighlight
            {
                SenderNetID = unit.NetId,
                NetID = unit.NetId
            };
            _packetHandlerManager.SendPacket(userId, highlightPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing skin and player name information of the specified player on the loading screen.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="player">Player information to send.</param>
        public void NotifyRequestRename(int userId, ClientInfo player)
        {
            var loadName = new RequestRename
            {
                PlayerID = player.PlayerId,
                PlayerName = player.Name,
                // Most packets show a large default value (in place of what you would expect to be 0)
                // Seems to be randomized per-game and used for every RequestRename packet during that game.
                // So, using this SkinNo may be incorrect.
                SkinID = player.SkinNo,
            };
            _packetHandlerManager.SendPacket(userId, loadName, Channel.CHL_LOADING_SCREEN);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing skin information of the specified player on the loading screen.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="player">Player information to send.</param>
        public void NotifyRequestReskin(int userId, ClientInfo player)
        {
            var loadChampion = new RequestReskin
            {
                PlayerID = player.PlayerId,
                SkinID = player.SkinNo,
                SkinName = player.Champion.Model
            };
            _packetHandlerManager.SendPacket(userId, loadChampion, Channel.CHL_LOADING_SCREEN);
        }

        /// <summary>
        /// Sends a packet to player detailing that the game has been unpaused.
        /// </summary>
        /// <param name="unpauser">Unit that unpaused the game.</param>
        /// <param name="showWindow">Whether or not to show a window before unpausing (delay).</param>
        public void NotifyResumePacket(Champion unpauser, ClientInfo player, bool isDelayed)
        {
            var resume = new ResumePacket
            {
                SenderNetID = 0,
                Delayed = isDelayed,
                ClientID = player.ClientId
            };
            if (unpauser != null)
            {
                resume.SenderNetID = unpauser.NetId;
            }

            _packetHandlerManager.SendPacket(player.ClientId, resume, Channel.CHL_S2C);
        }

        public void NotifyS2C_ActivateMinionCamp(NeutralMinionCamp monsterCamp, int userId = -1)
        {
            var packet = new S2C_ActivateMinionCamp
            {
                Position = monsterCamp.CampPosition,
                CampIndex = (byte)monsterCamp.CampIndex,
                SpawnDuration = monsterCamp.SpawnDuration,
                TimerType = monsterCamp.TimerType
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }
        }

        public void NotifyS2C_AmmoUpdate(Spell spell)
        {
            if (spell.Caster is Champion ch)
            {
                var packet = new S2C_AmmoUpdate
                {
                    IsSummonerSpell = spell.SpellName.StartsWith("Summoner"),
                    SpellSlot = spell.Slot,
                    CurrentAmmo = spell.CurrentAmmo,
                    // TODO: Implement this. Example spell which uses it is Syndra R.
                    MaxAmmo = -1,
                    SenderNetID = spell.Caster.NetId
                };

                if (spell.CurrentAmmo < spell.SpellData.MaxAmmo)
                {
                    packet.AmmoRecharge = spell.CurrentAmmoCooldown;
                    packet.AmmoRechargeTotalTime = spell.AmmoCooldown;
                }

                _packetHandlerManager.SendPacket(ch.ClientId, packet, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to the specified user or all users detailing that the hero designated to the given clientInfo has been created.
        /// </summary>
        /// <param name="clientInfo">Information about the client which had their hero created.</param>
        /// <param name="userId">User to send the packet to. Set to -1 to broadcast.</param>
        public void NotifyS2C_CreateHero(ClientInfo clientInfo, int userId, TeamId team, bool doVision)
        {
            var champion = clientInfo.Champion;
            var heroPacket = new S2C_CreateHero()
            {
                NetID = champion.NetId,
                ClientID = clientInfo.ClientId,
                // NetNodeID,
                // For bots (0 = Beginner, 1 = Intermediate)
                SkillLevel = 0,
                IsBot = champion.IsBot,
                // BotRank, deprecated as of v4.18
                SpawnPositionIndex = clientInfo.InitialSpawnIndex,
                SkinID = champion.SkinID,
                Name = champion.Name,
                Skin = champion.Model,
                DeathDurationRemaining = champion.Stats.RespawnTimer,
                // TimeSinceDeath
                TeamIsOrder = champion.Team is TeamId.TEAM_ORDER,
                CreateHeroDeath = champion.Stats.IsDead ? CreateHeroDeath.Dead : champion.Stats.IsZombie ? CreateHeroDeath.Zombie : CreateHeroDeath.Alive
            };
            SendSpawnPacket(userId, champion, heroPacket, doVision);
        }

        public void NotifyS2C_CreateMinionCamp(NeutralMinionCamp monsterCamp, int userId, TeamId team)
        {
            var packet = new S2C_CreateMinionCamp
            {
                Position = new Vector3(monsterCamp.CampPosition.X, 60, monsterCamp.CampPosition.Z),
                CampIndex = (byte)monsterCamp.CampIndex,
                MinimapIcon = monsterCamp.CampIcon,
                RevealAudioVOComponentEvent = (byte)monsterCamp.RevealEvent,
                SideTeamID = (byte)monsterCamp.BuffSide,
                Expire = monsterCamp.GetTimerExpiry(),
                TimerType = monsterCamp.TimerType
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Disables the U.I when the game ends
        /// </summary>
        /// <param name="player"></param>
        public void NotifyS2C_DisableHUDForEndOfGame()
        {
            _packetHandlerManager.BroadcastPacket(new S2C_DisableHUDForEndOfGame(), Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends packets to all players notifying the result of a match (Victory or defeat)
        /// </summary>
        /// <param name="losingTeam">The Team that lost the match</param>
        /// <param name="time">The offset for the result to actually be displayed</param>
        public void NotifyS2C_EndGame(TeamId winningTeam)
        {
            //TODO: Provide support for more than 2 teams
            var gameEndPacket = new S2C_EndGame
            {
                IsTeamOrderWin = winningTeam == TeamId.TEAM_ORDER
            };
            _packetHandlerManager.BroadcastPacket(gameEndPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_HandleCapturePointUpdate(int capturePointIndex, uint otherNetId, int PARType, int attackTeam, CapturePointUpdateCommand capturePointUpdateCommand)
        {
            //TODO: Provide support for more than 2 teams
            var packet = new S2C_HandleCapturePointUpdate
            {
                CapturePointIndex = (byte)capturePointIndex,
                OtherNetID = otherNetId,
                PARType = (byte)PARType,
                AttackTeam = (byte)attackTeam,
                CapturePointUpdateCommand = (byte)capturePointUpdateCommand
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, PacketFlags.NONE);
        }

        /// <summary>
        /// Notifies the game about a score
        /// </summary>
        /// <param name="team"></param>
        /// <param name="score"></param>
        public void NotifyS2C_HandleGameScore(TeamId team, int score)
        {
            //TODO: Provide support for more than 2 teams
            var packet = new S2C_HandleGameScore
            {
                TeamID = (uint)team,
                Score = score
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, PacketFlags.NONE);
        }

        /// <summary>
        /// Sends a side bar tip to the specified player (ex: quest tips).
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="title">Title of the tip.</param>
        /// <param name="text">Description text of the tip.</param>
        /// <param name="imagePath">Path to an image that will be embedded in the tip.</param>
        /// <param name="tipCommand">Action suggestion(? unconfirmed).</param>
        /// <param name="playerNetId">NetID to send the packet to.</param>
        /// <param name="targetNetId">NetID of the target referenced by the tip.</param>
        /// TODO: tipCommand should be a lib/core enum that gets translated into a league version specific packet enum as it may change over time.
        public void NotifyS2C_HandleTipUpdate(int userId, string title, string text, string imagePath, int tipCommand, uint playerNetId, uint targetNetId)
        {
            var packet = new S2C_HandleTipUpdate
            {
                SenderNetID = playerNetId,
                TipCommand = (byte)tipCommand,
                TipImagePath = imagePath,
                TipName = text,
                TipOther = title,
                TipID = targetNetId
            };
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the stats (CS, kills, deaths, etc) of the player who owns the specified Champion.
        /// </summary>
        /// <param name="champion">Champion owned by the player.</param>
        public void NotifyS2C_HeroStats(Champion champion)
        {
            //TODO: Find out what exactly this does and when/how it is sent

            //var response = new S2C_HeroStats { Data = champion.ChampionStatistics };
            //_packetHandlerManager.BroadcastPacket(response, Channel.CHL_S2C);
        }

        public void NotifyS2C_IncrementPlayerScore(ScoreData scoreData)
        {
            var packet = new S2C_IncrementPlayerScore
            {
                PlayerNetID = scoreData.Owner.NetId,
                TotalPointValue = scoreData.Owner.ChampionStats.Score,
                PointValue = scoreData.Points,
                ShouldCallout = scoreData.DoCallOut,
                ScoreCategory = (byte)scoreData.ScoreCategory,
                ScoreEvent = (byte)scoreData.ScoreEvent
            };

            VisionService.BroadCastVisionPacket(scoreData.Owner, packet);
        }

        /// <summary>
        /// Sends a packet to the specified client's team detailing a map ping.
        /// </summary>
        /// <param name="client">Info of the client that initiated the ping.</param>
        /// <param name="pos">2D top-down position of the ping.</param>
        /// <param name="targetNetId">Target of the ping (if applicable).</param>
        /// <param name="type">Type of ping; COMMAND/ATTACK/DANGER/MISSING/ONMYWAY/FALLBACK/REQUESTHELP. *NOTE*: Not all ping types are supported yet.</param>
        public void NotifyS2C_MapPing(Vector2 pos, Pings type, uint targetNetId = 0, ClientInfo client = null)
        {
            var response = new S2C_MapPing
            {
                // TODO: Verify if this is correct. Usually 0.

                TargetNetID = targetNetId,
                PingCategory = (byte)type,
                Position = pos,
                //Unhardcode these bools later
                PlayAudio = true,
                ShowChat = true,
                PingThrottled = false,
                PlayVO = true
            };

            if (targetNetId != 0)
            {
                response.TargetNetID = targetNetId;
            }

            if (client != null)
            {
                response.SenderNetID = client.Champion.NetId;
                response.SourceNetID = client.Champion.NetId;
                _packetHandlerManager.BroadcastPacketTeam(client.Team, response, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.BroadcastPacket(response, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to the specified player which forces their camera to move to a specified point given certain parameters.
        /// </summary>
        /// <param name="player">Player who'll it's camera moved</param>
        /// <param name="startPosition">The starting position of the camera (Not yet known how to get it's values)</param>
        /// <param name="endPosition">End point to where the camera will move</param>
        /// <param name="travelTime">The time the camera will have to travel the given distance</param>
        /// <param name="startFromCurretPosition">Wheter or not it starts from current position</param>
        /// <param name="unlockCamera">Whether or not the camera is unlocked</param>
        public void NotifyS2C_MoveCameraToPoint(ClientInfo player, Vector3 startPosition, Vector3 endPosition, float travelTime = 0, bool startFromCurretPosition = true, bool unlockCamera = false)
        {
            var cam = new S2C_MoveCameraToPoint
            {
                SenderNetID = player.Champion.NetId,
                StartFromCurrentPosition = startFromCurretPosition,
                UnlockCamera = unlockCamera,
                TravelTime = travelTime,
                TargetPosition = endPosition
            };
            if (startPosition != Vector3.Zero)
            {
                cam.StartPosition = startPosition;
            }

            _packetHandlerManager.SendPacket(player.ClientId, cam, Channel.CHL_S2C);
        }
        public void NotifyS2C_Neutral_Camp_Empty(NeutralMinionCamp neutralCamp, ObjAIBase? killer = null, int userId = -1)
        {
            var packet = new S2C_Neutral_Camp_Empty
            {
                KillerNetID = killer?.NetId ?? 0,
                //Investigate what this does, from what i see on packets, my guess is a check if the enemy team had vision of the camp dying
                DoPlayVO = true,
                CampIndex = neutralCamp.CampIndex,
                TimerType = neutralCamp.TimerType,
                //Check what the hell this is for
                TimerExpire = neutralCamp.GetTimerExpiry(),
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified unit has been killed by the specified killer.
        /// </summary>
        /// <param name="data">Data of the death.</param>
        public void NotifyS2C_NPC_Die_MapView(DeathData data)
        {
            var dieMapView = new S2C_NPC_Die_MapView
            {
                SenderNetID = data.Unit.NetId,
                DeathData = new LeaguePackets.Game.Common.DeathData
                {
                    BecomeZombie = data.BecomeZombie,
                    DieType = (byte)data.DieType,
                    DamageType = (byte)data.DamageType,
                    DamageSource = (byte)data.DamageSource,
                    DeathDuration = data.DeathDuration
                }
            };

            if (data.Killer != null)
            {
                dieMapView.DeathData.KillerNetID = data.Killer.NetId;
            }

            _packetHandlerManager.BroadcastPacket(dieMapView, Channel.CHL_S2C);
        }

        public void NotifyOnEnterTeamVisibility(GameObject o, TeamId team, int userId)
        {
            var enterTeamVis = new S2C_OnEnterTeamVisibility
            {
                SenderNetID = o.NetId,
                //TODO: Provide support for more than 2 teams
                VisibilityTeam = 0
            };
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                enterTeamVis.VisibilityTeam = 1;
            }

            _packetHandlerManager.SendPacket(userId, enterTeamVis, Channel.CHL_S2C);
        }

        public void NotifyOnEvent(IEvent gameEvent, AttackableUnit sender = null)
        {
            var packet = new OnEvent
            {
                Event = gameEvent
            };

            if (sender != null)
            {
                packet.SenderNetID = sender.NetId;
            }

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that announces a specified message (ex: "Minions have spawned.")
        /// </summary>
        /// <param name="eventId">Id of the event to happen.</param>
        /// <param name="sourceNetID">Not yet know it's use.</param>
        public void NotifyS2C_OnEventWorld(IEvent mapEvent, AttackableUnit? source = null)
        {
            if (mapEvent == null)
            {
                return;
            }

            var packet = new S2C_OnEventWorld
            {
                EventWorld = new EventWorld
                {
                    Event = mapEvent,
                    Source = source?.NetId ?? 0
                }
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to either all players with vision of the specified GameObject or a specified user.
        /// The packet contains details of which team lost visibility of the GameObject and should only be used after it is first initialized into vision (NotifyEnterVisibility).
        /// </summary>
        /// <param name="o">GameObject going out of vision.</param>
        /// <param name="userId">User to send the packet to.</param>
        public void NotifyOnLeaveTeamVisibility(GameObject o, TeamId team, int userId = -1)
        {
            var leaveTeamVis = new S2C_OnLeaveTeamVisibility
            {
                SenderNetID = o.NetId,
                //TODO: Provide support for more than 2 teams
                VisibilityTeam = 0
            };
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                leaveTeamVis.VisibilityTeam = 1;
            }

            if (userId < 0)
            {
                // TODO: Verify if we should use BroadcastPacketTeam instead.
                _packetHandlerManager.BroadcastPacket(leaveTeamVis, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, leaveTeamVis, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified object's current animations have been paused/unpaused.
        /// </summary>
        /// <param name="obj">GameObject that is playing the animation.</param>
        /// <param name="pause">Whether or not to pause/unpause animations.</param>
        public void NotifyS2C_PauseAnimation(GameObject obj, bool pause)
        {
            var animPacket = new S2C_PauseAnimation
            {
                SenderNetID = obj.NetId,
                Pause = pause
            };

            _packetHandlerManager.BroadcastPacket(animPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified object detailing that it is playing the specified animation.
        /// </summary>
        /// <param name="obj">GameObject that is playing the animation.</param>
        /// <param name="animation">Internal name of the animation to play.</param>
        /// TODO: Implement AnimationFlags enum for this and fill it in.
        /// <param name="flags">Animation flags. Refer to AnimationFlags enum.</param>
        /// <param name="timeScale">How fast the animation should play. Default 1x speed.</param>
        /// <param name="startTime">Time in the animation to start at.</param>
        /// TODO: Verify if this description is correct, if not, correct it.
        /// <param name="speedScale">How much the speed of the GameObject should affect the animation.</param>
        public void NotifyS2C_PlayAnimation(GameObject obj, string animation, AnimationFlags flags = 0, float timeScale = 1.0f, float startTime = 0.0f, float speedScale = 1.0f)
        {
            var packet = new S2C_PlayAnimation
            {
                SenderNetID = obj.NetId,
                AnimationFlags = (byte)flags,
                ScaleTime = timeScale,
                StartProgress = startTime,
                SpeedRatio = speedScale,
                AnimationName = animation
            };
            VisionService.BroadCastVisionPacket(obj, packet);
        }

        public void NotifyS2C_UnlockAnimation(GameObject obj, string name)
        {
            var packet = new S2C_UnlockAnimation()
            {
                AnimationName = name
            };
            //TODO: Handle animation like fades
            VisionService.BroadCastVisionPacket(obj, packet);
        }

        /// <summary>
        /// Sends a packet to all players detailing an emotion that is being performed by the unit that owns the specified netId.
        /// </summary>
        /// <param name="type">Type of emotion being performed; DANCE/TAUNT/LAUGH/JOKE/UNK.</param>
        /// <param name="netId">NetID of the unit performing the emotion.</param>
        public void NotifyS2C_PlayEmote(Emotions type, uint netId)
        {
            // convert type
            EmoteID targetType;
            switch (type)
            {
                case Emotions.DANCE:
                    targetType = EmoteID.Dance;
                    break;
                case Emotions.TAUNT:
                    targetType = EmoteID.Taunt;
                    break;
                case Emotions.LAUGH:
                    targetType = EmoteID.Laugh;
                    break;
                case Emotions.JOKE:
                    targetType = EmoteID.Joke;
                    break;
                case Emotions.UNK:
                    targetType = (EmoteID)type;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var packet = new S2C_PlayEmote
            {
                SenderNetID = netId,
                EmoteID = (byte)targetType
            };
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        public void NotifyS2C_PlaySound(string soundName, AttackableUnit soundOwner)
        {
            var packet = new S2C_PlaySound
            {
                SoundName = soundName,
                OwnerNetID = soundOwner.NetId
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player which is meant as a response to the players query about the status of the game.
        /// </summary>
        /// <param name="userId">User to send the packet to; player that sent the query.</param>
        public void NotifyS2C_QueryStatusAns(int userId)
        {
            var response = new S2C_QueryStatusAns
            {
                Response = true
            };
            _packetHandlerManager.SendPacket(userId, response, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified unit detailing that its animation states have changed to the specified animation pairs.
        /// Replaces the unit's normal animation behaviors with the given animation pairs. Structure of the animationPairs is expected to follow the same structure from before the replacement.
        /// </summary>
        /// <param name="u">AttackableUnit to change.</param>
        /// <param name="animationPairs">Dictionary of animations to set.</param>
        public void NotifyS2C_SetAnimStates(AttackableUnit u)
        {
            var setAnimPacket = new S2C_SetAnimStates
            {
                SenderNetID = u.NetId,
                AnimationOverrides = u.AnimationStates
            };

            _packetHandlerManager.BroadcastPacket(setAnimPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_SetGreyscaleEnabledWhenDead(ClientInfo client, bool enabled)
        {
            var packet = new S2C_SetGreyscaleEnabledWhenDead
            {
                Enabled = enabled,
                SenderNetID = client.Champion.NetId
            };

            _packetHandlerManager.SendPacket(client.ClientId, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing that the spell in the given slot has had its spelldata changed to the spelldata of the given spell name.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="netId">NetId of the unit that owns the spell being changed.</param>
        /// <param name="spellName">Internal name of the spell to grab spell data from (to set).</param>
        /// <param name="slot">Slot of the spell being changed.</param>
        public void NotifyS2C_SetSpellData(int userId, uint netId, string spellName, int slot)
        {
            var spellDataPacket = new S2C_SetSpellData
            {
                SenderNetID = netId,
                ObjectNetID = netId,
                HashedSpellName = HashString(spellName),
                SpellSlot = (byte)slot
            };

            _packetHandlerManager.SendPacket(userId, spellDataPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the level of the spell in the given slot has changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="netId">NetId of the unit that owns the spell being changed.</param>
        /// <param name="slot">Slot of the spell being changed.</param>
        /// <param name="level">New level of the spell to set.</param>
        public void NotifyS2C_SetSpellLevel(int userId, uint netId, int slot, int level)
        {
            var spellLevelPacket = new S2C_SetSpellLevel
            {
                SenderNetID = netId,
                SpellSlot = slot,
                SpellLevel = level
            };

            _packetHandlerManager.SendPacket(userId, spellLevelPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the game has started the spawning GameObjects that occurs at the start of the game.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        public void NotifyS2C_StartSpawn(int userId, TeamId team, List<ClientInfo> players)
        {
            var start = new S2C_StartSpawn
            {
                BotCountOrder = 0,
                BotCountChaos = 0
            };

            foreach (var player in players)
            {
                if (player.Champion.IsBot)
                {
                    if (team is TeamId.TEAM_ORDER)
                    {
                        start.BotCountOrder++;
                    }
                    else
                    {
                        start.BotCountChaos++;
                    }
                }
            }

            _packetHandlerManager.SendPacket(userId, start, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified object has stopped playing an animation.
        /// </summary>
        /// <param name="obj">GameObject that is playing the animation.</param>
        /// <param name="animation">Internal name of the animation to stop.</param>
        /// <param name="stopAll">Whether or not to stop all animations. Only works if animation is empty/null.</param>
        /// <param name="fade">Whether or not the animation should fade before stopping.</param>
        /// <param name="ignoreLock">Whether or not locked animations should still be stopped.</param>
        public void NotifyS2C_StopAnimation(GameObject obj, string animation, bool stopAll = false, bool fade = false, bool ignoreLock = true)
        {
            var animPacket = new S2C_StopAnimation
            {
                SenderNetID = obj.NetId,
                Fade = fade,
                IgnoreLock = ignoreLock,
                StopAll = stopAll,
                AnimationName = animation
            };

            _packetHandlerManager.BroadcastPacket(animPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing spell tooltip parameters that the game does not inform automatically.
        /// </summary>
        /// <param name="data">The list of changed tool tip values.</param>
        public void NotifyS2C_ToolTipVars(List<ToolTipData> data)
        {
            List<TooltipVars> variables = [];
            foreach (var tip in data)
            {
                var vars = new TooltipVars()
                {
                    OwnerNetID = tip.NetID,
                    SlotIndex = (byte)tip.Slot
                };

                for (var x = 0; x < tip.Values.Length; x++)
                {
                    vars.HideFromEnemy[x] = tip.Values[x].Hide;
                    vars.Values[x] = tip.Values[x].Value;
                }

                variables.Add(vars);
            }

            var answer = new S2C_ToolTipVars
            {
                Tooltips = variables
            };

            _packetHandlerManager.BroadcastPacket(answer, Channel.CHL_S2C, PacketFlags.NONE);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified attacker that it is looking at (targeting) the specified attacked unit with the given AttackType.
        /// </summary>
        /// <param name="attacker">Unit that is attacking.</param>
        /// <param name="target">Unit that is being attacked.</param>
        public void NotifyS2C_UnitSetLookAt(AttackableUnit attacker, LookAtType lookAtType, AttackableUnit? target, Vector3 targetPosition = default)
        {
            var packet = new S2C_UnitSetLookAt
            {
                SenderNetID = attacker.NetId,
                LookAtType = (byte)lookAtType,
                TargetPosition = target?.Position3D ?? targetPosition,
                TargetNetID = target?.NetId ?? 0
            };
            VisionService.BroadCastVisionPacket(attacker, packet);
        }

        public void NotifyS2C_UpdateAscended(ObjAIBase ascendant = null)
        {
            var packet = new S2C_UpdateAscended();
            if (ascendant != null)
            {
                packet.AscendedNetID = ascendant.NetId;
            }
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, PacketFlags.NONE);
        }

        /// <summary>
        /// Sends a packet to all players detailing the attack speed cap overrides for this game.
        /// </summary>
        /// <param name="overrideMax">Whether or not to override the maximum attack speed cap.</param>
        /// <param name="maxAttackSpeedOverride">Value to override the maximum attack speed cap.</param>
        /// <param name="overrideMin">Whether or not to override the minimum attack speed cap.</param>
        /// <param name="minAttackSpeedOverride">Value to override the minimum attack speed cap.</param>
        public void NotifyS2C_UpdateAttackSpeedCapOverrides(bool overrideMax, float maxAttackSpeedOverride, bool overrideMin, float minAttackSpeedOverride, AttackableUnit unit = null)
        {
            var overridePacket = new S2C_UpdateAttackSpeedCapOverrides
            {
                DoOverrideMax = overrideMax,
                DoOverrideMin = overrideMin,
                MaxAttackSpeedOverride = maxAttackSpeedOverride,
                MinAttackSpeedOverride = minAttackSpeedOverride
            };
            if (unit != null)
            {
                overridePacket.SenderNetID = unit.NetId;
            }
            _packetHandlerManager.BroadcastPacket(overridePacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the given bounce missile that it has updated (unit/position).
        /// </summary>
        /// <param name="p">Missile that has been updated.</param>
        public void NotifyS2C_UpdateBounceMissile(SpellMissile p)
        {
            var packet = new S2C_UpdateBounceMissile()
            {
                SenderNetID = p.NetId,
                TargetNetID = p.TargetUnit!.NetId,
                CasterPosition = p.Position3D
            };
            VisionService.BroadCastVisionPacket(p, packet);
        }

        public void NotifyS2C_LineMissileHitList(SpellLineMissile p, IEnumerable<AttackableUnit> units)
        {
            var packet = new S2C_LineMissileHitList()
            {
                SenderNetID = p.NetId,
                Targets = units.Select(u => u.NetId).ToList()
            };
            VisionService.BroadCastVisionPacket(p, packet);
        }

        public void NotifyS2C_ChainMissileSync(SpellChainMissile p)
        {
            var packet = new S2C_ChainMissileSync()
            {
                OwnerNetworkID = p.SpellOrigin.Caster.NetId,
                SenderNetID = p.NetId,
                TargetCount = 1,
            };
            packet.TargetNetIDs[0] = p.TargetUnit!.NetId;
            VisionService.BroadCastVisionPacket(p, packet);
        }

        /// <summary>
        /// Sends a packet to all players updating a champion's death timer.
        /// </summary>
        /// <param name="champion">Champion that died.</param>
        public void NotifyS2C_UpdateDeathTimer(Champion champion)
        {
            var cdt = new S2C_UpdateDeathTimer { SenderNetID = champion.NetId, DeathDuration = champion.Stats.RespawnTimer / 1000f };
            _packetHandlerManager.BroadcastPacket(cdt, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing that the specified spell's toggle state has been updated.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="s">Spell being updated.</param>
        public void NotifyS2C_UpdateSpellToggle(int userId, Spell s)
        {
            var spellTogglePacket = new S2C_UpdateSpellToggle
            {
                SenderNetID = s.Caster.NetId,
                SpellSlot = s.Slot,
                ToggleValue = s.Toggle
            };

            _packetHandlerManager.SendPacket(userId, spellTogglePacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing a debug message.
        /// </summary>
        /// <param name="message">Debug message to send.</param>
        /// <param name="sourceNetId"></param>
        public void NotifyS2C_SystemMessage(string message, uint sourceNetId = 0)
        {
            var dm = new S2C_SystemMessage
            {
                SourceNetID = sourceNetId,
                //TODO: Ivestigate the cases where SenderNetID is used
                Message = message
            };
            _packetHandlerManager.BroadcastPacket(dm, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing a debug message.
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="message">Debug message to send.</param>
        /// <param name="sourceNetId"></param>
        public void NotifyS2C_SystemMessage(int userId, string message, uint sourceNetId = 0)
        {
            var dm = new S2C_SystemMessage
            {
                SourceNetID = sourceNetId,
                //TODO: Ivestigate the cases where SenderNetID is used
                Message = message
            };
            _packetHandlerManager.SendPacket(userId, dm, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified team detailing a debug message.
        /// </summary>
        /// <param name="team">TeamId to send the packet to; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="message">Debug message to send.</param>
        /// <param name="sourceNetId"></param>
        public void NotifyS2C_SystemMessage(TeamId team, string message, uint sourceNetId = 0)
        {
            var dm = new S2C_SystemMessage
            {
                SourceNetID = sourceNetId,
                //TODO: Ivestigate the cases where SenderNetID is used
                Message = message
            };
            _packetHandlerManager.BroadcastPacketTeam(team, dm, Channel.CHL_S2C);
        }

        public void NotifyS2C_UnitSetMinimapIcon(int userId, AttackableUnit unit, bool changeIcon, bool changeBorder)
        {
            var packet = new S2C_UnitSetMinimapIcon
            {
                UnitNetID = unit.NetId,
                ChangeIcon = changeIcon,
                IconCategory = "",
                ChangeBorder = changeBorder,
                BorderCategory = "",
                BorderScriptName = ""
            };
            if (changeIcon)
            {
                packet.IconCategory = unit.IconInfo.IconCategory;
            }
            if (changeBorder)
            {
                packet.BorderCategory = unit.IconInfo.BorderCategory;
                packet.BorderScriptName = unit.IconInfo.BorderScriptName;
            }
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified unit's team has been set.
        /// </summary>
        /// <param name="unit">AttackableUnit who's team has been set.</param>
        public void NotifySetTeam(AttackableUnit unit)
        {
            //TODO: Provide support for more than 2 teams
            var p = new S2C_UnitChangeTeam
            {
                SenderNetID = unit.NetId,
                UnitNetID = unit.NetId,
                TeamID = (uint)unit.Team // TODO: Verify if TeamID is actually supposed to be a uint
            };
            _packetHandlerManager.BroadcastPacket(p, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the spawning (of champions & buildings) that occurs at the start of the game has ended.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        public void NotifySpawnEnd(int userId)
        {
            var endSpawnPacket = new S2C_EndSpawn();
            _packetHandlerManager.SendPacket(userId, endSpawnPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified Champion detailing that the Champion's items have been swapped.
        /// </summary>
        /// <param name="c">Champion who swapped their items.</param>
        /// <param name="fromSlot">Slot the item was previously in.</param>
        /// <param name="toSlot">Slot the item was swapped to.</param>
        public void NotifySwapItemAns(Champion c, int fromSlot, int toSlot)
        {
            //TODO: reorganize in alphabetic order
            var swapItem = new SwapItemAns
            {
                SenderNetID = c.NetId,
                Source = (byte)fromSlot,
                Destination = (byte)toSlot
            };
            VisionService.BroadCastVisionPacket(c, swapItem);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the amount of time since the game started (in seconds). Used to initialize the user's in-game timer.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="time">Time since the game started (in milliseconds).</param>
        public void NotifySyncMissionStartTimeS2C(int userId, float time)
        {
            var sync = new SyncMissionStartTimeS2C()
            {
                StartTime = time / 1000.0f
            };

            _packetHandlerManager.SendPacket(userId, sync, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the amount of time since the game started (in seconds).
        /// </summary>
        /// <param name="gameTime">Time since the game started (in milliseconds).</param>
        public void NotifySynchSimTimeS2C(float gameTime)
        {
            var sync = new SynchSimTimeS2C()
            {
                SynchTime = gameTime / 1000.0f
            };

            _packetHandlerManager.BroadcastPacket(sync, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the amount of time since the game started (in seconds).
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="time">Time since the game started (in milliseconds).</param>
        public void NotifySynchSimTimeS2C(int userId, float time)
        {
            var sync = new SynchSimTimeS2C()
            {
                SynchTime = time / 1000.0f
            };

            _packetHandlerManager.SendPacket(userId, sync, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the results of server's the version and game info check for the specified player.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="players">List of ClientInfo of all players set to connect to the game.</param>
        /// <param name="version">Version of the player being checked.</param>
        /// <param name="gameMode">String of the internal name of the gamemode being played.</param>
        /// <param name="mapId">ID of the map being played.</param>
        public void NotifySynchVersion(int userId, TeamId team, List<ClientInfo> players, string version, string gameMode, GameFeatures gameFeatures, int mapId, string[] mutators)
        {
            var syncVersion = new SynchVersionS2C
            {
                // TODO: Unhardcode all booleans below
                VersionMatches = true,
                // Logs match to file.
                WriteToClientFile = false,
                // Whether or not this game is considered a match.
                MatchedGame = true,
                // Unknown
                DradisInit = false,

                MapToLoad = mapId,
                VersionString = version,
                MapMode = gameMode,
                // TODO: Unhardcode all below
                PlatformID = "NA1",
                MutatorsNum = 0,
                OrderRankedTeamName = "",
                OrderRankedTeamTag = "",
                ChaosRankedTeamName = "",
                ChaosRankedTeamTag = "",
                // site.com
                MetricsServerWebAddress = "",
                // /messages
                MetricsServerWebPath = "",
                // 80
                MetricsServerPort = 0,
                // site.com
                DradisProdAddress = "",
                // /messages
                DradisProdResource = "",
                // 80
                DradisProdPort = 0,
                // test-lb-#.us-west-#.elb.someaws.com
                DradisTestAddress = "",
                // /messages
                DradisTestResource = "",
                // 80
                DradisTestPort = 0,
                // TODO: Create a new TipConfig class and use it here (basically, unhardcode this).
                TipConfig = new TipConfig
                {
                    TipID = 0,
                    ColorID = 0,
                    DurationID = 0,
                    Flags = 3
                },
                GameFeatures = (ulong)gameFeatures,
            };

            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                var info = new PlayerLoadInfo
                {
                    PlayerID = player.PlayerId,
                    // TODO: Change to players[i].Item2.SummonerLevel
                    SummonorLevel = 1,
                    SummonorSpell1 = HashString(player.SummonerSkills[0]),
                    SummonorSpell2 = HashString(player.SummonerSkills[1]),
                    // TODO
                    Bitfield = 0,
                    TeamId = (uint)player.Team,
                    BotName = "",
                    BotSkinName = "",
                    EloRanking = player.Rank,
                    BotSkinID = 0,
                    BotDifficulty = 0,
                    ProfileIconId = player.Icon,
                    // TODO: Unhardcode these two.
                    AllyBadgeID = 0,
                    EnemyBadgeID = 0
                };

                if (player.Champion.IsBot)
                {
                    info.Bitfield |= 1;
                    //TODO: Fix the display of summoner spells
                    info.BotName = player.Champion.Model;
                    info.BotSkinName = player.Champion.Model;
                    info.BotSkinID = player.Champion.SkinID;
                    //info.BotDifficulty = player.BotDifficulty;
                }

                syncVersion.PlayerInfo[i] = info;
            }

            byte mutatorCount = 0;
            for (byte i = 0; i < mutators.Length && i < 8; i++)
            {
                syncVersion.Mutators[mutators[i] is null ? mutatorCount : mutatorCount++] = mutators[i];
            }
            syncVersion.MutatorsNum = mutatorCount;

            // TODO: syncVersion.Mutators

            // TODO: syncVersion.DisabledItems

            // TODO: syncVersion.EnabledDradisMessages

            _packetHandlerManager.SendPacket(userId, syncVersion, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the status (results) of a surrender vote that was called for and ended.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="surrendererTeam">TeamId that called for the surrender vote; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="reason">SurrenderReason of why the vote ended.</param>
        /// <param name="yesVotes">Number of votes for the surrender.</param>
        /// <param name="noVotes">Number of votes against the surrender.</param>
        public void NotifyTeamSurrenderStatus(int userId, TeamId userTeam, TeamId surrendererTeam, SurrenderReason reason, int yesVotes, int noVotes)
        {
            var surrenderStatus = new S2C_TeamSurrenderStatus()
            {
                SurrenderReason = (uint)reason,
                ForVote = (byte)yesVotes,
                AgainstVote = (byte)noVotes,
                TeamID = (uint)surrendererTeam,
            };
            _packetHandlerManager.SendPacket(userId, surrenderStatus, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players on the same team as the Champion that made the surrender vote detailing what vote was made.
        /// </summary>
        /// <param name="starter">Champion that made the surrender vote.</param>
        /// <param name="open">Whether or not to automatically open the surrender voting menu.</param>
        /// <param name="votedYes">Whether or not voting for the surrender is still available.</param>
        /// <param name="yesVotes">Number of players currently for the surrender.</param>
        /// <param name="noVotes">Number of players currently against the surrender.</param>
        /// <param name="maxVotes">Maximum number of votes possible.</param>
        /// <param name="timeOut">Time until voting becomes unavailable.</param>
        public void NotifyTeamSurrenderVote(Champion starter, bool open, bool votedYes, int yesVotes, int noVotes, int maxVotes, float timeOut)
        {
            var surrender = new S2C_TeamSurrenderVote()
            {
                PlayerNetID = starter.NetId,
                OpenVoteMenu = open,
                VoteYes = votedYes,
                ForVote = (byte)yesVotes,
                AgainstVote = (byte)noVotes,
                NumPlayers = (byte)maxVotes,
                TeamID = (uint)starter.Team,
                TimeOut = timeOut,
            };
            _packetHandlerManager.BroadcastPacketTeam(starter.Team, surrender, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that their screen's tint is shifting to the specified color.
        /// </summary>
        /// <param name="team">TeamID to apply the tint to.</param>
        /// <param name="enable">Whether or not to fade in the tint.</param>
        /// <param name="speed">Amount of time that should pass before tint is fully applied.</param>
        /// <param name="color">Color of the tint.</param>
        public void NotifyTint(TeamId team, bool enable, float speed, float maxWeight, Color color)
        {
            var c = new LeaguePackets.Game.Common.Color
            {
                Blue = color.B,
                Green = color.G,
                Red = color.R,
                Alpha = color.A
            };
            //TODO: Provide support for more than 2 teams
            var tint = new S2C_ColorRemapFX
            {
                IsFadingIn = enable,
                FadeTime = speed,
                TeamID = (uint)team,
                Color = c,
                MaxWeight = maxWeight
            };
            _packetHandlerManager.BroadcastPacket(tint, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to a specific player detailing that their screen's tint is shifting to the specified color.
        /// </summary>
        /// <param name="champ">champion to apply the tint to.</param>
        /// <param name="enable">Whether or not to fade in the tint.</param>
        /// <param name="speed">Amount of time that should pass before tint is fully applied.</param>
        /// <param name="color">Color of the tint.</param>
        public void NotifyTintPlayer(Champion champ, bool enable, float speed, Color color)
        {
            var c = new LeaguePackets.Game.Common.Color
            {
                Blue = color.B,
                Green = color.G,
                Red = color.R,
                Alpha = color.A
            };

            var tint = new S2C_ColorRemapFX
            {
                IsFadingIn = enable,
                FadeTime = speed,
                TeamID = (uint)champ.Team.FromTeamId(),
                Color = c,
                MaxWeight = (c.Alpha / 255.0f)
            };
            _packetHandlerManager.SendPacket(champ.ClientId, tint, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that the specified Champion has gained the specified amount of experience.
        /// </summary>
        /// <param name="champion">Champion that gained the experience.</param>
        /// <param name="experience">Amount of experience gained.</param>
        /// TODO: Verify if sending to all players is correct.
        public void NotifyUnitAddEXP(Champion champion, float experience)
        {
            var xp = new UnitAddEXP
            {
                TargetNetID = champion.NetId,
                ExpAmmount = experience
            };
            // TODO: Verify if we should change to BroadcastPacketVision
            _packetHandlerManager.BroadcastPacket(xp, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that the specified Champion has killed a specified player and received a specified amount of gold.
        /// </summary>
        /// <param name="c">Champion that killed a unit.</param>
        /// <param name="died">AttackableUnit that died to the Champion.</param>
        /// <param name="gold">Amount of gold the Champion gained for the kill.</param>
        /// TODO: Only use BroadcastPacket when the unit that died is a Champion.
        public void NotifyUnitAddGold(Champion target, GameObject source, float gold)
        {
            // TODO: Verify if this handles self-gold properly.
            var ag = new UnitAddGold
            {
                SenderNetID = source.NetId,
                TargetNetID = target.GoldOwner.Owner.NetId,
                SourceNetID = source.NetId,
                GoldAmmount = gold
            };
            _packetHandlerManager.SendPacket(target.ClientId, ag, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to optionally all players (given isGlobal), a specified user that is the source of damage, or a specified user that is receiving the damage. The packet details an instance of damage being applied to a unit by another unit.
        /// </summary>
        /// <param name="isGlobal">Whether or not the packet should be sent to all players.</param>
        /// <param name="sourceId">ID of the user who dealt the damage that should receive the packet.</param>
        /// <param name="targetId">ID of the user who is taking the damage that should receive the packet.</param>
        public void NotifyUnitApplyDamage(DamageData damageData, bool isGlobal = true)
        {
            var damagePacket = new UnitApplyDamage
            {
                DamageResultType = (byte)damageData.DamageResultType,
                DamageType = (byte)damageData.DamageType,
                TargetNetID = damageData.Target.NetId,
                SourceNetID = damageData.Attacker.NetId,
                Damage = MathF.Ceiling(damageData.Damage),
                //Sender isn't always the unit itself, sometimes missiles
                SenderNetID = damageData.Target.NetId
            };

            if (isGlobal)
            {
                _packetHandlerManager.BroadcastPacket(damagePacket, Channel.CHL_S2C);
            }
            else
            {
                // todo: check if damage dealt by disconnected players cause anything bad
                if (damageData.Attacker is Champion attackerChamp)
                {
                    _packetHandlerManager.SendPacket(attackerChamp.ClientId, damagePacket, Channel.CHL_S2C);
                }
                else if (damageData.Attacker is Pet pet && pet.Owner is Champion ch)
                {
                    _packetHandlerManager.SendPacket(ch.ClientId, damagePacket, Channel.CHL_S2C);
                }

                if (damageData.Target is Champion targetChamp)
                {
                    _packetHandlerManager.SendPacket(targetChamp.ClientId, damagePacket, Channel.CHL_S2C);
                }
            }
        }

        public void NotifyUpdateLevelPropS2C(UpdateLevelPropData propData)
        {
            var packet = new UpdateLevelPropS2C
            {
                UpdateLevelPropData = propData
            };
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }


        /// <summary>
        /// Sends a notification that the object has entered the team's scope and fully synchronizes its state.
        /// </summary>
        public void NotifyEnterTeamVision(AttackableUnit u, int userId = -1, GamePacket? spawnPacket = null)
        {
            if (spawnPacket is not OnEnterVisibilityClient visibilityPacket)
            {
                List<GamePacket>? packets = null;
                if (spawnPacket != null)
                {
                    packets = new List<GamePacket>(1) { spawnPacket };
                }
                visibilityPacket = VisionService.ConstructEnterVisibilityClientPacket(u, packets);
            }

            OnEnterLocalVisibilityClient healthbarPacket = new()
            {
                SenderNetID = u.NetId,
                MaxHealth = u.Stats.HealthPoints.Total,
                Health = u.Stats.CurrentHealth,
            };

            _packetHandlerManager.SendPacket(userId, visibilityPacket, Channel.CHL_S2C);
            _packetHandlerManager.SendPacket(userId, healthbarPacket, Channel.CHL_S2C);

            if (u.Replication is not null)
            {
                //TODO: try to include it to packets too?
                //TODO: hold until replication?
                OnReplication us = new()
                {
                    SyncID = (uint)Environment.TickCount,
                    ReplicationData = new List<ReplicationData>(1)
                    {
                        u.Replication.GetData(false)
                    }
                };
                _packetHandlerManager.SendPacket(userId, us, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Creates a package and puts it in the queue that will be emptied with the NotifyWaypointGroup call.
        /// </summary>
        /// <param name="u">AttackableUnit that is moving.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="useTeleportID">Whether or not to teleport the unit to its current position in its path.</param>
        public void HoldMovementDataUntilWaypointGroupNotification(AttackableUnit u, int userId, bool useTeleportID = false)
        {
            var data = PacketExtensions.CreateMovementDataNormal(u, Game.Map.NavigationGrid, useTeleportID);

            List<MovementDataNormal> list = null;
            if (!_heldMovementData.TryGetValue(userId, out list))
            {
                _heldMovementData[userId] = list = [];
            }
            list.Add(data);
        }

        /// <summary>
        /// Sends all packets queued by HoldMovementDataUntilWaypointGroupNotification and clears queue.
        /// </summary>
        public void NotifyWaypointGroup()
        {
            foreach (var kv in _heldMovementData)
            {
                int userId = kv.Key;
                var list = kv.Value;

                if (list.Count > 0)
                {
                    var packet = new WaypointGroup
                    {
                        SyncID = Environment.TickCount,
                        Movements = list
                    };

                    _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_LOW_PRIORITY);

                    list.Clear();
                }
            }
        }

        /// <summary>
        /// Creates a package and puts it in the queue that will be emptied with the NotifyOnReplication call.
        /// </summary>
        /// <param name="u">Unit who's stats have been updated.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="partial">Whether or not the packet should only include stats marked as changed.</param>
        public void HoldReplicationDataUntilOnReplicationNotification(AttackableUnit u, int userId, bool partial = true)
        {
            var data = u.Replication.GetData(partial);

            List<ReplicationData> list = null;
            if (!_heldReplicationData.TryGetValue(userId, out list))
            {
                _heldReplicationData[userId] = list = [];
            }
            list.Add(data);
        }

        /// <summary>
        /// Sends all packets queued by HoldReplicationDataUntilOnReplicationNotification and clears queue.
        /// </summary>
        public void NotifyOnReplication()
        {
            foreach (var kv in _heldReplicationData)
            {
                int userId = kv.Key;
                var list = kv.Value;

                if (list.Count > 0)
                {
                    var packet = new OnReplication()
                    {
                        SyncID = (uint)Environment.TickCount,
                        ReplicationData = list
                    };

                    _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_LOW_PRIORITY, PacketFlags.UNSEQUENCED);

                    list.Clear();
                }
            }
        }

        /// <summary>
        /// Sends a packet to all players that have vision of the specified unit.
        /// The packet details a group of waypoints with speed parameters which determine what kind of movement will be done to reach the waypoints, or optionally a GameObject.
        /// Functionally referred to as a dash in-game.
        /// </summary>
        /// <param name="u">Unit that is dashing.</param>
        /// TODO: Implement ForceMovement class which houses these parameters, then have that as the only parameter to this function (and other Dash-based functions).
        public void NotifyWaypointGroupWithSpeed(AttackableUnit u)
        {
            // TODO: Implement Dash class and house a List of these with waypoints.
            var md = PacketExtensions.CreateMovementDataWithSpeed(u, Game.Map.NavigationGrid);

            var speedWpGroup = new WaypointGroupWithSpeed
            {
                SyncID = Environment.TickCount,
                // TOOD: Implement support for multiple speed-based movements (functionally known as dashes).
                Movements = [md]
            };

            VisionService.BroadCastVisionPacket(u, speedWpGroup);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that their request to view something with their camera has been acknowledged.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="request">ViewRequest housing information about the camera's view.</param>
        /// TODO: Verify if this is the correct implementation.
        public void NotifyWorld_SendCamera_Server_Acknologment(ClientInfo client, World_SendCamera_Server request)
        {
            var answer = new World_SendCamera_Server_Acknologment
            {
                //TODO: Check these values
                SenderNetID = client.Champion.NetId,
                SyncID = request.SyncID,
            };
            _packetHandlerManager.SendPacket(client.ClientId, answer, Channel.CHL_S2C, PacketFlags.NONE);
        }
        /// <summary>
        /// Sends a packet that an unit's health bar has been hidden/shown
        /// Upon testing, I found that only the 'show' matters or we are not sending it properly.
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="show">To show the health bar or not</param>
        /// <param name="observerTeamId">Which team should see the changes</param>
        /// <param name="changeHealthBarType">Whether to change the health bar type</param>
        /// <param name="healthBarType">The type of health bar to show/hide</param>
        public void NotifyS2C_ShowHealthBar(AttackableUnit unit, bool show, TeamId observerTeamId = TeamId.TEAM_UNKNOWN, bool changeHealthBarType = false, HealthBarType healthBarType = HealthBarType.Invalid)
        {
            var showHealthBar = new S2C_ShowHealthBar
            {
                ShowHealthBar = show,
                SenderNetID = unit.NetId,
                ObserverTeamID = (uint)observerTeamId,
                ChangeHealthBarType = changeHealthBarType,
                HealthBarType = (byte)healthBarType
            };
            VisionService.BroadCastVisionPacket(unit, showHealthBar);
        }

        /// <summary>
        /// Sends a packet to the the unit that an item has ben successfully used
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="slot">The slot of the iem that has been used</param>
        /// <param name="itemsInSlot">How many stacks of the item remain</param>
        /// <param name="spellCharges">How many spell charges of the item are left</param>
        public void NotifyUseItemAns(ObjAIBase unit, byte slot, int itemsInSlot, int spellCharges = 0)
        {
            var useItemAns = new UseItemAns
            {
                SenderNetID = unit.NetId,
                Slot = slot,
                ItemsInSlot = (byte)itemsInSlot,
                SpellCharges = (byte)spellCharges
            };
            VisionService.BroadCastVisionPacket(unit, useItemAns);
        }

        public void NotifyS2C_UnitSetPARType(AttackableUnit owner, byte pARType)
        {
            var PARPacket = new S2C_UnitSetPARType
            {
                SenderNetID = owner.NetId,
                PARType = pARType
            };
            _packetHandlerManager.BroadcastPacket(PARPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_DisplayLocalizedTutorialChatText(int clientId, string message)
        {
            var pkt = new S2C_DisplayLocalizedTutorialChatText()
            {
                Message = message
            };
            _packetHandlerManager.SendPacket(clientId, pkt, Channel.CHL_S2C);
        }

        public void NotifyS2C_OpenTutorialPopup(int clientId, string message)
        {
            var pkt = new S2C_OpenTutorialPopup()
            {
                MessageboxID = message
            };
            _packetHandlerManager.SendPacket(clientId, pkt, Channel.CHL_S2C);
        }

        public void NotifyS2C_ShowObjectiveText(int clientId, string message)
        {
            var pkt = new S2C_ShowObjectiveText()
            {
                Message = message
            };
            _packetHandlerManager.SendPacket(clientId, pkt, Channel.CHL_S2C);
        }

        public void NotifyS2C_ReplaceObjectiveText(int clientId, string message)
        {
            var pkt = new S2C_ReplaceObjectiveText()
            {
                TextID = message
            };
            _packetHandlerManager.SendPacket(clientId, pkt, Channel.CHL_S2C);
        }

        public void NotifyS2C_HideObjectiveText(int clientId)
        {
            _packetHandlerManager.SendPacket(clientId, new S2C_DisplayLocalizedTutorialChatText(), Channel.CHL_S2C);
        }

        public void NotifyS2C_ShowAuxiliaryText(int clientId, string message)
        {
            var pkt = new S2C_ShowAuxiliaryText()
            {
                MessageID = message
            };
            _packetHandlerManager.SendPacket(clientId, pkt, Channel.CHL_S2C);
        }

        public void NotifyS2C_RefreshAuxiliaryText(int clientId, string message)
        {
            var pkt = new S2C_RefreshAuxiliaryText()
            {
                MessageID = message
            };
            _packetHandlerManager.SendPacket(clientId, pkt, Channel.CHL_S2C);
        }

        public void NotifyS2C_HideAuxiliaryText(int clientId)
        {
            _packetHandlerManager.SendPacket(clientId, new S2C_HideAuxiliaryText(), Channel.CHL_S2C);
        }

        public void NotifyCreateFollowerObject
        (
            string characterName,
            string internalName,
            int skinId,
            uint masterObjectNetId,
            uint senderNetId,
            byte netNodeId = 64
        )
        {
            var pkt = new S2C_CreateFollowerObject()
            {
                NetID = masterObjectNetId,
                SkinID = skinId,
                NetNodeID = netNodeId,
                SenderNetID = senderNetId,
                InternalName = internalName,
                CharacterName = characterName
            };

            _packetHandlerManager.BroadcastPacket(pkt, Channel.CHL_S2C);
        }

        public void NotifyReattachFollowerObject(uint newOwnerNetId, uint senderNetId)
        {
            var reattachFollower = new S2C_ReattachFollowerObject()
            {
                NewOwnerId = newOwnerNetId,
                SenderNetID = senderNetId
            };
            _packetHandlerManager.BroadcastPacket(reattachFollower, Channel.CHL_S2C);
        }
    }
}
