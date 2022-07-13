using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerClone;
    [SerializeField] float radius = 1.0f;
    [SerializeField] List<GameObject> cloneList;
    [SerializeField] int score = 0;


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GateController gateController))
        {
            if (gateController.gateNumber > 0)
            {
                //Debug.Log(gateController.gateNumber);

                CreateClonesAroundPoint(gateController.gateNumber, transform.position, radius);
                radius = radius + 0.5f;
                score += gateController.gateNumber;
            }
            else if (gateController.gateNumber < 0)
            {
                for (int i = 0; i > gateController.gateNumber; i--)
                {
                    score--;

                    if (score > -1)
                    {
                        Destroy(cloneList[cloneList.Count - 1].gameObject);
                        cloneList.RemoveAt(cloneList.Count - 1);
                    }
                    else
                    {
                        Debug.Log("Game Over");
                        break;
                    }

                    if (radius >= 1.5f)
                    {
                        radius = radius - 0.5f;
                    }
                    else
                    {
                        radius = 1.0f;
                    }
                }
            }
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

            var spawnDir = new Vector3(vertical, horizontal, 0);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var clone = Instantiate(playerClone, spawnPos, Quaternion.identity) as GameObject;
            cloneList.Add(clone);

            /* Make the new object child of the Player */
            clone.transform.parent = player.transform;

            /* Rotate the enemy to face towards player */
            //clone.transform.LookAt(point);

            /* Random colors */
            clone.GetComponent<MeshRenderer>().material.color = ColorList.colors[Random.Range(0, 12)];

            /* Adjust height */
            //clone.transform.Translate(new Vector3(0, clone.transform.localScale.y / 2, 0));
        }
    }
}
