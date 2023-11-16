using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int SelectedWeapon = 0;


    // Start is called before the first frame update
    void Start()
    {
        Selectweapon();
    }
    
    // Update is called once per frame
    void Update()
    {
        int previusSelectedWeapon  = SelectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedWeapon >= transform.childCount - 1) 
                SelectedWeapon = 0;
            else
                SelectedWeapon++;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0 )
                SelectedWeapon = transform.childCount - 1;
            else
                SelectedWeapon--;

        }

        if(previusSelectedWeapon != SelectedWeapon)
        {
            Selectweapon();
        }
    }

    void Selectweapon()
    {
        int i = 0;
        foreach(Transform weapon in transform) 
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
