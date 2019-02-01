using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Homework2_106
{
	class GameObject
	{
		/// <summary>
		/// The image of the GameObject
		/// </summary>
		protected Texture2D texture;
		/// <summary>
		/// The position and size of the GameObject
		/// </summary>
		protected Rectangle position;

		/// <summary>
		/// An Object storing an image, it's size, and position (in a rectangle object)
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="rectangle"></param>
		public GameObject(Texture2D texture, Rectangle rectangle)
		{
			this.texture = texture;
			this.position = rectangle;
		}

		/// <summary>
		/// Image used to show the object
		/// </summary>
		public Texture2D Texture { get { return texture;} set { texture = value; } }
		/// <summary>
		/// Rectangle storing the position and size of the object
		/// </summary>
		public Rectangle Position { get { return position; } set { position = value;} }

		/// <summary>
		/// The x coordinate of the top-left of this Object
		/// </summary>
		public int X
		{
			get { return position.X; }
			set
			{
				position.Location = new Point(value, position.Y);
			}
		}
		/// <summary>
		/// The y coordinate of the top-left of this Object
		/// </summary>
		public int Y
		{
			get { return position.Y; }
			set
			{
				position.Location = new Point(position.X, value);
			}
		}

		public virtual void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, position, Color.White);
		}
	}
}
