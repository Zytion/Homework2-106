using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Homework2_106
{
	class Collectible : GameObject
	{
		/// <summary>
		/// Is the collectible currently being shown
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		/// An object that can be picked up by the player
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="rectangle"></param>
		public Collectible(Texture2D texture, Rectangle rectangle) : base (texture, rectangle)
		{
			Active = true;
		}
		/// <summary>
		/// Checks to see if the collectible is intersecting with the other GameObject
		/// </summary>
		/// <param name="check"></param>
		/// <returns></returns>
		public bool CheckCollision(GameObject check)
		{
			if(Active)
				return rectangle.Intersects(check.Rectangle);

			return false;
		}

		public override void Draw(SpriteBatch sb)
		{
			if(Active)
				base.Draw(sb);
		}
	}
}
