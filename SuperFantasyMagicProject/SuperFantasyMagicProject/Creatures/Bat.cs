using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperFantasyMagicProject.Creatures
{
    class Bat : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Bat()
        {
            Path = "Enemies/Bat/Pink/Animation 1/PinkBat1.1";
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Constructor that specifies position.
        /// </summary>
        /// <param name="position"></param>
        public Bat(Vector2 position)
        {
            Path = "Enemies/Bat/Pink/Animation 1/PinkBat1.1";
            Position = position;
        }

        

        public override int Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array
            return 0;
        }

        public override void SpecialAttack()
        {
            //Applies Screatch against Player (100% of the time)
            //Reduce Player Strength and Health
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
