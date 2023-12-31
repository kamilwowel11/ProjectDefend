using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectDefend.Inputs
{
    public static class MultiSelect
    {
        private static Texture2D _whiteTexture;

        public static Texture2D WhiteTexture
        {
            get
            {
                if (_whiteTexture == null)
                {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }
                return _whiteTexture;
            }
        }
        public static void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
        }
        public static void DrawScreenRectBorder(Rect rect,float thickness, Color color)
        {
            // Top
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            // Bottom
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
            // Left
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            // Right
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
        }
        public static Rect GetScreenRect(Vector3 screenPos1, Vector3 screenPos2)
        {
            // Go from the bottom right to the top left
            screenPos1.y = Screen.height - screenPos1.y;
            screenPos2.y = Screen.height - screenPos2.y;

            // Corners
            Vector3 bottomRight = Vector3.Max(screenPos1, screenPos2);
            Vector3 topLeft = Vector3.Min(screenPos1, screenPos2);

            // Create the rectngle
            return Rect.MinMaxRect(topLeft.x,topLeft.y, bottomRight.x,bottomRight.y);
        }

        public static Bounds GetViewportBounds(Camera camera, Vector3 screenPos1, Vector3 screenPos2)
        {
            Vector3 pos1 = camera.ScreenToViewportPoint(screenPos1);
            Vector3 pos2 = camera.ScreenToViewportPoint(screenPos2);

            Vector3 min = Vector3.Min(pos1, pos2);
            Vector3 max = Vector3.Max(pos1, pos2);

            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;

            Bounds bounds = new Bounds();

            bounds.SetMinMax(min, max);

            return bounds;
        }

    }
}

