﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct3D12;
using Axiverse.Injection;

namespace Axiverse.Interface.Graphics.Shaders
{
    public class GeometryShader : Shader
    {
        [StructLayout(LayoutKind.Sequential, Pack = 16, Size = 256)]
        public struct PerObject
        {
            public Matrix4 WorldViewProjection;
            public Vector4 Color;
        }

        public DescriptorLayout Layout { get; private set; }

        // cbufferview
        // samplers
        public GeometryShader(GraphicsDevice device) : base(device)
        {
            
        }

        public void Initialize()
        {
            // Pipeline state.
            var testShaderPath = "../../../../../Resources/Engine/Forward/Standard.hlsl";
            VertexShader = ShaderBytecode.CompileFromFile(testShaderPath, "VSMain", "vs_5_0");
            PixelShader = ShaderBytecode.CompileFromFile(testShaderPath, "PSMain", "ps_5_0");

            // Root parameters.
            var rootParameters = new RootParameter[]
            {
                new RootParameter(ShaderVisibility.All,
                    new DescriptorRange()
                    {
                        RangeType = DescriptorRangeType.ConstantBufferView,
                        BaseShaderRegister = 0,
                        OffsetInDescriptorsFromTableStart = int.MinValue,
                        DescriptorCount = 1,
                    }),
                new RootParameter(ShaderVisibility.Pixel,
                    new DescriptorRange()
                    {
                        RangeType = DescriptorRangeType.ShaderResourceView,
                        DescriptorCount = 1,
                        OffsetInDescriptorsFromTableStart = int.MinValue,
                        BaseShaderRegister = 0
                    }),
                 new RootParameter(ShaderVisibility.Pixel,
                    new DescriptorRange()
                    {
                        RangeType = DescriptorRangeType.Sampler,
                        BaseShaderRegister = 0,
                        DescriptorCount = 1
                    }),
            };
            var rootSignatureDescription = new RootSignatureDescription(RootSignatureFlags.AllowInputAssemblerInputLayout, rootParameters);
            RootSignature = RootSignature.Create(Device, rootSignatureDescription);

            Layout = new DescriptorLayout(
                DescriptorLayout.EntryType.ConstantBufferShaderResourceOrUnorderedAccessView,
                DescriptorLayout.EntryType.ConstantBufferShaderResourceOrUnorderedAccessView,
                DescriptorLayout.EntryType.SamplerState);
        }
    }
}
