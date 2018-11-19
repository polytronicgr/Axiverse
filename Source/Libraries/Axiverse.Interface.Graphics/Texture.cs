﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Windows;
using SharpDX.Direct3D12;

using Axiverse.Resources;
using Axiverse.Injection;

namespace Axiverse.Interface.Graphics
{
    using System.Drawing;
    using SharpDX.Direct3D12;

    public class Texture : GraphicsResource
    {
        internal CpuDescriptorHandle NativeRenderTargetView;
        internal CpuDescriptorHandle NativeDepthStencilView;

        /// <summary>
        /// Gets the dimensions of the texture.
        /// </summary>
        public int Dimensions { get; private set; }

        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the texture. This will always be 1 for 1D textures.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the depth of the texture. This will always be 1 for 1D and 2D textures.
        /// </summary>
        public int Depth { get; private set; }

        /// <summary>
        /// Gets the number of mip levels of the texture. Should be at least 1.
        /// </summary>
        public short MipLevels { get; private set; } = 1;

        public Texture(GraphicsDevice device) : base(device)
        {

        }

        public Resource Resource;
        public Resource UploadResource;

        internal void Initialize(Resource resource)
        {
            Resource = resource;

            NativeRenderTargetView = Device.RenderTargetViewAllocator.Allocate(1);
            Device.NativeDevice.CreateRenderTargetView(Resource, null, NativeRenderTargetView);
            // TODO: Recycle render target view
        }

        public void CreateDepth(int width, int height)
        {
            Width = width;
            Height = height;


            ClearValue depthOptimizedClearValue = new ClearValue()
            {
                Format = Format.D32_Float,
                DepthStencil = new DepthStencilValue() { Depth = 1.0F, Stencil = 0 },
            };

            Resource = Device.NativeDevice.CreateCommittedResource(
                new HeapProperties(HeapType.Default),
                HeapFlags.None,
                ResourceDescription.Texture2D(
                    Format.D32_Float, Width, Height,
                    mipLevels: MipLevels,
                    flags: ResourceFlags.AllowDepthStencil),
                ResourceStates.DepthWrite,
                depthOptimizedClearValue);

            NativeDepthStencilView = Device.DepthStencilViewAllocator.Allocate(1);
            Device.NativeDevice.CreateDepthStencilView(Resource, null, NativeDepthStencilView);
        }

        public void Load(string filename)
        {
            var library = Injector.Global.Resolve<Library>();

            Bitmap bitmap;
            using (var stream = library.OpenRead(filename))
            {
                bitmap = new Bitmap(stream);
            }

            Width = bitmap.Width;
            Height = bitmap.Height;
            Depth = 1;
            Dimensions = 2;
            MipLevels = (short)(Math.Floor(Math.Log(Math.Min(Width, Height), 2)));


            // create resources

            var imageFormat = Format.B8G8R8A8_UNorm;
            double levels = Math.Log(Math.Min(Width, Height), 2) - 1;
            var resourceDescription =
                ResourceDescription.Texture2D(imageFormat, Width, Height, mipLevels: MipLevels);            

            Resource = Device.NativeDevice.CreateCommittedResource(
                new HeapProperties(HeapType.Default),
                HeapFlags.None,
                resourceDescription,
                ResourceStates.CopyDestination);

            UploadResource = Device.NativeDevice.CreateCommittedResource(
                new HeapProperties(CpuPageProperty.WriteBack, MemoryPool.L0),
                HeapFlags.None,
                resourceDescription,
                ResourceStates.GenericRead);

            // copy into upload buffer

            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, Width, Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            UploadResource.WriteToSubresource(
                0,
                new ResourceRegion()
                {
                    Back = 1,
                    Right = Width,
                    Bottom = Height,
                },
                data.Scan0,
                Width * 4,
                Width * Height * 4);

            bitmap.UnlockBits(data);

            // Generate mipmaps on CPU
            for (int i = 1; i < MipLevels; i++)
            {
                int mipWidth = Width / (1 << i);
                int mipHeight = Height / (1 << i);
                Bitmap mipMap = new Bitmap(bitmap, new Size(mipWidth, mipHeight));
                data = mipMap.LockBits(
                    new Rectangle(0, 0, mipWidth, mipHeight),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                UploadResource.WriteToSubresource(
                    i,
                    new ResourceRegion()
                    {
                        Back = 1,
                        Right = mipWidth,
                        Bottom = mipHeight,
                    },
                    data.Scan0,
                    mipWidth * 4,
                    mipWidth * mipHeight * 4);

                mipMap.UnlockBits(data);
                mipMap.Dispose();
            }



            bitmap.Dispose();


            Device.UploadQueue.Enqueue(this);
            //ShaderResourceViewDescription;
            //ShaderResourceViewDescription = new ShaderResourceViewDescription
            //{
            //    Shader4ComponentMapping = 5768,
            //    //Shader4ComponentMapping = D3DXUtilities.DefaultComponentMapping(),
            //    Format = imageFormat,
            //    Dimension = ShaderResourceViewDimension.Texture2D,
            //    Texture2D = { MipLevels = 1 },
            //};

            // queue upload job from
            // Renderer.ResourcePipeline.Resources.Add(this);
        }

        public override void Upload(CommandList commandList)
        {
            if (UploadResource != null)
            {
                // copy from upload buffer to gpu buffer

                for (int i = 0; i < MipLevels; i++)
                {
                    commandList.NativeCommandList.CopyTextureRegion(
                        new TextureCopyLocation(Resource, i), 0, 0, 0,
                        new TextureCopyLocation(UploadResource, i), null);
                }

                commandList.NativeCommandList.ResourceBarrierTransition(
                    Resource,
                    ResourceStates.CopyDestination,
                    ResourceStates.PixelShaderResource);
            }
        }

        public override void DisposeUpload()
        {
            if (UploadResource != null)
            {
                UploadResource.Dispose();
                UploadResource = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            UploadResource?.Dispose();
            Resource?.Dispose();
        }
    }
}
