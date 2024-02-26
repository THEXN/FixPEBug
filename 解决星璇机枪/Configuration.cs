using System;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TShockAPI;

namespace fixbugpe
{
    public class Configuration
    {
        public static readonly string FilePath = Path.Combine(TShock.SavePath, "解决PE锤子喝药等相关bug.json");
        // 你可以在这里添加你希望排除的物品信息
        private static readonly int[] DefaultExemptItemList = { 205, 206, 207, 1128 };

        [JsonProperty("免检测物品列表")]
        public int[] ExemptItemList { get; set; }

        [JsonProperty("是否杀死")]
        public bool KillPlayerOnUse { get; set; } = true;

        [JsonProperty("是否上buff")]
        public bool ApplyBuffOnUse { get; set; } = false;

        [JsonProperty("上什么buff")]
        public int[] BuffTypes { get; set; } = { 163, 149 , 23 , 164 };

        [JsonProperty("是否踢出")]
        public bool KickPlayerOnUse { get; set; } = false;

        public Configuration()
        {
            ExemptItemList = DefaultExemptItemList;
        }

        public void Write(string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                var str = JsonConvert.SerializeObject(this, Formatting.Indented);
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(str);
                }
            }
        }

        public static Configuration Read(string path)
        {
            if (!File.Exists(path))
                return new Configuration();
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    var cf = JsonConvert.DeserializeObject<Configuration>(sr.ReadToEnd());
                    return cf;
                }
            }
        }
    }
}
