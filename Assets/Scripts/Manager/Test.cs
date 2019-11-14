using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float backgoundDislpace = 40.90001f;
    public float backgroundspeed = 0.1f;
    public BackgroundController bg1;
    public BackgroundController bg2;
    BackgroundController currentBG;

    // Use this for initialization
    void Start()
    {
        currentBG = bg1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -backgroundspeed, 0);
    }

    public void RespawnBG(BackgroundController bg)
    {
        if (bg != currentBG)
            return;
        BackgroundController other = (bg == bg1) ? bg2 : bg1;
        bg.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + backgoundDislpace, other.transform.position.z);
        currentBG = other;
    }
}
