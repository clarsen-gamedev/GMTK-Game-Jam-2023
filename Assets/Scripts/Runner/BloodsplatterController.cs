// Name: BloodsplatterController.cs
// Author: Connor Larsen
// Date: 07/09/2023
// Description: Delete the bloodsplatter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodsplatterController : MonoBehaviour
{
    public void Delete()
    {
        Destroy(gameObject);
    }
}
