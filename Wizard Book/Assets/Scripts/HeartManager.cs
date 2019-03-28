using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {

    private int maxHearts = 3;
    public int startHearts = 3;
    private int maxHealth;
    private int healthPerHeart = 2;

    public Image[] healthImages;
    public Sprite[] healthSprites;

    HeroHealth health;
        

	// Use this for initialization
	void Start () {
        health = GetComponent<HeroHealth>();
        maxHealth = maxHearts * healthPerHeart;
        CheckHealthAmount();
	}

    void Update()
    {
        if(health.health > maxHearts * healthPerHeart)
        {
            health.health = maxHearts * healthPerHeart;
        }
    }

    private void CheckHealthAmount()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            if(startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach(Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (health.health >= i * healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - health.health));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }

    }


    public void DamageDude()
    {
        health.health = Mathf.Clamp(health.health, 0, startHearts * healthPerHeart);
        UpdateHearts();
    }

    public void AddHeart()
    {
        health.health += 2;
        UpdateHearts();
    }

}
