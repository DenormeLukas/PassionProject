using UnityEngine;
using UnityEngine.Tilemaps;

public class Collisions : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        var tilemap = collision.gameObject.GetComponent<Tilemap>();
        if (tilemap != null)
        {
            Vector3 collisionWorldPos = collision.contacts[0].point;
            Vector3Int position = tilemap.WorldToCell(collisionWorldPos);
            var sprite = tilemap.GetSprite(position);
            if (sprite != null)
            {
                Color[] pixels = sprite.texture.GetPixels();
                float r = 0f, g = 0f, b = 0f;
                int pixelCount = 0;
                for (int j = 0; j < pixels.Length; j++)
                {
                    if (pixels[j].a > 0)
                    {
                        r += pixels[j].r;
                        g += pixels[j].g;
                        b += pixels[j].b;
                        pixelCount++;
                    }
                }

                Color averageColor = new Color(r / pixelCount, g / pixelCount, b / pixelCount);

                Debug.Log(averageColor);

                if (averageColor.r > 0.002f || averageColor.g > 0.002f || averageColor.b > 0.002f)
                {

                    Debug.Log("COLOR");

                } else
                {
                    Debug.Log("BLACK");
                }
            }
        }
    }
}
