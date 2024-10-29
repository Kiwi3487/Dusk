using UnityEngine;
using TMPro;
using System.Collections;

public class TextManager : MonoBehaviour
{
    //Script from my other project which allows text to appear and dissappear based on its current state and inputs, this script is hard coded and isnt very flexible 
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    private enum State
    {
        None,
        WASDCompleted,
        SpaceCompleted,
        Text4Displayed,
        Text5Displayed,
    }

    private State currentState = State.None;

    private void Update()
    {
        if (currentState == State.None && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(HandleTextSwitch(text1, text2, State.WASDCompleted));
        }

        if (currentState == State.WASDCompleted && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(HandleTextSwitch(text2, text3, State.SpaceCompleted));
        }

        if (currentState == State.SpaceCompleted && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            StartCoroutine(HandleTextSwitch(text3, text4, State.Text4Displayed));
        }

        if (currentState == State.Text4Displayed)
        {
            StartCoroutine(TransitionToText5AfterDelay());
        }
    }

    private IEnumerator HandleTextSwitch(TMP_Text textToDisable, TMP_Text textToEnable, State nextState)
    {
        if (textToDisable != null)
        {
            textToDisable.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.25f);

        if (textToEnable != null)
        {
            textToEnable.gameObject.SetActive(true);
        }

        currentState = nextState;
    }

    private IEnumerator TransitionToText5AfterDelay()
    {
        currentState = State.Text5Displayed;

        // Wait for 5 seconds before transitioning from text4 to text5
        yield return new WaitForSeconds(5f);

        // Transition from text4 to text5
        StartCoroutine(HandleTextSwitch(text4, text5, State.Text5Displayed));
    }
}
