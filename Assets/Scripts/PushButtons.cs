using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtons : MonoBehaviour {

    private const string PANEL_NAME = "Panel";
    private const string RESET_NAME = "Reset";
    private const float MALUS_TIME = -30.0f;

    [SerializeField] GameObject[] tSolution;  //sequence solution, const
    [SerializeField] GameObject firstPersonController;
    [SerializeField] GameObject GO_Door;
    [SerializeField] AudioClip[] AC_Buzz;

    private GameObject[] sequence = new GameObject[4];
    private int n = 0;              //n current element of the sequence
    private Interact interact;
    private Animation doorAnimation;
    private AudioSource AS_AudioSource;
    private GameObject target;
    private Timer timer;
    private bool done = false;

    // Use this for initialization
    void Start () {
        interact = firstPersonController.GetComponent<Interact>();
        doorAnimation = GO_Door.GetComponent<Animation>();
        timer = this.GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
        //if the player press 'E' in front of a button
		if (interact.GetInteraction())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                target = interact.GetTarget();
                PlaySound(target);
                if (target.transform.parent.CompareTag(PANEL_NAME) && !done)
                {
                    AddElement(target);
                }
                else if(target.transform.tag == RESET_NAME && !done)
                {
                    CleanSequence();
                }
            }
        }
	}

    //Play music note
    private void PlaySound(GameObject target)
    {
        if (target.GetComponent<AudioSource>() != null)
        {
            AS_AudioSource = target.GetComponent<AudioSource>();
            AS_AudioSource.Play();
        }
    }

    //Add new element to the current sequence
    private void AddElement(GameObject target)
    {
        AS_AudioSource = this.GetComponent<AudioSource>();
        bool result = false;

        if (sequence[n] == null)
        {
            sequence[n] = target;
        }
        n++;
        if (n == sequence.Length)
        {
            result = CheckSequence();
            if (result)
            {
                AS_AudioSource.clip = AC_Buzz[0];
                doorAnimation.Play();              //unlock door
                done = true;
            }
            else
            {
                AS_AudioSource.clip = AC_Buzz[1];
                timer.SetTime(MALUS_TIME);          //apply malus
            }
            AS_AudioSource.Play();
            CleanSequence();
        }
    }

    //Check if the whole sequence is right
    private bool CheckSequence()
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            if (sequence[i] != tSolution[i])
            {
                return false;
            }
        }
        return true;
    }

    //Clean each element of the sequence
    private void CleanSequence()
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            sequence[i] = null;
        }
        n = 0;
    }
}
