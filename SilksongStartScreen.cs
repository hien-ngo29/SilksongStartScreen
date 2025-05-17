using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UObject = UnityEngine.Object;
using SFCore;
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
            // SFCore.MenuStyleHelper.AddMenuStyleHook += AddMenuStyle;
            On.MenuStyleTitle.SetTitle += FixMenuTitle;
        }

        public override void Initialize()
        {
            // var tmpStyle = MenuStyles.Instance.styles.First(x => x.styleObject.name.Contains("Silksong_1"));
            // MenuStyles.Instance.SetStyle(MenuStyles.Instance.styles.ToList().IndexOf(tmpStyle), false);
        }

        // private (string languageString, GameObject styleGo, int titleIndex, string unlockKey, string[] achievementKeys,
        //     MenuStyles.MenuStyle.CameraCurves cameraCurves, AudioMixerSnapshot musicSnapshot) AddMenuStyle(
        //         MenuStyles self)
        // {
        //     GameObject styleGo = new GameObject("Silksong_1");
        //     styleGo.SetActive(false);
        //     styleGo.transform.SetParent(self.gameObject.transform);
        //     styleGo.transform.localPosition = new Vector3(0, 0, 0);

        //     SpriteRenderer renderer = styleGo.AddComponent<SpriteRenderer>();
        //     renderer.sprite = LoadSpriteFromResources("img1");

        //     var cameraCurves = new MenuStyles.MenuStyle.CameraCurves
        //     {
        //         saturation = 1.0f,
        //         redChannel = new AnimationCurve(),
        //         greenChannel = new AnimationCurve(),
        //         blueChannel = new AnimationCurve()
        //     };
        //     cameraCurves.redChannel.AddKey(new Keyframe(0f, 0f));
        //     cameraCurves.redChannel.AddKey(new Keyframe(1f, 1f));
        //     cameraCurves.greenChannel.AddKey(new Keyframe(0f, 0f));
        //     cameraCurves.greenChannel.AddKey(new Keyframe(1f, 1f));
        //     cameraCurves.blueChannel.AddKey(new Keyframe(0f, 0f));
        //     cameraCurves.blueChannel.AddKey(new Keyframe(1f, 1f));

        //     styleGo.SetActive(true);

        //     AudioMixerSnapshot audioSnapshot = self.styles[1].musicSnapshot.audioMixer.FindSnapshot("Normal");

        //     return ("UI_MENU_STYLE_RADIANT", styleGo, -1, "", null, cameraCurves, audioSnapshot);
        // }

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
