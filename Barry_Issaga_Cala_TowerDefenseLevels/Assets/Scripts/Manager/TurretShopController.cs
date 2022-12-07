using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopController : MonoBehaviour
{
    [SerializeField] private GameObject turretButtonPrefab;
    [SerializeField] private Transform turretPanelContent;
    [SerializeField] private TurretSettingsController[] turret;


    void Start()
    {
        for (int i = 0; i < turret.Length; i++)
        {
            CreateTurretCard(turret[i]);
        }
    }


    private void CreateTurretCard(TurretSettingsController turretSettings)
    {
        GameObject _instance = Instantiate(turretButtonPrefab, turretPanelContent.position, Quaternion.identity);
        _instance.transform.SetParent(turretPanelContent);

        //Seguridad para que el Localscale no se modifique
        _instance.transform.localScale = Vector3.one;

        TurretCardController _cardButton = _instance.GetComponent<TurretCardController>();
        _cardButton.SetTurretButton(turretSettings);
    }
   
}
