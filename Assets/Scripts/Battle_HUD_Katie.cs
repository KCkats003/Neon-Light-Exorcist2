using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle_HUD_Katie : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;

        // Get the current health from the unit
        int currentHP = unit.currentHP;
        hpSlider.value = currentHP;
        maxHealthText.text = unit.maxHP.ToString();
        currentHealthText.text = unit.maxHP.ToString();

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        currentHealthText.text = hp.ToString();
    }
}
