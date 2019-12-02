using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Bat : Character
    {
        

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

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
