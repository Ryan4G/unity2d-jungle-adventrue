using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 1.0f;

    public float maxOffset = 3.0f;

    private float currOffset = 0f;

    private Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currOffset = speed * Time.deltaTime;

        transform.Translate(0, currOffset, 0);

        if (Mathf.Abs(transform.position.y - originPos.y) >= maxOffset)
        {
            transform.position = new Vector3(originPos.x, originPos.y + speed * maxOffset, originPos.z);

            speed *= -1;
        }
    }
}
