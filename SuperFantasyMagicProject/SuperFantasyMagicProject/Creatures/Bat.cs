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

        public override void Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array
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
