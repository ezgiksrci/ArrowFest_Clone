using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 0;
   


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * TouchInput.Instance.horizontal * 10 * Time.deltaTime;
        float xPos = Mathf.Clamp(transform.position.x,-2,2);
        transform.position = new Vector3(xPos , transform.position.y, transform.position.z);
        CharacterMove();
    }

    private void CharacterMove()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
