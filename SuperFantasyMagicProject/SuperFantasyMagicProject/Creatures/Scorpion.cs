using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Scorpion : Character
    {
        public Scorpion()
        {

        }

        public override void Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array

        }

        public override void SpecialAttack()
        {
            //Attack at random against Player with "Poisen sting"
            //Apply Poisen on Target
            //Poisen should deal a small about of dmg every time the poisened character has a turn ( 1-2% of max health)
        }

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
