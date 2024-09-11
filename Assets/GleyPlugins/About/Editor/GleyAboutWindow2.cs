namespace GleyPlugins
{
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class GleyAboutWindow2 : EditorWindow
    {
        static GleyAboutWindow2 window;
        static Texture2D gleyIcon;

        private EditorFileLoaded2 fileLoader;
        private AssetVersions2 allAssetsVersion;

        private GUIContent websiteButtonContent;
        private GUIContent twitterButtonContent;
        private GUIContent youtubeButtonContent;
        private GUIContent facebookButtonContent;
        private Texture2D coverImage;
        private Texture2D websiteIcon;
        private Texture2D twitterIcon;
        private Texture2D youtubeIcon;
        private Texture2D facebookIcon;
        private Vector2 scrollPosition;
        private string updateResult;
        private int nrOfUpdates;
        private bool updateCheck;

        AssetStorePackage2[] assetStorePackages =
        {
        new AssetStorePackage2(GleyAssets2.JumpyCompleteGame,"Mobile Tools","MobileToolsIcon.png","All you need to publish your finished game on the store and BONUS a free game with all of them already integrated","https://assetstore.unity.com/packages/tools/integration/mobile-tools-complete-game-132284"),
        new AssetStorePackage2(GleyAssets2.Ads, "Mobile Ads","MobileAdsIcon.png","Show ads inside your game with this easy to use, multiple advertisers support tool.","https://assetstore.unity.com/packages/tools/integration/mobile-ads-gdpr-compliant-102892"),
        new AssetStorePackage2(GleyAssets2.EasyIAP,"Easy IAP","EasyIAPIcon.png","Sell In App products inside your game with minimal setup and very little programming knowledge.", "https://assetstore.unity.com/packages/tools/integration/easy-iap-in-app-purchase-128902"),
        new AssetStorePackage2(GleyAssets2.Notifications,"Mobile Push Notifications","NotificationsIcon.png","Send scheduled offline notifications to your users and keep them engaged.", "https://assetstore.unity.com/packages/tools/integration/mobile-push-notifications-156905"),
        new AssetStorePackage2(GleyAssets2.GameServices,"Easy Achievements and Leaderboards","AchievementsIcon.png","Submit achievements and scores with minimal setup and increase competition between your users.", "https://assetstore.unity.com/packages/tools/integration/easy-achievements-and-leaderboards-118119"),
        new AssetStorePackage2(GleyAssets2.RateGame,"Rate Game Popup","RateGameIcon.png","Increase the number of game ratings by encouraging users to rate your game.", "https://assetstore.unity.com/packages/tools/integration/rate-game-popup-android-ios-139131"),
        new AssetStorePackage2(GleyAssets2.CrossPromo,"Mobile Cross Promo","CrossPromoIcon.png","Easily cross promote and increase popularity for all of your published games.", "https://assetstore.unity.com/packages/tools/integration/mobile-cross-promo-148024"),
        new AssetStorePackage2(GleyAssets2.AllPlatformsSave,"All Platforms Save","SaveIcon.png","Easy to use: same line of code to save or load game data on all supported Unity platforms.", "https://assetstore.unity.com/packages/tools/integration/all-platforms-save-115960"),
        new AssetStorePackage2(GleyAssets2.DeliveryVehiclesPack,"Delivery Vehicles Pack","VehiclesIcon.png","Delivery Vehicles Pack contains 3 low poly, textured vehicles: Scooter, Three Wheeler, Minivan", "https://assetstore.unity.com/packages/3d/vehicles/land/delivery-vehicles-pack-55528")
    };

        GleyAssets2[] packagesInsideMobileTools = { GleyAssets2.Ads, GleyAssets2.EasyIAP, GleyAssets2.GameServices, GleyAssets2.RateGame, GleyAssets2.CrossPromo, GleyAssets2.AllPlatformsSave };

        [MenuItem("Window/Gley/About Gley")]
        private static void Init()
        {
            window = (GleyAboutWindow2)GetWindow(typeof(GleyAboutWindow2));
            window.minSize = new Vector2(600, 520);
            gleyIcon = EditorGUIUtility.Load("Assets/GleyPlugins/About/Editor/Icons/GleyLogo.png") as Texture2D;
            window.titleContent = new GUIContent(" About ", gleyIcon);
            window.Show();
        }

        void OnEnable()
        {
            coverImage = EditorGUIUtility.Load("Assets/GleyPlugins/About/Editor/Icons/GleyCover.png") as Texture2D;
            websiteIcon = EditorGUIUtility.Load("Assets/GleyPlugins/About/Editor/Icons/WebsiteIcon.png") as Texture2D;
            twitterIcon = EditorGUIUtility.Load("Assets/GleyPlugins/About/Editor/Icons/TwitterIcon.png") as Texture2D;
            youtubeIcon = EditorGUIUtility.Load("Assets/GleyPlugins/About/Editor/Icons/YoutubeIcon.png") as Texture2D;
            facebookIcon = EditorGUIUtility.Load("Assets/GleyPlugins/About/Editor/Icons/FacebookIcon.png") as Texture2D;

            websiteButtonContent = new GUIContent(" Website", websiteIcon);
            twitterButtonContent = new GUIContent(" Twitter", twitterIcon);
            youtubeButtonContent = new GUIContent(" Youtube", youtubeIcon);
            facebookButtonContent = new GUIContent(" Facebook", facebookIcon);

            for (int i = 0; i < assetStorePackages.Length; i++)
            {
                assetStorePackages[i].LoadTexture();
            }

            RefreshState();
        }

        void RefreshState()
        {
            nrOfUpdates = 0;
            for (int i = 0; i < assetStorePackages.Length; i++)
            {
                assetStorePackages[i].assetState = GetAssetState(assetStorePackages[i].asset);
            }

            if (nrOfUpdates != 0)
            {
                updateResult = nrOfUpdates + " updates available";
            }
            else
            {
                updateResult = "No updates available";
            }
        }

        void OnGUI()
        {
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.alignment = TextAnchor.UpperCenter;

            GUILayout.Label(coverImage, labelStyle);
            labelStyle.fontStyle = FontStyle.Bold;
            GUILayout.Label("Professional assets made easy to use for everyone", labelStyle);
            EditorGUILayout.Space();

            GUILayout.Label("Connect with us:", labelStyle);
            EditorGUILayout.SelectableLabel("gley.mobi@gmail.com", labelStyle);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(websiteButtonContent))
            {
                Application.OpenURL("https://gley.mobi");
            }
            if (GUILayout.Button(twitterButtonContent))
            {
                Application.OpenURL("https://twitter.com/GleyGames");
            }
            if (GUILayout.Button(youtubeButtonContent))
            {
                Application.OpenURL("https://www.youtube.com/c/gleygames");
            }
            if (GUILayout.Button(facebookButtonContent))
            {
                Application.OpenURL("https://www.youtube.com/c/gleygames");
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            if (GUILayout.Button("Open Asset Store Publisher Page"))
            {
                Application.OpenURL("https://assetstore.unity.com/publishers/19336");
            }
            EditorGUILayout.Space();

            if (updateCheck == false)
            {
                if (GUILayout.Button("Check for Updates"))
                {
                    updateCheck = true;
                    LoadFile();
                }
            }
            else
            {
                GUILayout.Label(updateResult, labelStyle);
            }
            EditorGUILayout.Space();

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height - 220));

            DrawPackages();

            GUILayout.EndScrollView();
        }

        void DrawPackages()
        {
            bool mobileToolsAvailable = (assetStorePackages[0].assetState != AssetState2.NotDownloaded);
            for (int i = 0; i < assetStorePackages.Length; i++)
            {
                if(mobileToolsAvailable)
                {
                    if(!packagesInsideMobileTools.Contains(assetStorePackages[i].asset))
                    {
                        DrawPack(assetStorePackages[i]);
                    }
                }
                else
                {
                    DrawPack(assetStorePackages[i]);
                }
            }
        }

        AssetState2 GetAssetState(GleyAssets2 assetToCheck)
        {
            AssetState2 result = AssetState2.InProject;

            if (!AssetDatabase.IsValidFolder("Assets/GleyPlugins/" + assetToCheck + "/Scripts"))
            {
                return AssetState2.NotDownloaded;
            }

            if (!File.Exists(Application.dataPath + "/GleyPlugins/" + assetToCheck + "/Scripts/Version.txt"))
            {
                nrOfUpdates++;
                return AssetState2.UpdateAvailable;
            }

            if (allAssetsVersion != null)
            {
                if (AssetNeedsUpdate(assetToCheck))
                {
                    nrOfUpdates++;
                    return AssetState2.UpdateAvailable;
                }
            }

            return result;
        }

        private bool AssetNeedsUpdate(GleyAssets2 assetToCheck)
        {
            string path = "Assets//GleyPlugins/" + assetToCheck + "/Scripts/Version.txt";
            StreamReader reader = new StreamReader(path);
            int localVersion = JsonUtility.FromJson<AssetVersion2>(reader.ReadToEnd()).shortVersion;
            int serverVersion = allAssetsVersion.assetsVersion.First(cond => cond.assetName == assetToCheck).shortVersion;
            reader.Close();
            if (localVersion != serverVersion)
            {
                return true;
            }
            return false;
        }

        void DrawPack(AssetStorePackage2 pack)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.Space();
            GUIStyle style = new GUIStyle();
            style.fontSize = 18;
            style.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(pack.texture, style);
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            GUILayout.Label(pack.name, style);
            style.fontSize = 12;
            style.wordWrap = true;
            //style.normal.background = downloadColor;
            GUILayout.Label(pack.description, style);
            EditorGUILayout.EndVertical();
            var oldColor = GUI.backgroundColor;
            string buttonText = "";
            switch (pack.assetState)
            {
                case AssetState2.ComingSoon:
                    GUI.backgroundColor = new Color32(190, 190, 190, 255);
                    buttonText = "Coming Soon";
                    break;
                case AssetState2.InProject:
                    GUI.backgroundColor = new Color32(253, 195, 71, 255);
                    buttonText = "Owned";
                    break;
                case AssetState2.NotDownloaded:
                    GUI.backgroundColor = new Color32(42, 180, 240, 255);
                    buttonText = "Download";
                    break;
                case AssetState2.UpdateAvailable:
                    GUI.backgroundColor = new Color32(76, 229, 89, 255);
                    buttonText = "Update Available";
                    break;
            }

            if (GUILayout.Button(buttonText, GUILayout.Width(130), GUILayout.Height(64)))
            {
                updateCheck = false;
                Application.OpenURL(pack.url);
            }
            GUI.backgroundColor = oldColor;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        private void LoadFile()
        {
            updateResult = "Connecting to server";
            fileLoader = new EditorFileLoaded2();
            string url = "https://gley.mobi/versions/AssetVersions.txt";
            fileLoader.LoadFile(url);
            EditorApplication.update = MyUpdate;
        }

        private void MyUpdate()
        {
            if (fileLoader.IsDone())
            {
                EditorApplication.update = null;
                LoadCompleted();
            }
        }

        private void LoadCompleted()
        {
            allAssetsVersion = JsonUtility.FromJson<AssetVersions2>(fileLoader.GetResult());
            RefreshState();
        }
    }
}
