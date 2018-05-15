﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;

using Axiverse;
using Axiverse.Interface.Graphics;
using Axiverse.Interface.Graphics.Shaders;

namespace HelloGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a window
            RenderForm form = new RenderForm()
            {
                Width       = 1024,
                Height      = 720,
                BackColor   = System.Drawing.Color.Black,
                Text        = "Axiverse | Hello Graphics",
            };
            form.Show();

            // Init the rendering device
            var device = GraphicsDevice.Create();
            var swapChain = SwapChain.Create(device, form);
            var commandList = CommandList.Create(device);

            // Define the vertex input layout.
            var inputElementDescs = new[]
            {
                new SharpDX.Direct3D12.InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0)
            };

            // Shaders
            var geometryShader = new GeometryShader(device);
            geometryShader.Initialize();
            var pipelineStateDescription = new PipelineStateDescription()
            {
                InputLayout = new SharpDX.Direct3D12.InputLayoutDescription(inputElementDescs),
                RootSignature = geometryShader.RootSignature,
                VertexShader = geometryShader.VertexShader,
                PixelShader = geometryShader.PixelShader,
            };
            var pipelineState = PipelineState.Create(device, pipelineStateDescription);
            var descriptorSet = new DescriptorSet(device, geometryShader.Layout);

            var transforms = new GeometryShader.PerObject[1];
            transforms[0].Color = new Vector4(0.4f, 0.5f, 0.8f, 1);
            var constantBuffer = GraphicsBuffer.Create(device, transforms, false);
            descriptorSet.SetConstantBuffer(0, constantBuffer);
            descriptorSet.SetSamplerState(0, SamplerState.Create(device, null));

            // Lets create some resources
            var indices = new int[] { 0, 2, 1 };
            var vertices = new float[] { 0.0f, 0.25f, 0.0f, -0.25f, 0.0f, 0.0f, 0.25f, 0.0f, 0.0f };
            var indexBuffer = GraphicsBuffer.Create(device, indices, false);

            var vertexBuffer = GraphicsBuffer.Create(device, vertices, false);
            var indexBinding = new IndexBufferBinding
            {
                Buffer = indexBuffer,
                Count = indices.Length,
                Offset = 0,
            };
            var vertexBinding = new VertexBufferBinding
            {
                Buffer = vertexBuffer,
                Count = 3,
                Offset = 0,
            };


            // Into the loop we go!
            using (var loop = new RenderLoop(form))
            {
                while (loop.NextFrame())
                {
                    var backBuffer = swapChain.StartFrame();
                    var backBufferHandle = swapChain.GetCurrentColorHandle();
                    commandList.Reset(swapChain);

                    commandList.ResourceTransition(backBuffer, ResourceState.Present, ResourceState.RenderTarget);
                    {
                        commandList.SetColorTarget(backBufferHandle);
                        commandList.SetViewport(0, 0, 1024, 720);
                        commandList.SetScissor(0, 0, 1024, 720);
                        commandList.ClearTargetColor(backBufferHandle, 1.0f, 0.0f, 1.0f, 1.0f);

                        commandList.SetRootSignature(pipelineStateDescription.RootSignature);
                        commandList.PipelineState = pipelineState;
                        commandList.SetDescriptors(descriptorSet);

                        commandList.SetIndexBuffer(indexBuffer, indexBuffer.Size, IndexBufferType.Integer32);
                        commandList.SetVertexBuffer(vertexBuffer, 0, vertexBuffer.Size, 3 * 4);
                        commandList.DrawIndexed(indices.Length);
                    }
                    commandList.ResourceTransition(backBuffer, ResourceState.RenderTarget, ResourceState.Present);

                    commandList.Close();
                    swapChain.ExecuteCommandList(commandList);
                    commandList.FinishFrame(swapChain);
                    swapChain.Present();
                }
            }
        }
    }
}
