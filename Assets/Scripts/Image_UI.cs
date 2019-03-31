using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_UI : MonoBehaviour {

    Image m_Image;
    public Sprite missile;
    public Sprite oil;
    public Car_Controller player;

	// Use this for initialization
	void Start () {
        m_Image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.weapon == 0 && player.hasWeapon)
        {
            m_Image.sprite = missile;
        }
        else if (player.weapon == 1 && player.hasWeapon)
        {
            m_Image.sprite = oil;
        }
        else
        {
            m_Image.sprite = null;
        }
	}
}
