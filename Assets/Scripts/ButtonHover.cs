using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text theText;
    private TMP_TextInfo textInfo;
    private Color32[] vertexColors;

    public Color topLeftColor = new Color(0.968f, 0.027f, 0.561f); // #F7078F
    public Color bottomRightColor = new Color(0.227f, 0.984f, 0.996f); // #3AFCFE

    void Start()
    {
        textInfo = theText.textInfo;
        int vertexCount = textInfo.characterCount * 4; // 4 vertices per character
        vertexColors = new Color32[vertexCount];
        UpdateColors(Color.white);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateColors(topLeftColor, bottomRightColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restore original colors
        UpdateColors(topLeftColor, topLeftColor);
    }

    private void UpdateColors(Color32[] colors)
    {
        for (int i = 0; i < vertexColors.Length; i++)
        {
            vertexColors[i] = colors[i];
        }
        theText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private void UpdateColors(Color color)
    {
        for (int i = 0; i < vertexColors.Length; i++)
        {
            vertexColors[i] = color;
        }
        theText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private void UpdateColors(Color topLeftColor, Color bottomRightColor)
    {
        int materialIndex = 0; // We assume there's only one material
        TMP_MeshInfo[] meshInfo = theText.textInfo.meshInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            Color32[] colors = meshInfo[materialIndex].colors32;

            colors[vertexIndex + 0] = topLeftColor; // Top Left
            colors[vertexIndex + 1] = bottomRightColor; // Bottom Left
            colors[vertexIndex + 2] = bottomRightColor; // Bottom Right
            colors[vertexIndex + 3] = topLeftColor; // Top Right
        }

        theText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
