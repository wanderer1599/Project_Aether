using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue_System : MonoBehaviour
{
    [Header("Dialogue Box Settings")]
    public GameObject characterDialogueBox, regularDialogueBox, mainDialogueBox;

    [Header("Text settings")]
    public Text characterWritingText;
    public Text regularWritingText;
    private Text mainWritingText;
    public Text charactersName;

    [Header("Text GameObject")]
    public GameObject characterTextGO;
    public GameObject regularTextGO;
    private GameObject mainTextGO;
    
    public GameObject charactersImage;

    [Header("Sprite Images")]
    public Sprite character1;
    public Sprite character2;

    [Header("State Settings")]
    public State state;
    public State currentState;
    private string storyInstance;

    private List<string> fullStoryInstance = new List<string>();
    private int fullStoryInstanceTracker = 0;
    private bool haveMoreText = false;

    [SerializeField]
    public float letterWidth, letterHeight;

    public GameObject button;

    private int howManyButtonsFit;

    private List<GameObject> buttonsList = new List<GameObject>();

    public GameObject cursor = null;

    private int pages, currentPage = 1;

    private int cursorPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        DialogueBoxSetText(state, fullStoryInstanceTracker);
        cursorPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
        Scroll();
        if (Input.GetKeyDown(KeyCode.Return) && currentState.options.Length > 0) NextState(currentState.stateOptions[((currentPage - 1) * howManyButtonsFit) + cursorPosition]);
        if (Input.GetKeyDown(KeyCode.Space) && currentState.options.Length == 0) NextState(currentState.nextState);
    }

    public void DialogueBoxSetText(State S, int tracker)
    {
        currentState = S;
        SetTextDialogueBox();
        storyInstance = S.storyText;
        mainWritingText.text = GetStoryInstance(S, tracker);
        mainDialogueBox.SetActive(true);
        charactersName.text = S.getName.ToString();
        SetAvatar(S);
        SetPages();
    }

    public void NextState(State S)
    {
        if (haveMoreText) { mainWritingText.text = GetStoryInstance(currentState, ++fullStoryInstanceTracker); return; }
        else if (S)
        {
            currentState = S;
            SetTextDialogueBox();
            fullStoryInstanceTracker = 0;
            SetPages();
            if (currentState.options.Length == 0)
            {
                mainWritingText.text = GetStoryInstance(currentState, fullStoryInstanceTracker);
                foreach (GameObject Button in GameObject.FindGameObjectsWithTag("button")) Destroy(Button);
                buttonsList.Clear();
                cursor.SetActive(false);
            }
            else
            {
                currentPage = 1;
                RebuildButtonsOnPage(currentPage);
                mainWritingText.text = "";
            }
            SetAvatar(currentState);
            charactersName.text = currentState.getName.ToString();
           }
        else
        {
            regularDialogueBox.SetActive(false);
            characterDialogueBox.SetActive(false);
        }
    }
    public void SetAvatar(State S)
    {
        if (S.getName.ToString() == "Yevgeniy") charactersImage.GetComponent<RawImage>().texture = character1.texture;
        else if (S.getName.ToString() == "Eugene") charactersImage.GetComponent<RawImage>().texture = character2.texture;

    }

    /// <summary>
    /// Input current state, cut down the text string until it fits the determined text,
    ///add the remaining text to the next one and return the current string
    /// </summary>
    /// <param name="S">The state to use</param>
    /// <param name="tracker"></param>
    /// <returns></returns>

    public string GetStoryInstance(State S, int tracker)
    {
        string dummyString = "";
        int whereStop = 0;
        Rect thisTextDimensions = characterWritingText.GetComponent<RectTransform>().rect;
        if (fullStoryInstance.Count == 0) fullStoryInstance.Add(S.storyText);
        for (int i = 0; i <= fullStoryInstance[tracker].Length - 1; i++)
        {
            if (i == 0 && (fullStoryInstance[tracker][i] == ' ' || fullStoryInstance[tracker][i] == '.')) continue;
            dummyString += fullStoryInstance[tracker][i];
            if (fullStoryInstance[tracker][i] == ' ' || fullStoryInstance[tracker][i] == '.' || i == fullStoryInstance[tracker].Length - 1)
            {
                storyInstance = dummyString;
                whereStop = i;
            }
            if (dummyString.Length > (thisTextDimensions.width / letterWidth) * (thisTextDimensions.height / letterHeight))            {
                break;
            }
        }
        string dummy2 = fullStoryInstance[tracker].Remove(0, whereStop);
        if (dummy2.Length > 1) { fullStoryInstance.Add(dummy2); haveMoreText = true; }
        else { haveMoreText = false; fullStoryInstance.Clear(); }
        return storyInstance;
    }

    public void RebuildButtonsOnPage(int page)
    {
        if (currentState.options.Length == 0) return;
        if(howManyButtonsFit == 0) { Debug.LogError("0 buttons can fit"); return; }
        foreach (GameObject Button in GameObject.FindGameObjectsWithTag("button")) Destroy(Button);
        cursor.SetActive(true);
        cursorPosition = 0;
        buttonsList.Clear();
        int howManyButtonsPlace = 0;
        if (page == 1 && currentState.options.Length < howManyButtonsFit) howManyButtonsPlace = currentState.options.Length;
        else if ((page * howManyButtonsFit) > currentState.options.Length) howManyButtonsPlace = currentState.options.Length % howManyButtonsFit;
        else if ((page * howManyButtonsFit) <= currentState.options.Length) howManyButtonsPlace = howManyButtonsFit;
        GameObject prevButton = null;
        for (int i = 1; i <= howManyButtonsPlace; i++)
        {
            GameObject Button = null;
            if (!prevButton)
            {
                Button = Instantiate(button, new Vector2(characterDialogueBox.transform.position.x, characterDialogueBox.transform.position.y + 30f), Quaternion.identity, mainDialogueBox.transform) as GameObject;
            }
            else Button = Instantiate(button, new Vector2(prevButton.transform.position.x, prevButton.transform.position.y - 60f), Quaternion.identity, mainDialogueBox.transform) as GameObject;
            prevButton = Button;
            prevButton.GetComponentInChildren<Text>().text = "Option " + i + ": " + currentState.options[((currentPage - 1) * howManyButtonsFit) + i - 1];
            prevButton.SetActive(true);
            buttonsList.Add(prevButton);
            cursor.transform.position = buttonsList[cursorPosition].transform.position;
        }
    }
    public void SetPages()
    {
        if (currentState.options.Length >= howManyButtonsFit)
        {
            pages = currentState.options.Length / howManyButtonsFit;
            if (currentState.options.Length % howManyButtonsFit != 0) pages++;
        }
    }
    private void ManageState()
    {
        for (int index = 0; index < currentState.options.Length; index++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + index))
            {
                NextState(currentState.stateOptions[index]);
            }
        }
    }
    public void Scroll()
    {
        if (currentState.options.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorPosition++;
                if (cursorPosition > buttonsList.Count - 1)
                {
                    cursorPosition = 0;
                    if (currentPage == pages) currentPage = 1;
                    else currentPage++;
                    RebuildButtonsOnPage(currentPage);
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentState.options.Length > 0)
                {
                    cursorPosition--;
                    if (cursorPosition < 0)
                    {
                        if (currentPage == 1) currentPage = pages;
                        else currentPage--;
                        RebuildButtonsOnPage(currentPage);
                        cursorPosition = buttonsList.Count - 1;
                    }
                }
            }
            cursor.transform.position = buttonsList[cursorPosition].transform.position;
        }
    }
    public void SetTextDialogueBox()
    {
        if (currentState.regular)
        {
            mainDialogueBox = regularDialogueBox;
            mainWritingText = regularWritingText;
        }
        else
        {
            mainDialogueBox = characterDialogueBox;
            mainWritingText = characterWritingText;
        }
        howManyButtonsFit = (int)mainDialogueBox.GetComponent<RectTransform>().rect.height / (int)(button.GetComponent<RectTransform>().rect.height + 10);
        mainDialogueBox.SetActive(true);
    }
}