using System;
using Fusion;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(HealthChanged))]
    private float NetworkedHealth { get; set; }
    public GameObject team1Spawn, team2Spawn;
    public float currentHealthAmount;
    private const float MaxHealth = 200f;
    public event Action<float> OnDamageEvent;

    private void Start()
    {
        team1Spawn = GameObject.FindGameObjectWithTag("Team1Spawn");
        team2Spawn = GameObject.FindGameObjectWithTag("Team2Spawn");
    }
    public override void Spawned()
    {

        NetworkedHealth = MaxHealth;
    }

    private void Update()
    {
        currentHealthAmount = NetworkedHealth;
    }
    void HealthChanged()
    {
        Debug.Log($"Health changed to: {NetworkedHealth}");
        OnDamageEvent?.Invoke(NetworkedHealth/MaxHealth);
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(float damage)
    {

        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        NetworkedHealth -= damage;
        
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RespawnRpc()
    {

        Debug.Log("Player has been killed!");
        NetworkedHealth = MaxHealth;

    }
    public override void FixedUpdateNetwork()
        
    {

        transform.position = team1Spawn.transform.position;
        if (NetworkedHealth < 0)
        {

            RespawnRpc();
        }
    }

}