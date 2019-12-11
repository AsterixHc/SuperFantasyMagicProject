﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SuperFantasyMagicProject
{
    class GameObject
    {

        //TODO: This class is currently completely redundant, as it contains no members except empty default methods.
        //Move mutual members from Characters into here instead (texture, texturePath, position and other stuff)

        public GameObject()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {
            //Is Loading the Content
        }

        public virtual void Update(GameTime gameTime)
        {
            //Is Updating every frame and the time inbetween
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Is Drawing a sprite with the use of SpriteBatch
        }
    }
}
