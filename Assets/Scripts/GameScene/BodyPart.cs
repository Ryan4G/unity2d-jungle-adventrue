using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Sprite detachedSripte;

    public Sprite burnedSprite;

    // bleeding position
    public Transform bloodFountainOrigin;

    private bool detached = false;

    public void Detach()
    {
        detached = true;

        this.tag = "Untagged";

        transform.SetParent(null, true);
    }

    public void Update()
    {
        if (!detached)
        {
            return;
        }

        var rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody.IsSleeping())
        {
            // delete joint
            foreach (Joint2D joint in GetComponentsInChildren<Joint2D>())
            {
                Destroy(joint);
            }

            // delete rigidbody
            foreach (Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>())
            {
                Destroy(body);
            }

            // delete collider
            foreach(Collider2D collider in GetComponentsInChildren<Collider2D>())
            {
                Destroy(collider);
            }

            Destroy(this);
        }
    }

    public void ApplyDamageSprite(StickMan.DamageType damageType)
    {
        Sprite spriteToUse = null;

        switch (damageType)
        {
            case StickMan.DamageType.Burning:
                {
                    spriteToUse = burnedSprite;
                    break;
                }
            case StickMan.DamageType.Slicing:
                {
                    spriteToUse = detachedSripte;
                    break;
                }
            default:
                {
                    break;
                }
        }

        if (spriteToUse != null)
        {
            GetComponent<SpriteRenderer>().sprite = spriteToUse;
        }
    }
}
