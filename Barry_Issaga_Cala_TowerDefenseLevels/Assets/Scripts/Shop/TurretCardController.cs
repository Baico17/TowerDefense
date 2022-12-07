using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretCardController : MonoBehaviour
{
    [Header("Ui Settings")]
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public void SetTurretButton(TurretSettingsController turretSettings)
    {
        turretImage.sprite = turretSettings.turretShopSprite;
        turretCost.text = turretSettings.turretShopCost.ToString();
    }
}
