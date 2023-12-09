using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    public Text levelText, hitPointText, pesosText, upgradeText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            // if no more character
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectChanged();
        }
        else
        {
            currentCharacterSelection--;

            // if no more character
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectChanged();
        }
    }
    private void OnSelectChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();

    }

    // Update character info
    public void UpdateMenu()
    {
        // weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)    
            upgradeText.text = "MAX";
        else
            upgradeText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        // meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        // xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total exp points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;

        }
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
