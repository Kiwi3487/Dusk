using UnityEngine;

public class LensFlareController : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 20f;
    public Light directLight;

    private void Update()
    {
        if (player == null || directLight == null) return;

        // Point the light toward the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.forward = directionToPlayer;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        directLight.enabled = distanceToPlayer <= activationDistance;
    }
}