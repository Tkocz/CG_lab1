﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Manager;
using Manager.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Manager.Subsystems
{
    public class HeightmapSystem : Core
	{
		Matrix world;
		public HeightmapSystem(Matrix world)
		{
			device = Engine.GetInst().GraphicsDevice;
			this.world = world;
			LoadHeightMap();

		}
		GraphicsDevice device;
		public void LoadHeightMap()
		{
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var heightmapComponent = entity.GetComponent<HeightmapComponent>();
				if (heightmapComponent == null)
					continue;
				LoadHeightMapData(heightmapComponent.heightMap, heightmapComponent);
				SetUpVertices(heightmapComponent);
				SetUpIndices(heightmapComponent);
				CalculateNormals(heightmapComponent);
				SetUpBuffers(heightmapComponent);
			}

		}

		public void SetUpBuffers(HeightmapComponent component)
		{
			if (component.vertexBuffer == null)
			{
				component.vertexBuffer = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration, component.vertices.Length, BufferUsage.WriteOnly);
				component.vertexBuffer.SetData(component.vertices);
			}

			component.indexBuffer = new IndexBuffer(device, typeof(int), component.indices.Length, BufferUsage.WriteOnly);
			component.indexBuffer.SetData(component.indices);
		}

		private void SetUpHeightMapData(HeightmapComponent component)
		{
			component.heightMapData = new float[4, 3];
			component.heightMapData[0, 0] = 0;
			component.heightMapData[1, 0] = 0;
			component.heightMapData[2, 0] = 0;
			component.heightMapData[3, 0] = 0;

			component.heightMapData[0, 1] = 0.5f;
			component.heightMapData[1, 1] = 0;
			component.heightMapData[2, 1] = -1.0f;
			component.heightMapData[3, 1] = 0.2f;

			component.heightMapData[0, 2] = 1.0f;
			component.heightMapData[1, 2] = 1.2f;
			component.heightMapData[2, 2] = 0.8f;
			component.heightMapData[3, 2] = 0;
		}

		private void SetUpVertices(HeightmapComponent component)
		{
			component.vertices = new VertexPositionNormalTexture[component.terrainWidth * component.terrainHeight];
			for (int x = 0; x < component.terrainWidth; x++)
			{
				for (int y = 0; y < component.terrainHeight; y++)
				{
					component.vertices[x + y * component.terrainWidth].Position = new Vector3(x, component.heightMapData[x, y], -y);
					component.vertices[x + y * component.terrainWidth].TextureCoordinate = new Vector2(x, y);
				}
			}
		}

		private void SetUpIndices(HeightmapComponent component)
		{
			component.indices = new int[(component.terrainWidth - 1) * (component.terrainHeight - 1) * 6];
			int counter = 0;
			for (int y = 0; y < component.terrainHeight - 1; y++)
			{
				for (int x = 0; x < component.terrainWidth - 1; x++)
				{
					int lowerLeft = x + y * component.terrainWidth;
					int lowerRight = (x + 1) + y * component.terrainWidth;
					int topLeft = x + (y + 1) * component.terrainWidth;
					int topRight = (x + 1) + (y + 1) * component.terrainWidth;

					component.indices[counter++] = topLeft;
					component.indices[counter++] = lowerRight;
					component.indices[counter++] = lowerLeft;

					component.indices[counter++] = topLeft;
					component.indices[counter++] = topRight;
					component.indices[counter++] = lowerRight;
				}
			}
		}

		private void CalculateNormals(HeightmapComponent component)
		{
			for (int i = 0; i < component.vertices.Length; i++)
				component.vertices[i].Normal = new Vector3(0, 0, 0);

			for (int i = 0; i < component.indices.Length / 3; i++)
			{
				int index1 = component.indices[i * 3];
				int index2 = component.indices[i * 3 + 1];
				int index3 = component.indices[i * 3 + 2];

				Vector3 side1 = component.vertices[index1].Position - component.vertices[index3].Position;
				Vector3 side2 = component.vertices[index1].Position - component.vertices[index2].Position;
				Vector3 normal = Vector3.Cross(side1, side2);

				component.vertices[index1].Normal += normal;
				component.vertices[index2].Normal += normal;
				component.vertices[index3].Normal += normal;
			}

			for (int i = 0; i < component.vertices.Length; i++)
				component.vertices[i].Normal.Normalize();
		}

		private void LoadHeightMapData(Texture2D heightMap, HeightmapComponent component)
		{
			component.terrainWidth = heightMap.Width;
			component.terrainHeight = heightMap.Height;

			Color[] heightMapColors = new Color[component.terrainWidth * component.terrainHeight];
			heightMap.GetData(heightMapColors);

			component.heightMapData = new float[component.terrainWidth, component.terrainHeight];
			for (int x = 0; x < component.terrainWidth; x++)
				for (int y = 0; y < component.terrainHeight; y++)
					component.heightMapData[x, y] = heightMapColors[x + y * component.terrainWidth].R;
		}


        //Creates and renders all the HeightmapComponents.


		public override void draw(GameTime gameTime)
		{;
			CameraComponent camera = null;
			device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var cameraModel = entity.GetComponent<CameraComponent>();
				if (cameraModel != null)
					camera = cameraModel;
			}
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var heightMapComponent = entity.GetComponent<HeightmapComponent>();
				if (heightMapComponent == null)
					continue;
				

				device.SetVertexBuffer(heightMapComponent.vertexBuffer);
				device.Indices = heightMapComponent.indexBuffer;
				RasterizerState rs = new RasterizerState();
				rs.FillMode = FillMode.Solid;
				device.RasterizerState = rs;
				var effect = heightMapComponent.basicEffect;
				var terrainWidth = heightMapComponent.terrainWidth;
				var terrainHeight = heightMapComponent.terrainHeight;
				var objectWorld = Matrix.CreateTranslation(-terrainWidth / 2.0f, 0, terrainHeight / 2.0f);
				effect.World = objectWorld;
				effect.View = camera.view;
				effect.Projection = camera.projection;

				effect.EnableDefaultLighting();
				effect.LightingEnabled = true;

				effect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
				effect.DirectionalLight0.Direction = new Vector3(-0.5f, 1.5f, 3.5f);
				effect.DirectionalLight0.SpecularColor = new Vector3(-0.1f, -0.1f, -0.1f);
				effect.DirectionalLight0.Enabled = true;

				effect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
				effect.PreferPerPixelLighting = true;
				effect.SpecularPower = 100;
				effect.EmissiveColor = new Vector3(0.1f, 0.1f, 0.1f);
				effect.DiffuseColor = Color.SandyBrown.ToVector3();
				effect.TextureEnabled = true;
				effect.Texture = heightMapComponent.heightMapTexture;
				foreach (EffectPass pass in effect.CurrentTechnique.Passes)
				{
					pass.Apply();
					device.Indices = heightMapComponent.indexBuffer;
					device.SetVertexBuffer(heightMapComponent.vertexBuffer);
					device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, heightMapComponent.vertices.Length, 0, heightMapComponent.indices.Length / 3);
				}
			}
		}
    }
}
