using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Sprite UISprite;
    private UnityEngine.UI.Image interactIcon;
    private UnityEngine.UI.Image noteImage;
    bool nearby;

    private void Start()
    {
        interactIcon = GameObject.Find("InteractIcon").GetComponent<UnityEngine.UI.Image>();
        noteImage = GameObject.Find("NoteImage").GetComponent<UnityEngine.UI.Image>();
        nearby = false;
    }
    private void Update()
    {
        if (nearby && Input.GetKeyDown("e"))
        {
            noteImage.enabled = !noteImage.enabled;
            noteImage.sprite = UISprite;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearby = true;
            interactIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactIcon.enabled = false;
            noteImage.enabled = false;
            nearby = false;
        }
    }
}
