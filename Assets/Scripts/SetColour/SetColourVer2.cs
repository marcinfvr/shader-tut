
using UnityEngine;
using UnityEngine.UI;

public class SetColourVer2 : ShaderDispatcher, IModule
{
    [ColorUsage(false)]
    [SerializeField]private Color m_Color;
    [Header("Config")]
    [SerializeField] private RawImage m_Output;
    [SerializeField] private Vector2Int m_TextureSize;

    private RenderTexture m_Texture;

    public void Setup()
    {
        Init();
        m_Texture = Tools.CreateTexture(m_TextureSize);
        m_Output.texture = m_Texture;
        m_Output.SetNativeSize();
        SetTexture("Result", m_Texture);
    }

    protected override void PreDispatch()
    {
        SetVector("Colour", m_Color);
    }

    public void Dispatch()
    {
        Dispatch(m_Texture.width, m_Texture.height);
    }

    private void OnDestroy()
    {
        m_Texture?.Release();
    }
}
