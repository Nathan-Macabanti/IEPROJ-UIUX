using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private bool SpacePress = false;
    public float ticks;
	private Image image;
	public GameObject title;
	public GameObject Glow;
	private Animator anim;
	void Start()
	{
		image = title.GetComponent<Image>();
		if (Glow== true)
		{
			anim = Glow.GetComponent<Animator>();
		}
		
	}
    public void onPlayPress()
    {
        SceneManager.LoadScene("tutorial_1.0");
    }
    public void onExitPress()
    { 
	    Application.Quit();
    }
    public void onPlayGamePress()
    {
        SceneManager.LoadScene("Prototype");
    }
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Space) && SpacePress == false)
	    {
		    SpacePress = true;
		    image.CrossFadeColor(Color.black, ticks, true, true);
		    StartCoroutine(deactivate(ticks));
	    }
    }

    public IEnumerator deactivate(float t)
    {
	    yield return new WaitForSeconds(t);
		title.SetActive(false);
    }

    public void CursorHoverOnArcade()
    {
	    anim.SetBool("HoveredArcade", true);

	}
    public void CursorHoverOffArcade()
    {
	    anim.SetBool("HoveredArcade", false);

    }
    public void CursorHoverOnVending()
    {
	    anim.SetBool("HoveredVending", true);

    }
    public void CursorHoverOffVending()
    {
	    anim.SetBool("HoveredVending", false);

    }
}