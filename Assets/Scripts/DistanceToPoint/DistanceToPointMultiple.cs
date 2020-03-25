using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToPointMultiple : ShaderDispatcher, IModule
{
    [SerializeField] private Vector2Int m_TextureSize;
    [SerializeField][Range(350f, 700f)] private float m_Radius;
    [SerializeField][ColorUsage(false)] private Color m_ColourOn;
    [SerializeField][ColorUsage(false)] private Color m_ColourOff;
    [SerializeField] private Transform[] m_Targets;
    [SerializeField] private RawImage m_Output;

    private ComputeBuffer m_DispatchArgs;
    private RenderTexture m_Texture;

    private Vector4[] m_TargetsData;
    private ComputeBuffer m_TargetsBuffer;

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

        m_TargetsData = new Vector4[m_Targets.Length];
        m_TargetsBuffer?.Dispose();
        m_TargetsBuffer = new ComputeBuffer(m_TargetsData.Length, sizeof(float) * 4);
        SetBuffer("data", m_TargetsBuffer);
    }

    protected override void PreDispatch()
    {
        SetVector("colourOn", m_ColourOn);
        SetVector("colourOff", m_ColourOff);

        RefreshTargetData();
    }

    private void RefreshTargetData()
    {
        for (int i = 0; i < m_Targets.Length; i++)
        {
            var target = m_Targets[i];
            m_TargetsData[i] = target.position;
            m_TargetsData[i].w = m_Radius;
        }
        m_TargetsBuffer.SetData(m_TargetsData);
    }

    public void Dispatch()
    {
        DispatchIndirect(m_DispatchArgs);
    }

    private void OnDestroy()
    {
        m_Texture?.Release();
        m_TargetsBuffer?.Dispose();
        m_DispatchArgs?.Dispose();
    }
}
