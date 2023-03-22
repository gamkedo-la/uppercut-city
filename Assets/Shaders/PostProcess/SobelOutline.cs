using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SobelOutline : ScriptableRendererFeature
{
    public SobelOutlineSettings OutlineSettings = new SobelOutlineSettings();
    
    private SobelOutlinePass _renderPass;
    private RenderTargetHandle _outlineTexture;
    
    public override void Create()
    {
        _renderPass = new SobelOutlinePass(OutlineSettings.sobelOutlineMaterial);
        _renderPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        
        _outlineTexture.Init("_OutlineTexture");
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (OutlineSettings.sobelOutlineMaterial == null)
        {
            Debug.LogWarning("Missing outline material");
            return;
        }
        
        _renderPass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
        renderer.EnqueuePass(_renderPass);
    }

    [System.Serializable]
    public class SobelOutlineSettings
    {
        public Material sobelOutlineMaterial = null;
    }

    class SobelOutlinePass : ScriptableRenderPass
    {
        public const string SobelShader = "PostProcess/SobelOutline";
        public Material OutlineMaterial = null;
        
        private RenderTargetHandle TempColorTexture;
        private RenderTargetIdentifier _source { get; set; }
        private RenderTargetHandle _destination { get; set; }

        public SobelOutlinePass(Material material)
        {
            OutlineMaterial = material;
        }

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            _source = source;
            _destination = destination;
        }
        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(name: "SobelOutlinePass");

            var opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDescriptor.depthBufferBits = 0;

            if (_destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(TempColorTexture.id, opaqueDescriptor, FilterMode.Point);
                Blit(cmd, _source, TempColorTexture.Identifier(), OutlineMaterial, 0);
                Blit(cmd, TempColorTexture.Identifier(), _source);
            }
            else
            {
                Blit(cmd, _source, _destination.Identifier(), OutlineMaterial, 0);
            }
            
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
