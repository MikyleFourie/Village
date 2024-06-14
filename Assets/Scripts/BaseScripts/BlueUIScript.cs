using UnityEngine;

public class BlueUIScript : MonoBehaviour
{
    public GameObject waterDropletPrefab; // Assign the water droplet prefab in the Inspector
    private GameObject waterDropletInstance; // Instance of the water droplet UI element
    public VillagerAttributes tempVAscript;

    [SerializeField] Camera mainCamera;
    Vector3 offset;
    void Start()
    {
        offset = new Vector3(0, 1.8f, 0);

        // Instantiate the water droplet UI element
        waterDropletInstance = Instantiate(waterDropletPrefab, transform.position, Quaternion.identity);
        waterDropletInstance.transform.SetParent(GameObject.Find("Canvas1").transform, false); // Ensure it's under the Canvas
        waterDropletInstance.SetActive(false); // Start with it deactivated
    }

    void Update()
    {
        // Example: Check for user input to pick up water (you can adjust this logic)
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    PickUpWater();
        //}

        if (waterDropletInstance != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + offset);
            waterDropletInstance.transform.position = screenPos;
        }


    }

    public void PickUpWater()
    {
        // Example: When picking up water
        if (!waterDropletInstance.activeSelf)
        {
            waterDropletInstance.SetActive(true);
            tempVAscript = GetComponent<VillagerAttributes>();
            tempVAscript.isCarryingWater = true;
        }
    }

    // Example: Function to drop or use water
    public void DropWater()
    {
        if (waterDropletInstance.activeSelf)
        {
            waterDropletInstance.SetActive(false);
        }
    }

    void OnDestroy()
    {
        // Ensure the water droplet UI element is destroyed when the villager is destroyed
        if (waterDropletInstance != null)
        {
            Destroy(waterDropletInstance);
        }
    }
}
