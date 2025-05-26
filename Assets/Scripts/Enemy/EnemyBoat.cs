using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : MonoBehaviour, IDamageable
{
    [SerializeField] List<BoatParts> boatParts = new List<BoatParts>();

    [SerializeField] int boatHealth = 500;

    void SubcribetoDamage(bool subcribe)
    {
        if (subcribe)
            foreach (BoatParts item in boatParts)
                item.tookDamage += BoatPratTookeDamage;
        else
            foreach (BoatParts item in boatParts)
                item.tookDamage -= BoatPratTookeDamage;

    }
    public void SetOutlineStatus(bool show)
    {

    }

    public void TakeDamage(int damage)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SubcribetoDamage(true);
    }



    void BoatPratTookeDamage(int damage)
    {
        boatHealth -= damage;
        if (boatHealth < 0)
        {
            boatDestroyed();
        }

    }


    void boatDestroyed()
    {
        Debug.Log("Boat destroyed");
    }
    
    
}
