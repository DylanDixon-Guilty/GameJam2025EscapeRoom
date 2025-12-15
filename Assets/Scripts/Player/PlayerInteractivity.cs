using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


public class PlayerInteractivity : MonoBehaviour
{
    public static PlayerInteractivity Instance;
    public GameObject ExitPuzzleText;
    public static bool IsInteracting;
    public float playerReach = 3f; //The distance of which the player can interact with an object

    [SerializeField] private GameObject puzzleCamera;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject timerUI;
    
    private Interactable currentInteractable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ExitPuzzleText.SetActive(false);
        puzzleCamera.SetActive(false);
        timerUI.SetActive(false);
    }

    public void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        CheckInteraction();

        if(Input.GetKeyDown(KeyCode.E) && IsInteracting)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            InteractWithPuzzle();
            TimerManager.Instance.isTimerStart = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ExitPuzzleText.SetActive(false);
            puzzleCamera.SetActive(false);
            timerUI.SetActive(false);
            player.SetActive(true);
        }
    }

    private void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out hit, playerReach))
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if(currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }

                if(newInteractable.enabled && newInteractable != null)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HudManager.Instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable()
    {
        HudManager.Instance.DisableInteractionText();
        if(currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

    public void InteractWithPuzzle()
    {
        interactText.SetActive(false);
        ExitPuzzleText.SetActive(true);
        timerUI.SetActive(true);
        puzzleCamera.SetActive(true);
        player.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!GameManager.Instance.IsPuzzleOver)
        {
            interactText.SetActive(true);
            IsInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.SetActive(false);
        IsInteracting = false;
    }
}
