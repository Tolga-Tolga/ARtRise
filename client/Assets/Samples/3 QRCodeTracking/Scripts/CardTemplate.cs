using UnityEngine;

[System.Serializable]
public class CardTemplate
{
    public int width;
    public int height;

    // Zielposition des QR-Codes im normierten Bild
    public Vector2[] qrTargetCorners;

    // Position des Bildfelds im normierten Bild
    public RectInt artworkRect;

    public CardTemplate(
        int width,
        int height,
        Vector2[] qrCorners,
        RectInt artworkRect
    )
    {
        this.width = width;
        this.height = height;
        this.qrTargetCorners = qrCorners;
        this.artworkRect = artworkRect;
    }
}
