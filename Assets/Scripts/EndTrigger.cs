using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private UIController UIController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            UIController.EndGame();

    }
}
