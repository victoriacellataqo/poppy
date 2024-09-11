namespace GleyPlugins
{
    [System.Serializable]
    public class AssetVersion2
    {
        public GleyAssets2 assetName;
        public string longVersion;
        public int shortVersion;

        public AssetVersion2(GleyAssets2 assetName)
        {
            this.assetName = assetName;
        }
    }
}

