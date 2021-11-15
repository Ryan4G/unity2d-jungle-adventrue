using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    public Transform cameraFollowTarget;

    public Rigidbody2D ropeBody;

    public Sprite armHoldingEmpty;
    public Sprite armHoldingTreasure;

    public SpriteRenderer holdingArm;

    public GameObject deathPrefab;
    public GameObject flameDeathPrefab;
    public GameObject ghostPrefab;

    public float delayBeforeRemoving = 3.0f;
    public float delayBeforeReleasingGhost = .25f;

    public GameObject bloodFountainPrefab;

    private bool dead = false;

    private bool _holdingTreasure = false;

    public bool HoldingTreasure
    {
        get
        {
            return _holdingTreasure;
        }

        set
        {
            if (dead)
            {
                return;
            }

            _holdingTreasure = value;

            if (holdingArm != null)
            {
                if (_holdingTreasure)
                {
                    holdingArm.sprite = armHoldingTreasure;
                }
                else
                {
                    holdingArm.sprite = armHoldingEmpty;
                }
            }
        }
    }

    public enum DamageType
    {
        Slicing,
        Burning
    }

    public void ShowDamageEffect(DamageType type)
    {
        switch (type)
        {
            case DamageType.Burning:
                {
                    if (flameDeathPrefab != null)
                    {
                        Instantiate(flameDeathPrefab, cameraFollowTarget.position, cameraFollowTarget.rotation);
                    }

                    break;
                }
            case DamageType.Slicing:
                {
                    if (deathPrefab != null)
                    {
                        Instantiate(deathPrefab, cameraFollowTarget.position, cameraFollowTarget.rotation);
                    }

                    break;
                }
            default:
                {

                    break;
                }
        }
    }

    public void DestroyStickMan(DamageType type)
    {
        HoldingTreasure = false;

        dead = true;

        foreach(BodyPart part in GetComponentsInChildren<BodyPart>())
        {
            switch (type)
            {
                case DamageType.Burning:
                    {
                        bool shouldBurn = Random.Range(0, 2) == 0;

                        if (shouldBurn)
                        {
                            part.ApplyDamageSprite(type);
                        }

                        break;
                    }
                case DamageType.Slicing:
                    {
                        part.ApplyDamageSprite(type);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            bool shouldDetach = Random.Range(0, 2) == 0;

            if (shouldDetach)
            {
                part.Detach();

                if (type == DamageType.Slicing)
                {
                    if (part.bloodFountainOrigin != null && bloodFountainPrefab != null)
                    {
                        GameObject fountain = Instantiate(bloodFountainPrefab,
                            part.bloodFountainOrigin.position, part.bloodFountainOrigin.rotation);

                        fountain.transform.SetParent(this.cameraFollowTarget, false);
                    }
                }

                // destory joint
                var allJoints = part.GetComponentsInChildren<Joint2D>();
                foreach(Joint2D joint in allJoints)
                {
                    Destroy(joint);
                }
            }


            foreach (Transform child in part.gameObject.transform)
            {
                child.gameObject.tag = "Untagged";
            }

            var remove = gameObject.AddComponent<RemoveAfterDelay>();
            remove.delay = delayBeforeRemoving;
        }
    }

    private IEnumerator ReleaseGhost()
    {
        if (ghostPrefab != null)
        {
            yield break;
        }

        yield return new WaitForSeconds(delayBeforeReleasingGhost);

        Instantiate(ghostPrefab, transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
