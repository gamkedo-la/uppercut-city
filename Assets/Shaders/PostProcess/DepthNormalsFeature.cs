using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Shaders.PostProcess
{
    public class DepthNormalsFeature : ScriptableRendererFeature
    {
        private Material _depthNormalsMaterial;
        private RenderTargetHandle _depthNormalsTexture;
        private DepthNormalsPass _depthNormalsPass;
        
        public override void Create()
        {
            _depthNormalsMaterial = CoreUtils.CreateEngineMaterial("Hidden/Internal-DepthNormalsTexture");
            _depthNormalsTexture.Init("_CameraDepthNormalsTexture");
            _depthNormalsPass = new DepthNormalsPass(_depthNormalsMaterial)
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPrePasses
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _depthNormalsPass.Setup(renderingData.cameraData.cameraTargetDescriptor, _depthNormalsTexture);
            renderer.EnqueuePass(_depthNormalsPass);
        }
    }


    class DepthNormalsPass : ScriptableRenderPass
    {
        private int _depthBufferBits = 32;
        private RenderTargetHandle _depthAttachmentHandle { get; set; }
        internal RenderTextureDescriptor _descriptor { get; private set; }
        private Material _depthNormalsMaterial;
        private FilteringSettings _filteringSettings;
        private string _profilerTag = "DepthNormals Prepass";
        private List<ShaderTagId> _shaderTags;

        public DepthNormalsPass(Material material)
        {
            _depthNormalsMaterial = material;
            _filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
            _depthAttachmentHandle.Init("_DepthNormalsTexture");
            
            _shaderTags = new List<ShaderTagId>
            {
                new("DepthOnly"),
                new("DepthNormalsOnly")
            };
        }
        
        public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle depthAttachmentHandle)
        {
            _depthAttachmentHandle = depthAttachmentHandle;
            baseDescriptor.colorFormat = RenderTextureFormat.ARGB32;
            baseDescriptor.depthBufferBits = _depthBufferBits;
            _descriptor = baseDescriptor;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(_depthAttachmentHandle.id, _descriptor, FilterMode.Point);
            ConfigureTarget(_depthAttachmentHandle.Identifier());
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var drawSettings = CreateDrawingSettings(_shaderTags, ref renderingData,
                renderingData.cameraData.defaultOpaqueSortFlags);
            drawSettings.overrideMaterial = _depthNormalsMaterial;
            context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref _filteringSettings);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (_depthAttachmentHandle != RenderTargetHandle.CameraTarget)
            {
                cmd.ReleaseTemporaryRT(_depthAttachmentHandle.id);
                _depthAttachmentHandle = RenderTargetHandle.CameraTarget;
            }
        }
    }
}