using UnityEngine;
using System.Collections;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("플래시 FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDuration;
    private Material originalMat;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material;
    }

    void Update()
    {

    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMat;
    }
}
