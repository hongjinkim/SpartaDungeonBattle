using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using SpartaDungeonBattle.Class;


namespace SpartaDungeonBattle
{
    public class SaveManager
    {
        
 
        // 저장
        public static void SaveGame(GameManager gm)
        {
            //세이브 파일 저장 경로
            string path = Directory.GetCurrentDirectory() + "/save/";

            string playerJson = JsonSerializer.Serialize(gm.player);
            string inventoryJson = JsonSerializer.Serialize(gm.inventory);
            string productsJson = JsonSerializer.Serialize(gm.products);
            string potionJson = JsonSerializer.Serialize(gm.potion);
            string questJson = JsonSerializer.Serialize(gm.quests);


            File.WriteAllText(path + "player.json", playerJson);
            File.WriteAllText(path + "inventory.json", inventoryJson);
            File.WriteAllText(path + "products.json", productsJson);
            File.WriteAllText(path + "potion.json", potionJson);
            File.WriteAllText(path + "quest.json", questJson);
        }
        // 불러오기2

        public static void LoadGame(GameManager gm)
        {
            //세이브 파일 저장 경로
            string path = Directory.GetCurrentDirectory() + "/save/";

            string playerJson = File.ReadAllText(path + "player.json");
            string inventoryJson = File.ReadAllText(path + "inventory.json");
            string productsJson = File.ReadAllText(path + "products.json");
            string potionJson = File.ReadAllText(path + "potion.json");
            string questJson = File.ReadAllText(path + "quest.json");

            gm.player = JsonSerializer.Deserialize<Player>(playerJson);
            gm.inventory = JsonSerializer.Deserialize<List<EquipItem>>(inventoryJson);
            gm.products = JsonSerializer.Deserialize<List<EquipItem>>(productsJson);
            gm.potion = JsonSerializer.Deserialize<List<IItem>>(potionJson);
            gm.quests = JsonSerializer.Deserialize<List<Quest>>(questJson);
        }
    }
}
