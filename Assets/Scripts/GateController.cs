using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{
    [SerializeField] Text gateText;
    public int gateNumber;
  

    private void Start()
    {
        if (gateNumber > 0)
        {
            gateText.text = "+" + gateNumber.ToString();
        }
        else
            gateText.text = gateNumber.ToString();
    }
}
