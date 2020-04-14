using UnityEngine;

public class SetColourBrutal : MonoBehaviour
{

    [SerializeField] private MeshRenderer m_Output;
    [SerializeField] private Vector2Int m_TextureSize;
    [SerializeField] [ColorUsage(false)] private Color m_Color;

    private Texture2D m_Texture;
    private Color[] m_Colors;

    private void OnEnable()
    {
        //create texture
        m_Texture = new Texture2D(m_TextureSize.x, m_TextureSize.y)
        {
            filterMode = FilterMode.Point,
        };
        m_Output.material.mainTexture = m_Texture;
        m_Colors = new Color[m_TextureSize.x * m_TextureSize.y];
    }

    private void Update()
    {
        for (int i = 0; i < m_TextureSize.x * m_TextureSize.y; i++)
        {
            m_Colors[i] = m_Color;
        }

        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();
    }
}
