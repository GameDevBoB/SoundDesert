using UnityEngine;
using System.Collections;

public class SemeObj : MonoBehaviour
{
    private Animator myAnim;

    private int requiredButtonNumber;
    private int pressedButtonsNumber;

    void Awake()
    {
        myAnim = GetComponent<Animator>();
        requiredButtonNumber = 0;
    }

    // Use this for initialization
    void Start()
    {
        pressedButtonsNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        pressedButtonsNumber++;
        if ((pressedButtonsNumber == requiredButtonNumber))
        {
            myAnim.SetTrigger("Bloom");
            Invoke("LoadCredits", 2);
        }
    }

    public void Close()
    {
        pressedButtonsNumber--;
    }

    public void AddRequiredButton()
    {
        requiredButtonNumber++;
        //Debug.Log(requiredButtonNumber);
    }

    private void LoadCredits()
    {
        Application.LoadLevel(5);
    }
}
