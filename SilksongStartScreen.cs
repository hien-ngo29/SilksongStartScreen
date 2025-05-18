using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UObject = UnityEngine.Object;
using System.IO;
using System.Reflection;
using System.Linq;

namespace SilksongStartScreen
{
    public class SilksongStartScreen : Mod
    {
        public override string GetVersion() => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public SilksongStartScreen() : base("Silksong Start Screen")
        {
            On.MenuStyleTitle.SetTitle += FixMenuTitle;
        }

        private void FixMenuTitle(On.MenuStyleTitle.orig_SetTitle orig, MenuStyleTitle self, int index)
        {
            MenuStyleTitle.TitleSpriteCollection spriteCollection =
                index < 0 || index >= self.TitleSprites.Length
                    ? self.DefaultTitleSprite
                    : self.TitleSprites[index];

            Texture2D RealTitle_texture = new Texture2D(1, 1);
            using (Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("SilksongStartScreen.Resources.SilksongLogo.png"))
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                RealTitle_texture.LoadImage(bytes, false);
                RealTitle_texture.name = "SilksongLogo";
            }

            var RealTitle = Sprite.Create(RealTitle_texture,
                new Rect(0, 0, RealTitle_texture.width, RealTitle_texture.height),
                new Vector2(0.5f, 0.5f), spriteCollection.Default.pixelsPerUnit, 0, SpriteMeshType.FullRect);

            self.Title.sprite = RealTitle;
        }
    }
}
