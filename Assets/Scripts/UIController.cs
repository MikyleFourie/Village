using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    private TextMeshProUGUI dailogueTextMesh;
    [SerializeField] private GameObject tutorialPanel;
    private TextMeshProUGUI tutorialTextMesh;
    [SerializeField] private GameObject teamPanel;
    private TextMeshProUGUI teamTextMesh;
    private string teamString;
    public int redFollowing = 0;
    public int redPlaced = 0;

    [SerializeField] private GameObject redPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera;

    private string[] tutorialTexts = new string[]
    {
        "Move with [WASD]",
        "Press [SPACE] to CALL Villagers in range and ADD THEM TO YOUR TEAM",
        "Press [R] to TOGGLE RED Villagers",
        "While RED is TOGGLED: \n  [Click] to move ONE red Villager",
        "While RED is TOGGLED: \n  Press [SPACE] to call all Red Villagers back",
        "Press [R] to TOGGLE RED Villagers\nWhile RED is TOGGLED: \n  [Click] to move ONE red Villager \n  Press [SPACE] to call all Red Villagers back"

    };
    private int tutorialTextsIndex = 0;


    Vector3 offset;
    void Start()
    {
        offset = new Vector3(2.5f, 0, 0);

        dailogueTextMesh = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        tutorialTextMesh = tutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
        teamTextMesh = teamPanel.GetComponentInChildren<TextMeshProUGUI>();
        dialoguePanel.SetActive(false);
        tutorialPanel.SetActive(true);

        UpdateTeamString();
    }

    // Update is called once per frame
    void Update()
    {

        if (dialoguePanel != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(player.transform.position + offset);
            dialoguePanel.transform.position = screenPos;
        }


        tutorialTextMesh.text = tutorialTexts[tutorialTextsIndex];
        switch (tutorialTextsIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                    tutorialTextsIndex++;
                break;

            case 1:
                if (!redPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
                    tutorialTextsIndex++;
                break;

            case 2:
                if (!redPanel.activeSelf && Input.GetKey(KeyCode.R))
                    tutorialTextsIndex++;
                break;

            case 3:
                if (redPanel.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
                    tutorialTextsIndex++;
                break;

            case 4:
                if (redPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialTextsIndex++;
                    StartCoroutine(ShowBubble("I need to get through the door up there..."));
                }


                break;

                // Add more cases if you have additional tutorial steps
        }



    }

    public IEnumerator ShowBubble(string dialogue)
    {

        //Show panel
        dailogueTextMesh.text = dialogue;
        dialoguePanel.SetActive(true);


        yield return new WaitForSeconds(4f);

        dialoguePanel.SetActive(false);

        //Remove panel
    }

    public bool toggleRed()
    {
        if (redPanel.activeSelf)
        {
            Debug.Log("Panel off");
            redPanel.SetActive(false);
            return false;
        }
        else
        {
            Debug.Log("Panel on");
            redPanel.SetActive(true);
            return true;
        }

    }

    private void UpdateTeamString()
    {
        teamString = "<color=red>R:</color> (<color=red>";
        for (int i = 0; i < redFollowing; i++)
        {
            teamString += "\u25A0";
        }
        teamString += "</color>) | <color=red>";
        for (int i = 0; i < redPlaced; i++)
        {
            teamString += "\u25A0";
        }
        teamString += "</color>";

        teamTextMesh.text = teamString;

    }

    public void AddToFollowing()
    {
        redFollowing++;
        UpdateTeamString();
    }

    public void AddToPlaced()
    {
        redPlaced++;
        UpdateTeamString();
    }

    public void RemoveFromFollowing()
    {
        redFollowing--;
        UpdateTeamString();
    }

    public void RemoveFromPlaced()
    {
        redPlaced--;
        UpdateTeamString();
    }

    public void EndGame()
    {
        player.SetActive(false);
        dialoguePanel.SetActive(false);
        //callPanel.SetActive(false);
        redPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        teamPanel.SetActive(false);
        endGamePanel.SetActive(true);
    }

}
