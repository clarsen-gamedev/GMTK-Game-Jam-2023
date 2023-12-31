// Name: RunnerDeath.cs
// Author: Connor Larsen
// Date: 07/08/2023
// Description: Script which controls the runners' death

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerDeath : MonoBehaviour
{
    [SerializeField] GameObject blood;

    public void Death()
    {
        Destroy(transform.parent.gameObject);
    }

    public void HaltMomentum()
    {
        transform.parent.gameObject.GetComponent<RunnerController>().isDead = true;
        transform.parent.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Instantiate(blood, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().sortingOrder = -5;
    }
}