using System.Collections;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    public float dissolveDuration = 10;
    public float dissolveStrength;
    public Color startColor;
    public Color endColor;

    private Material dissolveMaterial;
    private bool isDissolving = false; // Track if the object is currently dissolving

    // Start the dissolving process
    public void StartDissolver()
    {
        if (!isDissolving)
        {
            StartCoroutine(Dissolve());
        }
    }

    private IEnumerator Dissolve()
    {
        isDissolving = true;

        float elapsedTime = 0;
        dissolveMaterial = GetComponent<Renderer>().material;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            Color lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);
            dissolveMaterial.SetColor("_Color", lerpedColor);

            yield return null;
        }

        // Reverse the dissolution process
        yield return new WaitForSeconds(1f); // Add a delay before reversing the process
        StartCoroutine(ReverseDissolve());

        isDissolving = false;
    }

    // Reverse the dissolution process
    private IEnumerator ReverseDissolve()
    {
        float elapsedTime = 0;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(1, 0, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            Color lerpedColor = Color.Lerp(endColor, startColor, dissolveStrength);
            dissolveMaterial.SetColor("_Color", lerpedColor);

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDissolver();
        }
    }
}
