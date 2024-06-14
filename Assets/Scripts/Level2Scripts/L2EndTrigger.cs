using UnityEngine;

public class L2EndTrigger : MonoBehaviour
{
    [SerializeField] private UIController2 UIController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            UIController.EndGame();

    }
}
