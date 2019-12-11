﻿using System;
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
            MaxHealth = RogueStats.maxHealth;
            CurrentHealth = RogueStats.currentHealth;
            Mana = RogueStats.mana;
            Strenght = RogueStats.Strenght;
            Agility = RogueStats.Agility;
            Intelligence = RogueStats.Intelligence;
            Damage = RogueStats.damage;
            Turnspeed = RogueStats.turnSpeed;
            Critical = RogueStats.critical;
            Path = "Player/Jeremy/Jeremy pink/JeremyPinkAttack/JeremyPinkAttackRight1";
            Position = Vector2.Zero;
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
            CurrentHealth -= dmg;
        }

        //public void UpdateStats
    }
}
