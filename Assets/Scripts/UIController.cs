using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject callPanel;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject redPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject endGamePanel;

    [SerializeField] private GameObject player;
    void Start()
    {
        textMesh = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        dialoguePanel.SetActive(false);

        StartCoroutine(ShowBubble("I need to get through the door up there..."));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ShowBubble(string dialogue)
    {

        //Show panel
        textMesh.text = dialogue;
        dialoguePanel.SetActive(true);


        yield return new WaitForSeconds(4f);

        dialoguePanel.SetActive(false);
        tutorialPanel.SetActive(true);
        //Remove panel
    }

    public bool toggleRed()
    {
        if (redPanel.activeSelf)
        {
            redPanel.SetActive(false);
            return false;
        }
        else
        {
            redPanel.SetActive(true);
            return true;
        }

    }

    public IEnumerator showCall()
    {

        callPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        callPanel.SetActive(false);

    }

    public void EndGame()
    {
        player.SetActive(false);
        dialoguePanel.SetActive(false);
        callPanel.SetActive(false);
        redPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        endGamePanel.SetActive(true);
    }

}
