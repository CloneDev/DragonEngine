///**************************************************************
// * (c) Jens Richter 2014
// *************************************************************/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Spine;
//using DragonEngine.Entities;


//namespace DragonEngine.Manager
//{
//    public class SpineManager : Manager
//    {
//        #region Singleton

//        private static SpineManager mInstance;
//        public static SpineManager Instance { get { if (mInstance == null) mInstance = new SpineManager(); return mInstance; } }

//        #endregion

//        #region Properties
//        #endregion

//        #region Constructor

//        SpineManager()
//        {
//        }

//        #endregion

//        #region Methods

//        public override void LoadContent()
//        {
//            Add("fluffy");
//        }

//        public override void Unload()
//        {
//            mRessourcen.Clear();
//        }

//        /// <summary>
//        /// Fügt ein neues Element in mRessourcenManager ein.
//        /// </summary>
//        /// <param name="pName">Name des Skeletons.</param>
//        /// <param name="pPath">Pfad zu den Skeleton-Daten.</param>
//        public void Add<RawSpineData>(String pName, String pPath)
//        {
//            RawSpineData data;
//            if (!mRessourcen.ContainsKey(pName))
//            {
//                data = new RawSpineData(pPath, pName);
//                mRessourcen.Add(pName, data);
//            }
//            else
//                data = (RawSpineData)mRessourcen[pName];

//            data;
//        }

//        /// <summary>
//        /// Fügt ein neues Element in mRessourcenManager ein.
//        /// </summary>
//        /// <param name="pName">Name des Skeletons.</param>
//        public void Add(String pName)
//        {
//            if (!mRessourcen.ContainsKey(pName))
//            {
//                RawSpineData data = new RawSpineData(pName);
//                mRessourcen.Add(pName, data);
//            }
//        }

//        /// <summary>
//        /// Gibt RawSpineData zurück.
//        /// </summary>
//        public override T GetElementByString<T>(string pElementName)
//        {
//            if (mRessourcen.ContainsKey(pElementName))
//                return (T)mRessourcen[pElementName];
//            else
//                return (T)new object();
//        }

//        public Skeleton NewSkeleton(string pName, float pScale)
//        {
//            RawSpineData TmpSpineData = GetElementByString<RawSpineData>(pName);
//            TmpSpineData.json.Scale = SpineSettings.GetScaling(pName); //Set Scaling
//            SkeletonData TmpSkeletonData = TmpSpineData.json.ReadSkeletonData(SpineSettings.DefaultDataPath + pName + ".json"); //Apply Json with Scaling to get skelData
//            return new Skeleton(TmpSkeletonData);
//        }

//        public AnimationState NewAnimationState(SkeletonData pSkeletonData)
//        {
//            AnimationStateData TmpAnimationStateData = new AnimationStateData(pSkeletonData);
//            SpineSettings.SetFadingSettings(TmpAnimationStateData);//Set mixing between animations
//            return new AnimationState(TmpAnimationStateData);
//        }
//        #endregion
//    }
//}
