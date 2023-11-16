using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammunitionText;
    public Text magText;

    public static AmmoCount Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateAmmoText(int CurrentAmmo)
    {
        ammunitionText.text = "Ammo. " + CurrentAmmo;
    }

    public void UpdateMagText(int mag)
    {
        magText.text = "Mag. " + mag;
    }
}
