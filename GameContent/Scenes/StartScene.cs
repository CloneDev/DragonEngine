using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using DragonEngine.Entities;
using Microsoft.Xna.Framework.Input;
using DragonEngine;
using DragonEngine.Manager;
using Spine;

namespace SnakeMobile.GameContent.Scenes
{
    class StartScene : Scene
    {
        #region Properties

        #region Spine
        private SkeletonRenderer skeletonRenderer;
        private Skeleton skeleton;
        private AnimationState animationState;
        private SkeletonBounds bounds = new SkeletonBounds();
        private bool flipSkel;
        string name = "";
        #endregion

        #endregion

        #region Getter & Setter
        #endregion

        #region Construct

        public StartScene(String pSceneName)
            : base(pSceneName)
        {
            mClearColor = Color.CadetBlue;
            mUpdateAction.Add(CheckClick);
            mUpdateAction.Add(UpdateSpine);
            mDrawAction.Add(DrawSpine);
        }
        #endregion

        #region Methoden
        protected override void Load()
        {
            TextureManager.Instance.Add("BackgroundStart", @"gfx\menuBackground");
            LoadSpine(new Vector2(500, 650), "fluffy", 1f, 0.2f);
        }

        private void LoadSpine(Vector2 position, string name, float scale, float fading)
        {
            SpineSettings.LoadFadingSettings();
            this.name = name;
            //----------Spine-Daten aufbereiten----------
                
            //----------Hier ggf nochmal kapseln: Allg Vorlage f Spine-Objekttyp----------
                json.Scale = scale;
                SkeletonData skeletonData = json.ReadSkeletonData("Content/spine/" + name + ".json");
                // Define mixing between animations.
                AnimationStateData animationStateData = new AnimationStateData(skeletonData);
                SpineSettings.SetFadingSettings(animationStateData);
            //----------Konkretes instanziertes Spine-Objekt----------
                skeleton = new Skeleton(skeletonData);
                skeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.
                animationState = new AnimationState(animationStateData);
                skeleton.x = position.X;
                skeleton.y = position.Y;
        }

        private bool BoundingBoxCollision(Rectangle cbox) //Checken ob Rectangle mit bb-Attachement (z.B. Keule) kollidiert
        {
            bounds.Update(skeleton, true);
            bool collision = false;
            if (bounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height)
                || bounds.AabbIntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height)
                || bounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y)
                || bounds.AabbIntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height)
                )
            {
                if (bounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height) != null
                    ||
                    bounds.IntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
                    ||
                    bounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y) != null
                    ||
                    bounds.IntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
                    )
                {
                    collision = true;
                }
            }
            return collision;
        }

        private void UpdateSpine(GameTime gameTime)
        {
            //Player -> Drawposition
            //skeleton.X = position.X - camera.viewport.X;
            //skeleton.Y = position.Y - camera.viewport.Y;
            //skeleton.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            animationState.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            animationState.Apply(skeleton);
            skeleton.UpdateWorldTransform();
        }

        private void DrawSpine()
        {
            //----------Spine----------
            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();
            //Player -> Worldposition
            //skeleton.X = position.X;
            //skeleton.Y = position.Y;
        }

        private void CheckClick(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && animationState.GetCurrent(0) != null)
            {
                if (animationState.GetCurrent(0).animation.name == skeleton.data.FindAnimation("idle").name)
                    animationState.SetAnimation(0, "attack", false);
            }
            else if (ms.RightButton == ButtonState.Pressed)
            {
                Environment.Exit(0);
            }
            else
            {
                animationState.AddAnimation(0, "idle", true, 0);
            }
        }
        #endregion
    }
}
