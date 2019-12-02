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
        }

        public override void SpecialAttack()
        {
            //Applies Screatch against Player
            //Reduce Player Strength and Health
        }

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
