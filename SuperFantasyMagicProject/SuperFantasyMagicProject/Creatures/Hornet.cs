﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace SuperFantasyMagicProject
{
    class Hornet : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Hornet()
        {
            texturePath = "Enemies/Hornet/Yellow/Animation 1/Hornet1.";

            baseHealth = 30;
            baseMana = 50;
            baseCritical = 0.05;

            Strength = 4;       //Every point of Strength adds 10 to MaxHealth, and 2 to Damage.
            Agility = 8;        //Every two points of Agility adds 1 to TurnSpeed.
            Intelligence = 2;   //Every point of Intelligence adds 10 to MaxMana, and 0.1 to Critical

            CurrentHealth = MaxHealth;
            CurrentMana = MaxMana;
        }

        public override void LoadContent(ContentManager content)
        {
            //Load textures.
            textures = new Texture2D[3];

            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = content.Load<Texture2D>(texturePath + (i + 1));
            }

            texture = textures[0];

            //Set origin.
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void SpecialAttack()
        {
            //Applies Screatch against Player (100% of the time)
            //Reduce Player Strength and Health
        }
    }
}
