using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jawabanScript : MonoBehaviour
{
    public SpriteRenderer jawabanSprite;

    void Update()
    {
        if (gameObject.transform.parent==null)
        {
            Destroy(gameObject);
        }
    }
}
