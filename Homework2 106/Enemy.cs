using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2_106
{
    class Enemy : GameObject
    {
        private int moveDely;

        public int MoveType { get; set; }

        public int Radius { get; set; }

        public double Angle { get; set; }

        public Enemy(Texture2D texture, Rectangle rectangle, int moveType, int radius) : base(texture, rectangle)
        {
            MoveType = moveType;
            Angle = 0;
            Radius = radius;
            moveDely = 0;
        }
        public bool CheckCollision(GameObject check)
        {
            return rectangle.Intersects(check.Rectangle);
        }

        public void Move(Random rnd)
        {
            switch (MoveType)
            {
                case 0:
                    if (moveDely > 1)
                    {
                            if (rnd.Next(1) == 0)
                                X += 1;
                            else
                                X -= 1;
                            if (rnd.Next(1) == 0)
                                Y += 1;
                            else
                                Y -= 1;
                    }
                    else
                        moveDely++;
                    break;
                case 1:
                    X += 2;
                    break;
                case 2:
                    if (moveDely > 1)
                    {
                        X += (int)(Radius * Math.Cos(Angle));
                        Y += (int)(Radius * Math.Sin(Angle));
                        Angle += Math.PI / 12;
                        moveDely = 0;
                    }
                    else
                        moveDely++;
                    break;
                case 3:
                    X += 1;
                    Y += 1;
                    break;
            }
        }
    }
}
