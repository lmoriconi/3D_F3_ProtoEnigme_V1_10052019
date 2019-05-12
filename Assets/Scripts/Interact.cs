using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour {

    [SerializeField] float rayRange;
    [SerializeField] Texture tx_normal;
    [SerializeField] Texture tx_aim;
    [SerializeField] Texture tx_txt_reset;
    [SerializeField] Texture tx_txt_reset_aim;
    private Camera cam;
    private GameObject target;
    private Text hud;
    private bool isInteracting = false;

    void Start () {
        cam = GetComponent<Camera>();
        hud = GetComponentInChildren<Text>();
	}
	
	void Update () {

        // Create a vector at the center of our camera's viewport   
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        // Display the Raycast in the Scene view
        Debug.DrawRay(rayOrigin, cam.transform.forward * rayRange);

        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, rayRange))
        {
            // Check if it's a button collider
            if (hit.collider.GetComponent<Collider>().tag == "Button" || hit.collider.GetComponent<Collider>().tag == "Reset")
            {
                hud.text = "Press \'E\'";
                target = hit.collider.gameObject;
                isInteracting = true;
            }
            else
            {
                hud.text = "";
                isInteracting = false;
            }
        }
        else
        {
            hud.text = "";
            isInteracting = false;
        }
        if (target != null) ChangeColor(isInteracting);
    }

    public bool GetInteraction()
    {
        return isInteracting;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    private void ChangeColor(bool isAimed)
    {
        if (isAimed)
        {
            switch (target.tag)
            {
                case "Button":
                    target.GetComponent<Renderer>().material.mainTexture = tx_aim;
                    break;

                case "Reset":
                    target.GetComponent<Renderer>().material.mainTexture = tx_txt_reset_aim;
                    break;
            }
        }
        else
        {
            switch (target.tag)
            {
                case "Button":
                    target.GetComponent<Renderer>().material.mainTexture = tx_normal;
                    break;

                case "Reset":
                    target.GetComponent<Renderer>().material.mainTexture = tx_txt_reset;
                    break;
            }
        }
    }
}
