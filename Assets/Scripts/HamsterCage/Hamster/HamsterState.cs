using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HamsterState //: MonoBehaviour
{
    
    private string UUID;
    [Range(1, 5)]
    public uint foodLevel = 5; //0,1,2
    [Range(1, 5)]

    public uint WeightLevel = 1; //0,1,2
    [Range(1, 5)]
    public uint SpeedLevel = 1; //0,1,2
    [Range(1, 5)]
    public uint FrictionLevel = 1;
    [Range(1, 5)]
    public uint TurnSpeedLevel = 1;
    public string HamsterName = "";


    //FOOD FUNCTIONS
    public void IncreaseFoodLevel(uint amountIncrease)
    {
        foodLevel += amountIncrease;
    }

    public void DecreaseFoodLevel(uint amountDecrease)
    {
        foodLevel += amountDecrease;
    }




    public void GenerateUUID() {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
            int z1 = UnityEngine.Random.Range(0, 1000000);
            int z2 = UnityEngine.Random.Range(0, 1000000);
            UUID = currentEpochTime + ":" + z1 + ":" + z2;
    }

    public string getUUID() {
        return UUID;
    }

    public string getRandomName()
    {
        uint index = (uint)UnityEngine.Random.Range(0, 179);
        string LocalHamsterName = names[index];
        return LocalHamsterName;
    }

    private string[] names = {"Ace"
,"Acey"
,"Abrico"
,"Alfy"
,"Archie"
,"Argola"
,"Bram"
,"Bruno"
,"Bubba"
,"Buddy"
,"Buzz"
,"Caesar"
,"Chuck"
,"Cirrus"
,"Cooper"
,"Cotton"
,"Dexter"
,"Dion"
,"Ditto"
,"Dots"
,"Elmo"
,"Gus"
,"Hairy"
,"Happy"
,"Jasper"
,"Jo Jo"
,"Larry"
,"Lucius"
,"Mindy"
,"Muffy"
,"Mugs"
,"Pablo"
,"Pepper"
,"Rex"
,"Ripley"
,"Taz"
,"Teddy"
,"Tiny"
,"Aggy"
,"Amber"
,"Amelia"
,"Agnus"
,"Annie"
,"Apple"
,"April"
,"Ashes"
,"Ashley"
,"Babs"
,"Beans"
,"Bella"
,"Bertha"
,"Bijou"
,"Bitz"
,"Blitz"
,"Bonnie"
,"Boots"
,"Boress"
,"Candis"
,"Catnip"
,"Charm"
,"Cheeks"
,"Cheska"
,"Chili"
,"Chuu"
,"Conrad"
,"Cookie"
,"Curd"
,"Dolly"
,"Erma"
,"Foo  Foo"
,"Ginger"
,"Gretel"
,"Holly"
,"Honey"
,"Lady"
,"Lily"
,"Loulou"
,"Maggy"
,"Mini"
,"Minnie"
,"Pearl"
,"Windie"
,"Ally"
,"Axel"
,"Baby"
,"Bilbo"
,"Buffy"
,"Buster"
,"Button"
,"Cheeky"
,"Chewy"
,"Chip"
,"Chubby"
,"Cindy"
,"Disco"
,"Domino"
,"Ebi"
,"Elvis"
,"Emeril"
,"Flick"
,"Fluffy"
,"Hamlet"
,"Hammy"
,"Hank"
,"Henry"
,"Herman"
,"Jojo"
,"Karma"
,"Kernel"
,"Kitkat"
,"Kiwi"
,"Kobe"
,"Kujo"
,"Latte"
,"Lilly"
,"Lucky"
,"Marble"
,"Mimi"
,"Missy"
,"Mocha"
,"Muffin"
,"Nemo"
,"Niblet"
,"Nugget"
,"Odie"
,"Olly"
,"Oreo"
,"Panda"
,"Pauly"
,"Pedro"
,"Perogy"
,"Pooky"
,"Ringo"
,"Rocky"
,"Shaggy"
,"Shrimp"
,"Skippy"
,"Sleepy"
,"Sparky"
,"Stitch"
,"Taco"
,"Tot"
,"Tippy"
,"Trixie"
,"Tofu"
,"Toffee"
,"Turbo"
,"Uni"
,"Ziggy"
,"Zippy"
,"Boo  Bear"
,"Bunny"
,"Cocoa"
,"Dale"
,"Desert"
,"Echo"
,"Fu-Fu"
,"Fufu"
,"Furry"
,"Fuzzy"
,"Gem"
,"Gin"
,"Guava"
,"Gummie"
,"Hazel"
,"Mickey"
,"Mojo"
,"Mouse"
,"Paws"
,"Peewee"
,"Pepe"
,"Powder"
,"Sweety"
,"Abster"
,"Alfie"
,"Axe"
,"Bigon"
,"Butter"
};


}
