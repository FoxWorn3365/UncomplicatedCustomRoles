﻿using Exiled.API.Features;
using System.Collections.Generic;
using System;
using UncomplicatedCustomRoles.Manager;
using UncomplicatedCustomRoles.Structures;
using Handler = UncomplicatedCustomRoles.Events.EventHandler;

using PlayerHandler = Exiled.Events.Handlers.Player;
using ServerHandler = Exiled.Events.Handlers.Server;

// Nella vita troverai sempre qualcuno che ti farà stare bene, qualcuno che ti farà capire che anche tu sai amare, qualcuno che finalmente porta luce nell'ombra della tua vita.
// Poi quella persona inizia a ferire i tuoi sentimenti, ti inizia a far sentire male e poi sparisce nel nulla, lasciandoti cadere nel grande fosso, completamente al buio.
// Va e viene, è una costante che non cambierà mai e che non è destinata a cambiare.
// Godetevi tutti i momenti belli della vostra vita, secondo per secondo, attimo per attimo, millimetro per millimetro perché anche se sembra, non dureranno all'infinito...
// - Fox

namespace UncomplicatedCustomRoles
{
    internal class Plugin : Plugin<Config>
    {
        public override string Name => "UncomplicatedCustomRoles";
        public override string Prefix => "UncomplicatedCustomRoles";
        public override string Author => "FoxWorn3365, Dr.Agenda";
        public override Version Version { get; } = new(1, 5, 0);
        public override Version RequiredExiledVersion { get; } = new(8, 4, 3);
        public static Plugin Instance;
        internal Handler Handler;
        public static Dictionary<int, ICustomRole> CustomRoles;
        public static Dictionary<int, int> PlayerRegistry = new();
        public static Dictionary<int, int> RolesCount = new();
        public static List<int> RoleSpawnQueue = new();
        public bool DoSpawnBasicRoles = false;
        internal FileConfigs FileConfigs;
        public override void OnEnabled()
        {
            Instance = this;

            Handler = new();
            CustomRoles = new();

            FileConfigs = new();

            ServerHandler.RoundStarted += Handler.OnRoundStarted;
            PlayerHandler.Died += Handler.OnDied;
            PlayerHandler.Spawning += Handler.OnSpawning;
            PlayerHandler.Spawned += Handler.OnPlayerSpawned;
            PlayerHandler.Escaping += Handler.OnEscaping;

            foreach (ICustomRole CustomRole in Config.CustomRoles)
            {
                SpawnManager.RegisterCustomSubclass(CustomRole);
            }

            FileConfigs.Welcome();
            FileConfigs.LoadAll();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;

            ServerHandler.RoundStarted -= Handler.OnRoundStarted;
            PlayerHandler.Died -= Handler.OnDied;
            PlayerHandler.Spawning -= Handler.OnSpawning;
            PlayerHandler.Spawned -= Handler.OnPlayerSpawned;
            PlayerHandler.Escaping -= Handler.OnEscaping;

            Handler = null;
            CustomRoles = null;

            base.OnDisabled();
        }
    }
}