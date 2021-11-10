using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    public Rope target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpClick()
    {
        StartCoroutine(UpClick());
    }

    public void OnDownClick()
    {
        StartCoroutine(DownClick());
    }

    private IEnumerator UpClick()
    {
        target.isIncreasing = true;

        yield return new WaitForSeconds(0.5f);

        target.isIncreasing = false;
    }
    private IEnumerator DownClick()
    {
        target.isDecreasing = true;

        yield return new WaitForSeconds(0.5f);

        target.isDecreasing = false;
    }
}
