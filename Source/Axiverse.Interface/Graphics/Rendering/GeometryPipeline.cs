﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct3D12;

namespace Axiverse.Interface.Graphics
{
    public class GeometryPipeline : Pipeline
    {
        public Renderer Renderer { get; private set; }
        public RenderTarget RenderTarget { get; set; }
        public Scene Scene { get; private set; }

        private GraphicsCommandList commandList;
        public StandardPipelineState PipelineState { get; set; }

        public GeometryPipeline(Renderer renderer, Scene scene)
        {
            Renderer = renderer;
            RenderTarget = renderer.RenderTarget;
            Scene = scene;

            PipelineState = new StandardPipelineState(Renderer);
            PipelineState.Load(@"Engine\Forward\standard.hlsl", PositionColorTexture.Description);
        }

        public override void Execute()
        {
            if (commandList == null)
            {
                commandList = Renderer.Device.CreateCommandList(CommandListType.Direct, Renderer.CommandAllocator, null);
                commandList.Close();
            }

            commandList.Reset(Renderer.CommandAllocator, null);
            commandList.PipelineState = PipelineState.PipelineState;
            commandList.SetViewport(RenderTarget.Viewport);
            commandList.SetScissorRectangles(RenderTarget.ScissorRectangle);

            // TODO: make this transition higher level.
            // back buffer transition to render target
            commandList.ResourceBarrierTransition(RenderTarget.RenderTargets[RenderTarget.FrameIndex], ResourceStates.Present, ResourceStates.RenderTarget);

            // This var handle is auto generated either on swap or static
            var renderTargetViewHandle = RenderTarget.RenderTargetViewDescriptorHeap.CPUDescriptorHandleForHeapStart +
                (RenderTarget.FrameIndex * RenderTarget.RenderTargetViewDescriptorSize);
            var depthTargetViewHandle = RenderTarget.DepthStencilViewDescriptorHeap.CPUDescriptorHandleForHeapStart;

            commandList.SetRenderTargets(renderTargetViewHandle, depthTargetViewHandle);
            commandList.ClearRenderTargetView(renderTargetViewHandle, new Color4(0.0f, 0.0f, 0.0f, 1), 0, null);
            commandList.ClearDepthStencilView(depthTargetViewHandle, ClearFlags.FlagsDepth, 1, 0);

            // stuff here

            PipelineState.Apply(commandList);
            var viewProjection = Scene.Camera.ViewProjection;

            for (int i = 0; i < Scene.Entities.Count; i++)
            {
                var entity = Scene.Entities[i];
                var model = entity.GetComponent<ModelComponent>();
                if (model != null)
                {
                    PipelineState.SetTexture(model.Texture);
                    var matrix = Matrix.Transpose(entity.Transform.Transformation * viewProjection);
                    PipelineState.SetTransform(matrix, i);
                    PipelineState.ApplyConstantBuffer(commandList, i);
                    //model.Mesh.Draw(commandList);
                }
            }

            // TODO: transition back if we're not going to do canvas stuff.
            // commandList.ResourceBarrierTransition(RenderTarget.RenderTargets[RenderTarget.FrameIndex], ResourceStates.RenderTarget, ResourceStates.Present);

            commandList.Close();
            Renderer.ExecuteCommandList(commandList);
        }

        public override void Dispose()
        {
            commandList.Dispose();
            commandList = null;
        }
    }
}
