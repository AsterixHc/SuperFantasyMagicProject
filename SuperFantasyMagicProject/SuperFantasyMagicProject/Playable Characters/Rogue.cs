using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Rogue : Character
    {
        public Rogue()
        {
            MaxHealth = 100;
            CurrentHealth = 100;
            Mana = 100;
            Strenght = 10;
            Agility = 10;
            Intelligence = 10;
            Path = "Player/Jeremy/Jeremy pink/JeremyPinkAttack/JeremyPinkAttackRight1";
            Position = Vector2.Zero;
            Damage = 20;
        }

        public Rogue(int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence, Vector2 position, int damage)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            Mana = mana;
            Strenght = strenght;
            Agility = agility;
            Intelligence = intelligence;
            Path = "Player/Jeremy/Jeremy pink/JeremyPinkAttack/JeremyPinkAttackRight1";
            Position = position;
            Origin = Vector2.Zero;
            Damage = damage;
        }

        public override int Attack()
        {

            //Choose an Enemy from the enemy array
            //Choose enemy from enemy array
            return 0;
        }

        public override void SpecialAttack()
        {
            //Item is always the same from the same monster
            //Steal Item from choosen enemy in the enemy array

        }

        public override void LevelUp()
        {
            //Checks amount of Exprience gained
            //If Exprience gained is higher than Exprience needed to "Levelup"
            //Trigger LevelUp screen
            //Increase Stats
        }

        public override void Flee()
        {
            //Escape from Combat
        }

        public override void UseItem(Item item)
        {
            //Checks List for items
            //If item selected is on List
            //Call Item Script (f.eks. Item.Potion)
            //Gain Effect
            //Check if Item Effect Gained
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
