using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject healthbarText;
    [SerializeField] private Entity myEntity;
    private float healthbarOriginalWidth;
    // Start is called before the first frame update
    void Start()
    {
        healthbarOriginalWidth = healthbar.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbar(myEntity.health, myEntity.maxHealth);
    }

    public void UpdateHealthbar(int health,int maxhealth)
    {
        float ratio = (float)health / (float)maxhealth;
        healthbarText.GetComponent<TextMeshProUGUI>().text = health + "/" + maxhealth;
        healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthbarOriginalWidth * ratio, healthbar.GetComponent<RectTransform>().sizeDelta.y);
    }
}
