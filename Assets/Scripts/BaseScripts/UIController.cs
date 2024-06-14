using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int blueFollowing = 0;
    public int bluePlaced = 0;

    [SerializeField] private GameObject abilityPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera;

    private string[] tutorialTexts = new string[]
    {
        "Move with [WASD]",
        "Press [SPACE] to CALL Villagers in range and ADD THEM TO YOUR TEAM",
        "Press <color=red>[R]</color> to TOGGLE <color=red>RED</color> Villagers",
        "While <color=red>RED</color> is TOGGLED: \n  [Click] to move ONE <color=red>RED</color> Villager",
        "While <color=red>RED</color> is TOGGLED: \n  Press [SPACE] to call all <color=red>RED</color> Villagers back",
        "Press <color=red>[R]</color> to TOGGLE <color=red>RED</color> Villagers\nWhile <color=red>RED</color> is TOGGLED: \n  [Click] to move ONE <color=red>RED</color> Villager " +
        "\n  Press [SPACE] to call all <color=red>RED</color> Villagers back",

        "\"Press <color=red>[R]</color> to TOGGLE <color=red>RED</color> Villagers" +
        "\nWhile <color=red>RED</color> is TOGGLED: \n  " +
        "[Click] to move ONE <color=red>RED</color> Villager \n  " +
        "Press [SPACE] to call all <color=red>RED</color> Villagers back" +
        "Press <color=blue>[B]</color> to TOGGLE <color=blue>BLUE</color> Villagers\n" +
        "While <color=blue>BLUE</color> is TOGGLED: \n  [Click] to move ONE <color=blue>blue</color> Villager " +
        "\n  Press [SPACE] to call all <color=blue>BLUE</color> Villagers back.",


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
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(ShowBubble("I need to get up there somehow..."));
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            tutorialTextsIndex = 5;
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            StartCoroutine(ShowBubble("I must get water to those light patches of dirt..."));
            tutorialTextsIndex = 6;
        }


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
                if (!abilityPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
                    tutorialTextsIndex++;
                break;

            case 2:
                if (!abilityPanel.activeSelf && Input.GetKey(KeyCode.R))
                    tutorialTextsIndex++;
                break;

            case 3:
                if (abilityPanel.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
                    tutorialTextsIndex++;
                break;

            case 4:
                if (abilityPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialTextsIndex++;

                    StartCoroutine(ShowBubble("I can Exit through that Archeway..."));

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
        if (abilityPanel.activeSelf)
        {
            Debug.Log("Panel off");
            abilityPanel.SetActive(false);
            return false;
        }
        else
        {
            Debug.Log("Panel on");
            abilityPanel.GetComponent<Image>().color = new Color(1, 0, 0.04237556f, 0.07058824f);
            abilityPanel.SetActive(true);
            return true;
        }

    }


    public bool toggleBlue()
    {
        if (abilityPanel.activeSelf)
        {
            Debug.Log("Panel off");
            abilityPanel.SetActive(false);
            return false;
        }
        else
        {
            Debug.Log("Panel on");
            abilityPanel.GetComponent<Image>().color = new Color(0, 0.9718385f, 1, 0.1686275f);
            abilityPanel.SetActive(true);
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


        teamString += "\n<color=blue>B:</color> (<color=blue>";
        for (int i = 0; i < blueFollowing; i++)
        {
            teamString += "\u25A0";
        }
        teamString += "</color>) | <color=blue>";
        for (int i = 0; i < bluePlaced; i++)
        {
            teamString += "\u25A0";
        }
        teamString += "</color>";


        teamTextMesh.text = teamString;

    }

    public void AddToFollowing(int col)
    {
        if (col == 0)
        {
            redFollowing++;
        }
        else if (col == 1)
        {
            blueFollowing++;
        }

        UpdateTeamString();
    }

    public void AddToPlaced(int col)
    {
        if (col == 0)
        {
            redPlaced++;
        }
        else if (col == 1)
        {
            bluePlaced++;
        }
        UpdateTeamString();
    }

    public void RemoveFromFollowing(int col)
    {
        if (col == 0)
        {
            redFollowing--;
        }
        else if (col == 1)
        {
            blueFollowing--;
        }

        UpdateTeamString();
    }

    public void RemoveFromPlaced(int col)
    {
        if (col == 0)
        {
            redPlaced--;
        }
        else if (col == 1)
        {
            bluePlaced--;
        }

        UpdateTeamString();
    }

    public void EndGame()
    {
        player.SetActive(false);
        dialoguePanel.SetActive(false);
        //callPanel.SetActive(false);
        abilityPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        teamPanel.SetActive(false);
        endGamePanel.SetActive(true);
    }

}
