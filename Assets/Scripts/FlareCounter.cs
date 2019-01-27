using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlareCounter : MonoBehaviour
{
    private int flareCount;

    public Image flare1;
    public Image flare2;
    public Image flare3;

    // Start is called before the first frame update
    void Start()
    {
        flareCount = 3;
    }

    public void removeFlare()
    {
        switch (flareCount)
        {
            case 3:
                flare1.enabled = false;
                flareCount--;
                break;
            case 2:
                flare2.enabled = false;
                flareCount--;
                break;
            case 1:
                flare3.enabled = false;
                flareCount--;
                break;
            default:
                flareCount--;
                break;
        }
    }
}
