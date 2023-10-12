using System.IO;
using JaLoader;
using UnityEngine;

namespace BetterEngines
{
    public class MazdaEngine : Mod
    {
        public override string ModID => "BetterEngines"; // The mod's ID. Try making it as unique as possible, to avoid conflicting IDs.
        public override string ModName => "Better Engines"; // The mod's name. This is shown in the mods list. Does not need to be unique.
        public override string ModAuthor => "MeblIkea"; // The mod's author (you). Also shown in the mods list.
        public override string ModDescription => "For having custom engines."; // The mod's description. This is also shown in the mods list, upon clicking on "More Info".
        public override string ModVersion => "1.0.0"; // The mod's version. Also shown in the mods list. If your mod is open-source on GitHub, make sure that you're using the same format as your release tags (for example, 1.0.0)
        public override string GitHubLink => ""; // If your mod is open-source on GitHub, you can link it here to allow for automatic update-checking in-game. It compares the current ModVersion with the tag of the latest release (ex. 1.0.0 compared with 1.0.1)
        public override WhenToInit WhenToInit => WhenToInit.InGame; // When should the mod's OnEnable/Awake/Start/Update functions be called?
        public override bool UseAssets => true; // Does your mod use custom asset bundles?

        public override void SettingsDeclaration() // Declare all of your per-user settings here
        {
            base.SettingsDeclaration();
        }

        public override void CustomObjectsRegistration()
        {
            base.CustomObjectsRegistration();

            var engineObj = LoadAsset("betterengine", "Mazda13B", "");
            var assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsPath, "betterengine"));
            var engineAudio = assetBundle.LoadAsset<AudioClip>("MazdaIdling");
            assetBundle.Unload(false);

            var obj = Instantiate(engineObj);

            ModHelper.Instance.AddBasicObjectLogic(obj, "Malmo 13-Rot", "The Malmo 13-Rot is a Swedish rotary engine produced since 1982. This engine was designed for racing automobiles, and has nothing to do in an everyday car.", 2450, 42, false, true); //in-game name, description, price, weight, can find in crates (does not apply here), can buy in stores (does not apply here)            ModHelper.Instance.AddEnginePartLogic(obj, PartTypes.Engine, 4, true, true);
            ModHelper.Instance.AddEnginePartLogic(obj, PartTypes.Engine, 5, true, true); //durability, can buy in dealership, can find in junk cars (not a feature yet)
            ModHelper.Instance.AdjustCustomObjectPosition(obj, new Vector3(-90, 180, 0), new Vector3(0, -0.3f, 0)); //this adjusts how the object is held when picked up
            ModHelper.Instance.AdjustCustomObjectTrunkPosition(obj, new Vector3(0.1f, 0.2f, 0.1f), new Vector3(-53.2f, -64.1f, 155.2f), new Vector3(4, 2, 3)); //this adjusts how the object sits in the trunk
            obj.GetComponent<EngineComponentC>().engineAudio = engineAudio;
            ModHelper.Instance.ConfigureCustomEngine(obj, 4, 180, false); //acceleration (0-80kmh, in seconds), top speed
            CustomObjectsManager.Instance.RegisterObject(obj, "Malmo13");
        }
        public override void Start()
        {
            base.Start();

            Console.Instance.AddCommand("spawnMalmoEngine", "Spawns the Malmö 13-Rot engine next to the player", nameof(SpawnEngine), this);
        }

        public override void Update()
        {
            base.Update();
        }

        public void SpawnEngine()
        {
            Console.Instance.Log("Spawned engine!");
            CustomObjectsManager.Instance.SpawnObject("Malmo13", ModHelper.Instance.player.transform.position).transform.localScale = new Vector3(160, 200, 170);
        }
    }
}