using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [Tooltip("Dialogue text")]
    [TextArea(0, 10)]
    public string storyText;
    [Tooltip("The next State to use if there is one")]
    public State nextState;

    public enum CharsName { Yevgeniy, Eugene };
    [Tooltip("The characters variable to use")]
    public CharsName getName;

    [Header("Variables for the 'Options'")]
    public string[] options = new string[0];

    public State[] stateOptions;

    private readonly int pages;

    private int selectedOption = 0;

    private int hasChangedOptions = 0;

    public bool regular = false;

    private void OnValidate()
    {
        if (options.Length >= 0 && hasChangedOptions != options.Length)
        {
            hasChangedOptions = options.Length;
            stateOptions = new State[options.Length];
            storyText = "";
            
        }
        if (selectedOption == 0 && options.Length >= 1) selectedOption = 1;
    }


    public string GetStateStory()
    {
        return storyText;
    }

    public State GetNextStates()
    {
        return nextState;
    }
    
}
