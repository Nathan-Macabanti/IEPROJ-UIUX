using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private bool SpacePress = false;
	public Image image;
	public GameObject title;

	void Start()
	{
		image = title.GetComponent<Image>();
	}
    public void onPlayPress()
    {
        SceneManager.LoadScene("tutorial_1.0");
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
		    image.CrossFadeColor(Color.black, 5.0f, true, true);
		    StartCoroutine(deactivate(5.0f));
	    }
    }

    public IEnumerator deactivate(float t)
    {
	    yield return new WaitForSeconds(t);
		title.SetActive(false);
    }
}