using UnityEngine;
using UnityEngine.UI;

public class BackgroundImage : MonoBehaviour {

    private Sprite spriteToShow;
    private Image displayedImage;
    private Animator animator;

    private void Awake()
    {
        displayedImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void Fade(Sprite sprite)
    {
        spriteToShow = sprite;
        animator.SetBool("Fade", true);
        Debug.Log("Fade");
    }

    public void ChangeImage()
    {
        displayedImage.sprite = spriteToShow;
        animator.SetBool("Fade", false);
        Debug.Log("Change Image");
    }

    

}
