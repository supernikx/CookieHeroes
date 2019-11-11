using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bg1;
    [SerializeField]
    private GameObject bg2;
    [SerializeField]
    private GameObject shadowBg;

    public float moveSpeed;

    private Bounds bound;
    private IEnumerator backgroundSetupRoutine;
    private Vector3 startBg1Pos;
    private Vector3 startBg2Pos;

    public void Setup(Camera _cam)
    {
        shadowBg.SetActive(false);
        startBg1Pos = bg1.transform.position;
        startBg2Pos = bg2.transform.position;
        bound = _cam.GetOrthographicBounds();
    }

    public void StartBackground()
    {
        backgroundSetupRoutine = BackgroundSetupCoroutine();
        StartCoroutine(backgroundSetupRoutine);
    }

    public void ResetBackground()
    {
        if (backgroundSetupRoutine != null)
            StopCoroutine(backgroundSetupRoutine);

        shadowBg.SetActive(false);
        bg1.transform.position = startBg1Pos;
        bg2.transform.position = startBg2Pos;
    }

    public Bounds GetBackgroundBounds()
    {
        return bg1.GetComponent<SpriteRenderer>().bounds;
    }

    private IEnumerator BackgroundSetupCoroutine()
    {
        while (bg2.transform.position.y > 0)
        {
            bg1.transform.Translate(Vector3.down * moveSpeed);
            bg2.transform.Translate(Vector3.down * moveSpeed);
            yield return new WaitForFixedUpdate();
        }

        shadowBg.SetActive(true);
    }
}
