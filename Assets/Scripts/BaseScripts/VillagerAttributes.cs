using UnityEngine;
using UnityEngine.AI;

public class VillagerAttributes : MonoBehaviour
{
    public NavMeshAgent agent;
    // Enum for villager color
    public enum VillagerColor { Red, Blue, Green, Brown }

    // Villager color attribute
    public VillagerColor vCol;

    // Specific attribute for Blue villagers
    public bool isCarryingWater;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (vCol == VillagerColor.Red)
        {
            agent.SetAreaCost(NavMesh.GetAreaFromName("Water"), 1000f);
        }
        else if (vCol == VillagerColor.Blue)
        {
            agent.SetAreaCost(NavMesh.GetAreaFromName("Water"), 1f);
        }
    }
}
