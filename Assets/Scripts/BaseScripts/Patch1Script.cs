using UnityEngine;

public class Patch1Script : MonoBehaviour
{
    public GameObject temp;
    public VillagerMovement tempVMscript;
    public VillagerAttributes tempVAscript;
    public BlueUIScript tempBlueUIScript;

    [SerializeField] bool isCrop = true;

    [SerializeField] GameObject TreePrefab;

    [SerializeField] Level3Manager level3Manager;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "villager" && isCrop)
        {
            temp = other.gameObject;

            tempVAscript = temp.GetComponentInParent<VillagerAttributes>();
            tempVMscript = temp.GetComponentInParent<VillagerMovement>();
            tempBlueUIScript = temp.GetComponentInParent<BlueUIScript>();

            if (tempVAscript.vCol == VillagerAttributes.VillagerColor.Blue && tempVAscript.isCarryingWater)
            {
                Instantiate(TreePrefab, transform.position, Quaternion.identity);
                isCrop = false;
                tempVAscript.isCarryingWater = false;
                tempBlueUIScript.DropWater();
                level3Manager.Tick();
            }
        }
    }
}
