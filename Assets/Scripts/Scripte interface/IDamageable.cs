using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Va déterminer ce qui est touché et comment il réagit
    void TakeDamage(bool isHit);

}