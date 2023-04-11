using MelonLoader;
using BTD_Mod_Helper;
using EasterBunny;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppSystem;

[assembly: MelonInfo(typeof(EasterBunny.EasterBunny), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace EasterBunny;

public class EasterBunny : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<EasterBunny>("EasterBunny loaded!");
    }
}
public class EasterEggDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "EasterEggDisplay");
    }
}
public class EasterBunnyTower : ModTower
{
    public override TowerSet TowerSet => TowerSet.Magic;
    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 400;
    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 5;
    public override int BottomPathUpgrades => 0;
    public override string Description => "The easter bunny is coming to DESTROY the bloons";

    public override string Icon => "BunnyIcon";

    public override string Portrait => "BunnyIcon";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.display = new() { guidRef = "384e68940e30ed041ac7d60d94bb245f" };
        towerModel.GetBehavior<DisplayModel>().display = new() { guidRef = "384e68940e30ed041ac7d60d94bb245f" };

        towerModel.GetAttackModel().weapons[0] = Game.instance.model.GetTower(TowerType.Sauda).GetWeapon().Duplicate();
        towerModel.GetAttackModel().weapons[0].rate = 0.7f;

        towerModel.range = towerModel.GetAttackModel().range = 30; 
    }
}
public class SharperClaws : ModUpgrade<EasterBunnyTower>
{
    public override string Icon => "SharperClaws";
    public override string Portrait => "BunnyIcon";
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override int Cost => 400;

    public override string Description => "Scratches deal more damage";

    public override void ApplyUpgrade(TowerModel towerModel)
    {

        towerModel.GetAttackModel().weapons[0].projectile.GetDamageModel().damage += 1;
    }
}
public class QuickerSwipes : ModUpgrade<EasterBunnyTower>
{
    public override string Icon => "QuickerSwipes";
    public override string Portrait => "BunnyIcon";
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override int Cost => 650;

    public override string Description => "The bunny can swipe much quicker";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAttackModel().weapons[0].rate -= 0.4f;
    }
}
public class EasterEggs : ModUpgrade<EasterBunnyTower>
{
    public override string Icon => "EasterEggDisplay"; 
    public override string Portrait => "BunnyIcon";
    public override int Path => MIDDLE;
    public override int Tier => 3;
    public override int Cost => 900;

    public override string Description => "Gains a powerful long ranged easter egg attack";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAttackModel().weapons[0].rate -= 0.1f;
        var attack = Game.instance.model.GetTower(TowerType.DartMonkey).GetAttackModel();
        attack.name = "EasterEggAttack";
        attack.range = 100;
        attack.weapons[0].rate = 3f;
        attack.weapons[0].projectile.pierce = 20;
        attack.weapons[0].projectile.GetDamageModel().damage = 20;
        attack.weapons[0].projectile.ApplyDisplay<EasterEggDisplay>();

        towerModel.range = 100;
        towerModel.AddBehavior(attack);
    }
}
public class HeavyEggs : ModUpgrade<EasterBunnyTower>
{
    public override string Icon => "HeavyEggs";
    public override string Portrait => "BunnyIcon";
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override int Cost => 9000;

    public override string Description => "Heavier eggs knock back bloons";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAttackModel(1).weapons[0].projectile.AddBehavior(new WindModel("WindModel", 5, 5, 1, true, ""));
        towerModel.GetAttackModel(1).weapons[0].projectile.GetDamageModel().damage = 50;
        towerModel.GetAttackModel(1).weapons[0].rate -= 0.5f;
        towerModel.GetAttackModel(0).weapons[0].rate -= 0.1f;
        towerModel.GetAttackModel(0).weapons[0].projectile.pierce += 3;
        towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().damage += 1;
    }
}
public class TheKillerBunny : ModUpgrade<EasterBunnyTower>
{
    public override string Icon => "KillerBunny";
    public override string Portrait => "BunnyIcon";
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override int Cost => 35000;

    public override string Description => "The killer bunny of death and destruction and other stuff";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<WindModel>().distanceMax = 25;
        towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<WindModel>().distanceMin = 25;
        towerModel.GetAttackModel(1).weapons[0].projectile.GetDamageModel().damage = 500;
        towerModel.GetAttackModel(1).weapons[0].projectile.AddBehavior(Game.instance.model.GetTower(TowerType.BombShooter, 5, 0, 0).GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());

        towerModel.GetAttackModel(1).weapons[0].rate -= 1f;
        towerModel.GetAttackModel(0).weapons[0].rate -= 0.1f;
        towerModel.GetAttackModel(0).weapons[0].projectile.pierce += 3f;
        towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().damage += 4f;
    }
}