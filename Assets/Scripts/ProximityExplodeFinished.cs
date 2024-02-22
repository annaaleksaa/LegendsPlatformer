using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityExplodeFinished : MonoBehaviour
{
    [Tooltip("How close something needs to get before we explode")]
    public float explosionDistance;

    [Tooltip("Blue explosion particles")]
    public GameObject enemyExplosionParticles;

    public bool CheckForExplosion(Transform otherTransform)
    {
        //Explode if we get within the explodeDistance
        if (Vector3.Distance(transform.position, otherTransform.position) < explosionDistance)
        {
            StartCoroutine("Explode");
            return true;
        }
        return false;
    }

    private IEnumerator Explode()
    {
        Instantiate(enemyExplosionParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Destroy(transform.parent.gameObject);
    }
}
