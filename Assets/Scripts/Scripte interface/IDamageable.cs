using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Va d�terminer ce qui est touch� et comment il r�agit
    void TakeDamage(bool isHit);

}