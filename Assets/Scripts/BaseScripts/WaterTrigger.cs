using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public GameObject temp;
    public VillagerMovement tempVMscript;
    public VillagerAttributes tempVAscript;
    public BlueUIScript tempBlueUIScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "villager")
        {
            temp = other.gameObject;

            tempVAscript = temp.GetComponentInParent<VillagerAttributes>();
            tempVMscript = temp.GetComponentInParent<VillagerMovement>();
            tempBlueUIScript = temp.GetComponentInParent<BlueUIScript>();

            if (tempVAscript.vCol == VillagerAttributes.VillagerColor.Blue && !tempVAscript.isCarryingWater)
            {

                tempVAscript.isCarryingWater = true;
                tempBlueUIScript.PickUpWater();
            }
            Debug.Log("PING!!!!!!!!!!");
        }
    }
}
