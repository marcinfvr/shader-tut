using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static RenderTexture CreateTexture(Vector2Int size)
    {
        var rt = new RenderTexture(size.x, size.y, 0, RenderTextureFormat.ARGB32)
        {
            enableRandomWrite = true,
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp,
        };
        rt.Create();
        return rt;
    }

}
