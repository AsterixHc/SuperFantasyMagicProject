using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Hornet : Characters
    {

        public override void Attack()
        {
            //Attack at random against Player
        }

        public override void SpecialAttack()
        {
            //Random% chance to paralyze Player
            //Apply paralyze
        }

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
