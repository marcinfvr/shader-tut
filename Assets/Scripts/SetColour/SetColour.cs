using UnityEngine;

public class SetColour : MonoBehaviour
{
    [SerializeField] private ComputeShader m_Shader;
    [SerializeField] private string m_KernelName;

    [SerializeField] private MeshRenderer m_Output;
    [SerializeField] private Vector2Int m_TextureSize;
    [SerializeField][ColorUsage(false)] private Color m_Color;

    private RenderTexture m_Texture;
    private int m_KernelId;

    private void OnEnable()
    {
        //find kernel id
        m_KernelId = m_Shader.FindKernel(m_KernelName);
        //create texture
        m_Texture = new RenderTexture(m_TextureSize.x, m_TextureSize.y, 0, RenderTextureFormat.ARGB32)
        {
            enableRandomWrite = true, //has to be set when bound as RWTexture<>
            filterMode = FilterMode.Point,
        };
        m_Texture.Create(); //remember to Release()
        m_Output.material.mainTexture = m_Texture;

        //buffers and textures - set only once
        m_Shader.SetTexture(m_KernelId, "Result", m_Texture);
    }

    private void Update()
    {
        //update shader colour value
        m_Shader.SetVector("Colour", m_Color);
        //dispatch shader using width and height groups
        m_Shader.Dispatch(m_KernelId, m_Texture.width, m_Texture.height, 1);
    }

    private void OnDestroy()
    {
        m_Texture?.Release();
    }
}
