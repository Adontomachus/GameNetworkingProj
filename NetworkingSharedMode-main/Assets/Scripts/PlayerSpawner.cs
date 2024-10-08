using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public Transform spawnPoint1, spawnPoint2;
    int playerSpawned = 0;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            if (playerSpawned % 2 == 0)
            {
                Runner.Spawn(PlayerPrefab, spawnPoint1.position, Quaternion.identity, player);
            }
            else
            {
                Runner.Spawn(PlayerPrefab, spawnPoint2.position, Quaternion.identity, player);
            }
            playerSpawned++;

        }
    }
}
