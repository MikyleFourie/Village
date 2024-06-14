using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public bool T1Grown = false;
    public bool T2Grown = false;

    public GameObject ExitArch;

    public Patch1Script dp1;
    public Patch1Script dp2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Tick()
    {
        if (!T1Grown)
        {
            T1Grown = true;
        }
        else
        {
            T2Grown = true;
        }

        if (T1Grown && T2Grown)
        {
            ExitArch.SetActive(true);
        }
    }
}
