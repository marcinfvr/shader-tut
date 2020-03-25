using UnityEngine;

public class ShaderDispatcher : MonoBehaviour
{
    [SerializeField] private string m_KernelName = "CSMain";
    [SerializeField] private ComputeShader m_Shader;

    private int m_KernelId;

    public void Init()
    {
        m_KernelId = m_Shader.FindKernel(m_KernelName);
    }

    public virtual void Dispatch(int x = 1, int y = 1, int z = 1)
    {
        PreDispatch();
        m_Shader.Dispatch(m_KernelId, x, y, z);
    }

    public void DispatchIndirect(ComputeBuffer args)
    {
        PreDispatch();
        m_Shader.DispatchIndirect(m_KernelId, args);
    }

    protected virtual void PreDispatch() { }

    public void SetFloat(string name, float value)
    {
        m_Shader.SetFloat(name, value);
    }

    public void SetInt(string name, int value)
    {
        m_Shader.SetInt(name, value);
    }

    public void SetVector(string name, Vector3 value)
    {
        m_Shader.SetVector(name, value);
    }

    public void SetVector(string name, Vector4 value)
    {
        m_Shader.SetVector(name, value);
    }

    public void SetBuffer(string name, ComputeBuffer value)
    {
        m_Shader.SetBuffer(m_KernelId, name, value);
    }

    public void SetTexture(string name, Texture texture)
    {
        m_Shader.SetTexture(m_KernelId, name, texture);
    }
}
