using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
    [SerializeField] private GameObject turretCardPrefab;
    [SerializeField] private Transform turretPanelContainer;

    [Header("Turret Settings")]
    [SerializeField] private TurretSettings[] turrets; //scriptable object ¹è¿­

    private Node _currentNodeSelected;

    void Start()
    {
        for(int i=0; i<turrets.Length; i++)
        {
            CreateTurretCard(turrets[i]);
        }
        
    }

    private void CreateTurretCard(TurretSettings turretSettings)
    {
        GameObject newInstance = Instantiate(turretCardPrefab, turretPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(turretPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TurretCard cardButton = newInstance.GetComponent<TurretCard>();
        cardButton.SetupTurretButton(turretSettings);
    }
    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
        TurretCard.OnPlaceTurret += PlaceTurret;
        Node.OnTurretSold += TurretSold;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
        TurretCard.OnPlaceTurret -= PlaceTurret;
        Node.OnTurretSold -= TurretSold;
    }

    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
    }

    private void TurretSold()
    {
        _currentNodeSelected = null;
    }

    private void PlaceTurret(TurretSettings turretLoaded)
    {
        if(_currentNodeSelected != null)
        {
            GameObject turretInstance = Instantiate(turretLoaded.TurretPrefab);
            turretInstance.transform.position = _currentNodeSelected.transform.position;
            turretInstance.transform.parent = _currentNodeSelected.transform;

            Turret turretPlaced = turretInstance.GetComponent<Turret>();
            _currentNodeSelected.SetTurret(turretPlaced);
        }
    }

}
