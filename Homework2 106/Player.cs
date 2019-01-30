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
		/// <summary>
		/// The score of the current level
		/// </summary>
		public int LevelScore { get; set; }
		/// <summary>
		/// The overall score of the levels
		/// </summary>
		public int TotalScore { get; set; }

		public Player(Texture2D texture, Rectangle rectangle) : base (texture, rectangle)
		{
			LevelScore = 0;
			TotalScore = 0;
		}
	}
}
