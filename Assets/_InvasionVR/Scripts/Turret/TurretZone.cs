using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TurretTransactionEventArgs
{
    public TransactionType transactionType;
    public int amountGold;
}

public delegate void TurretTransactionEventHandler(object sender, TurretTransactionEventArgs e);

public class TurretZone : MonoBehaviour {

    public Turret buildedTurret = null;
    public TurretGUI turretGUI;

    // Use this for initialization
    void Start () {
        turretGUI.Hide();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildTurret(Turret turretPrefab)
    {
        if (buildedTurret == null)
        {
            if (true) // CHANGE IT
            {
                //if enough gold
                Turret currentTurret = Instantiate(turretPrefab, transform.position, transform.rotation);
                currentTurret.BuyTurret();
                buildedTurret = currentTurret;
                turretGUI.Show();
                
            }
            else
            {
                //else lack gold animation
            }
        }
    }

    public void SellTurret()
    {
        if (buildedTurret != null) {
            Destroy(buildedTurret.gameObject);
            // get your gold
            buildedTurret = null;
            turretGUI.Show();
        }
        else
        {
            Debug.Log("Shouldn'tBePossible to sell when no turret");
        }
    }

    public void Upgrade()
    {
        buildedTurret.UpgradeTurret();
    }


}

public enum TransactionType
{
    Buy,
    Sell,
    Upgrade
}
