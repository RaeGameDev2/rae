using UnityEngine;

public class TeleportBackground : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetTrigger("open");
    }

    public void ClosePortal()
    {
        anim.SetTrigger("close");
    }

    private void FinishCloseAnimation()
    {
        gameObject.SetActive(false);
    }
}
