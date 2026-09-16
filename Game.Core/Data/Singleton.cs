using Microsoft.Xna.Framework;

namespace withLuckAndWisdomProject
{
    //apply for singleton design pattern
    class Singleton
    {
        public bool IsMouseVisible = true;
        public string ContentRootDir = "Content";
        public readonly int ScreenHeight = 720;
        public readonly int ScreenWidth = 1280;
        public float SFXVolume = 1f;
        public float BGMVolume = 1f;

        public bool IsEnableSFX = true;
        public bool IsEnableBGM = true;
        public bool IsEnableAimer = true;
        public bool IsShareDataToDev = false;

        // Virtual resolution is the design resolution (Desktop 1280x720).
        // On mobile the real backbuffer differs, so we letterbox-scale.
        public Matrix ScaleMatrix = Matrix.Identity;
        public float ViewScale = 1f;
        public float ViewOffsetX = 0f;
        public float ViewOffsetY = 0f;
        public int ViewportWidth = 1280;
        public int ViewportHeight = 720;

        public void UpdateViewport(int realWidth, int realHeight)
        {
            if (realWidth <= 0 || realHeight <= 0) return;
            ViewportWidth = realWidth;
            ViewportHeight = realHeight;
            ViewScale = System.Math.Min((float)realWidth / ScreenWidth, (float)realHeight / ScreenHeight);
            if (ViewScale <= 0f) ViewScale = 1f;
            ViewOffsetX = (realWidth - ScreenWidth * ViewScale) / 2f;
            ViewOffsetY = (realHeight - ScreenHeight * ViewScale) / 2f;
            ScaleMatrix = Matrix.CreateTranslation(-ViewOffsetX, -ViewOffsetY, 0f)
                * Matrix.CreateScale(1f / ViewScale, 1f / ViewScale, 1f);
            // NOTE: ScaleMatrix maps device pixels -> virtual pixels.
            // For SpriteBatch (virtual -> device) use GetRenderScaleMatrix().
        }

        public Matrix GetRenderScaleMatrix()
        {
            return Matrix.CreateScale(ViewScale, ViewScale, 1f)
                * Matrix.CreateTranslation(ViewOffsetX, ViewOffsetY, 0f);
        }

        public Vector2 ToVirtual(Vector2 devicePixels)
        {
            if (ViewScale <= 0f) return devicePixels;
            return new Vector2((devicePixels.X - ViewOffsetX) / ViewScale, (devicePixels.Y - ViewOffsetY) / ViewScale);
        }

        //Base of singleton
        private static Singleton s_instance;

        //Constructor
        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new Singleton();
                }
                return s_instance;
            }
        }
    }
}
