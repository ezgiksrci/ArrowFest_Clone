using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerClone;
    [SerializeField] float radius = 1.0f;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GateController gateController))
        {
            Debug.Log(gateController.gateNumber);
            
            CreateClonesAroundPoint(gateController.gateNumber, transform.position, radius);
            radius++;
        }
    }

    public void CreateClonesAroundPoint(int num, Vector3 point, float radius)
    {

        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var clone = Instantiate(playerClone, spawnPos, Quaternion.identity) as GameObject;

            /* Make the new object child of the Player */
            clone.transform.parent = player.transform;

            /* Rotate the enemy to face towards player */
            clone.transform.LookAt(point);

            /* Adjust height */
            //clone.transform.Translate(new Vector3(0, clone.transform.localScale.y / 2, 0));
        }
    }
}
