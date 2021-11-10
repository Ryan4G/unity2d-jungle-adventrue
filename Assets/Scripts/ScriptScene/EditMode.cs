using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditMode : MonoBehaviour
{
    [Header("Normal Information")]
    public string initName;

    public Color color;

    [Space]
    public int missileCount;

    [Header("Important Information")]
    public int hitPoints;

    [SerializeField]
    private int magicPoints;

    [HideInInspector]
    public bool isHotileToPlayer;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        transform.LookAt(target);
    }
}
