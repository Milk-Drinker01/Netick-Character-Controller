using UnityEngine;
using Netick;
using Netick.Unity;

public class FirstPersonEventsHandler : NetworkEventsListener
{
    public Transform SpawnPos;
    public GameObject PlayerPrefab;

    // This is called on the server when a client has connected.
    public override void OnPlayerConnected(NetworkSandbox sandbox, Netick.NetworkPlayer client)
    {
        var spawnPos = SpawnPos.position + Vector3.left * (1 + sandbox.ConnectedPlayers.Count);
        var player = sandbox.NetworkInstantiate(PlayerPrefab, spawnPos, Quaternion.identity, client);
        client.PlayerObject = player.gameObject;
    }
}