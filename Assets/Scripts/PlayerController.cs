using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerClone;
    [SerializeField] float radius = 1.0f;
    public List<GameObject> cloneList;


    private void Start()
    {
        InvokeRepeating("ScaleAnimation", 0f, 1f);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GateController gateController))
        {
            if (gateController.gateNumber > 0)
            {
                //Debug.Log(gateController.gateNumber);

                CreateClonesAroundPoint(gateController.gateNumber, transform.position, radius);
                radius = radius + 0.5f;
            }
            else
            {
                for (int i = 0; i > gateController.gateNumber; i--)
                {
                    Destroy(cloneList[cloneList.Count - 1].gameObject);
                    cloneList.RemoveAt(cloneList.Count - 1);
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

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var clone = Instantiate(playerClone, spawnPos, Quaternion.identity) as GameObject;
            cloneList.Add(clone);

            /* Make the new object child of the Player */
            clone.transform.parent = player.transform;

            /* Rotate the enemy to face towards player */
            clone.transform.LookAt(point);

            /* Adjust height */
            //clone.transform.Translate(new Vector3(0, clone.transform.localScale.y / 2, 0));
        }
    }

    private void ScaleAnimation()
    {
        transform.DOScale(2, 0.5f).OnComplete(() => transform.DOScale(1, 0.5f));
    }
}
