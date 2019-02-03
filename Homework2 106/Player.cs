using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Homework2_106
{
	class Player : GameObject
	{
		private Texture2D flipedImage;

		/// <summary>
		/// The score of the current level
		/// </summary>
		public int LevelScore { get; set; }
		/// <summary>
		/// The overall score of the levels
		/// </summary>
		public int TotalScore { get; set; }
		/// <summary>
		/// Is the player facing to the left?
		/// </summary>
		public bool FacingLeft { get; set; }
		/// <summary>
		/// The Texture of the player fliped across the horizontal axis
		/// </summary>
		public Texture2D FlippedImgage { get { return flipedImage; } set { flipedImage = value; } }

		public Player(Texture2D flipedImage, Texture2D texture, Rectangle rectangle) : base (texture, rectangle)
		{
			LevelScore = 0;
			TotalScore = 0;
			FacingLeft = false;
			this.flipedImage = flipedImage;
		}

		public override void Draw(SpriteBatch sb)
		{
			if(FacingLeft)
				sb.Draw(flipedImage, rectangle, Color.White);
			else
				base.Draw(sb);
		}
	}
}
