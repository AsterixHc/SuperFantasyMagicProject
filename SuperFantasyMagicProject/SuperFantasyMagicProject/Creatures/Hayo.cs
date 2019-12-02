using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Hayo : Character
    {

        public override void Attack()
        {
            //Attack at random against Player
        }

        public override void SpecialAttack()
        {
            //Random chance to Apply "Gust"
            //If applied reduce Player Agility
        }

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
