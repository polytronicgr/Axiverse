﻿using SharpDX.Direct3D12;
using System.Collections.Generic;

namespace Axiverse.Interface.Graphics
{
    /// <summary>
    /// The command list to be executed and the set of resources utilized by that command list.
    /// </summary>
    public class CompiledCommandList
    {
        /// <summary>
        /// Gets or sets the <see cref="CommandList"/> which created the
        /// <see cref="CompiledCommandList"/>.
        /// </summary>
        internal CommandList CommandList { get; set; }

        /// <summary>
        /// Gets or sets the list of shader resource view descriptor heaps used by the command
        /// list to be released after the execution of the command list.
        /// </summary>
        internal List<DescriptorHeap> ShaderResourceViewHeaps { get; set; } = new List<DescriptorHeap>();

        /// <summary>
        /// Gets or sets the list of sampler heaps used by the command list to be released after
        /// the execution of the command list.
        /// </summary>
        internal List<DescriptorHeap> SamplerHeaps { get; set; } = new List<DescriptorHeap>();

        /// <summary>
        /// Releases all resources heald by this <see cref="CompiledCommandList"/>.
        /// </summary>
        public void Release()
        {
            var device = CommandList.Device;
            foreach (var shaderResourceViewHeap in ShaderResourceViewHeaps)
            {
                device.ShaderResourceViewDescriptorHeaps.Add(shaderResourceViewHeap);
            }
            ShaderResourceViewHeaps.Clear();

            foreach (var samplerHeaps in SamplerHeaps)
            {
                device.SamplerHeaps.Add(samplerHeaps);
            }
            SamplerHeaps.Clear();
        }
    }
}
