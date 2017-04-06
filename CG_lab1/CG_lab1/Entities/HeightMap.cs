using System;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Manager.Core;

namespace CG_lab1
{
	public class HeightMap
	{
		public static Component[] createComponents(string heighmap, string heightMapTexture,Vector3 scale, Vector3 position, Matrix rotation, Matrix objectWorld, Vector3 speed)
		{
			return new Component[]
			{
				new HeightmapComponent(heighmap, heightMapTexture),
				new TransformComponent(scale, position, rotation, objectWorld, speed),
				new CameraComponent()
			};

		}
	}
}


