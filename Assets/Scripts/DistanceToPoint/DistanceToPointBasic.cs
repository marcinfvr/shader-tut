using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToPointBasic : ShaderDispatcher, IModule
{
    [SerializeField] private Vector2Int m_TextureSize;
    [SerializeField][Range(350f, 700f)] private float m_Radius;
    [SerializeField][ColorUsage(false)] private Color m_ColourOn;
    [SerializeField][ColorUsage(false)] private Color m_ColourOff;
    [SerializeField] private Transform m_Target;
    [SerializeField] private RawImage m_Output;

    private ComputeBuffer m_DispatchArgs;
    private RenderTexture m_Texture;

    public void Setup()
    {
        Init();
        m_Texture = Tools.CreateTexture(m_TextureSize);
        m_Output.texture = m_Texture;
        m_Output.SetNativeSize();
        
        SetTexture("Result", m_Texture);

        m_DispatchArgs?.Dispose();
        m_DispatchArgs = new ComputeBuffer(3, sizeof(float), ComputeBufferType.IndirectArguments);
        m_DispatchArgs.SetData(new[] { m_TextureSize.x, m_TextureSize.y, 1 });
    }

    protected override void PreDispatch()
    {
        SetFloat("radius", m_Radius);
        SetVector("colourOn", m_ColourOn);
        SetVector("colourOff", m_ColourOff);
        SetVector("position", m_Target.position);
    }

    public void Dispatch()
    {
        DispatchIndirect(m_DispatchArgs);
    }

    private void OnDestroy()
    {
        m_Texture?.Release();
        m_DispatchArgs?.Dispose();
    }
}
