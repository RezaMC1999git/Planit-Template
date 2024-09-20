using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (otherLayerName == "Player") 
        {
            FollowPoints.instance.playerDetected = true;
            FollowPoints.instance.LookAtPlayer(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string otherLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (otherLayerName == "Player")
        {
            FollowPoints.instance.playerDetected = false;
        }
    }
}
