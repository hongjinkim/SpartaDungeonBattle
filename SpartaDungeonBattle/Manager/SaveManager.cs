using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SpartaDungeonBattle
{
    public class SaveManager
    {
        //세이브 파일 저장 경로
        string path = Directory.GetCurrentDirectory() + "/save/";
 
        // 저장
        public void SaveGame(GameManager gm)
        {
            string playerJson = JsonSerializer.Serialize(gm.player);
            string inventoryJson = JsonSerializer.Serialize(gm.inventory);
            string productsJson = JsonSerializer.Serialize(gm.products);

            File.WriteAllText(path + "player.json", playerJson);
            File.WriteAllText(path + "inventory.json", inventoryJson);
            File.WriteAllText(path + "products.json", productsJson);

        }
        // 불러오기
        public void LoadGame(GameManager gm)
        {
            string playerJson = File.ReadAllText(path + "player.json");
            string inventoryJson = File.ReadAllText(path + "inventory.json");
            string productsJson = File.ReadAllText(path + "products.json");

            gm.player = JsonSerializer.Deserialize<Player>(playerJson);
            gm.inventory = JsonSerializer.Deserialize<List<EquipItem>>(inventoryJson);
            gm.products = JsonSerializer.Deserialize<List<EquipItem>>(productsJson);
        }
    }
}
