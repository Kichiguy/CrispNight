using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : MonoBehaviour
{
    public float intensity;
    public float sputterVolatility;
    public float sparkUpToIntensity;

    private Light torch;

    // Update is called once per frame
    void Update()
    {
        torch.intensity = intensity;
    }
}
