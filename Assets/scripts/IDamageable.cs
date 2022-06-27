using UnityEngine;

public interface IDamageable
{
    void takehit(float damage, RaycastHit hit);

    void takedamage(float damage);
}
