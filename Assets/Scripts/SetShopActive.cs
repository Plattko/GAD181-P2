using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShopActive : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (((Vector2)player.position - new Vector2(0, 0)).sqrMagnitude > (27 * 27))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
