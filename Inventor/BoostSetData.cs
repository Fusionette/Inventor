using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Drawing;
using System.Resources;
using System.IO;

namespace Inventor
{
	class AttribMod
	{
		public string name;
		public string table;
		public string aspect;
        public string attrib;
        public string target;
        public string scale;
        public string allowResistance;
        public string duration;
        public string chance;
        public string cancelOnMiss;
        public string boostTemplate;
        public string displayAttackerHit;
        public string displayVictimHit;
        public string stackType;
        public string requires;
        public string reward;
        public string procsPerMinute;
        public string allowCombatMods;
        public string boostModAllowed;
    }

	class BoostType
	{
		public string displayName { get; set; }
		public string description;
		public string shortHelp;
		public List<List<AttribMod>> attribMods;
	}

    class SetGroup
    {
        public string displayName { get; set; }
        public string groupName;
        public string boostsAllowed;
        public string conversionGroup;
        public string include;
    }

    class SetBonus
    {
        public string fullName;
        public string displayName;
        public string displayHelp;
        public string displayShortHelp;

        public string displayList
        {
            get
            {
                return this.fullName.Substring(this.fullName.Length - 1) + ": " + this.displayName.Substring(this.displayName.IndexOf(" ") + 1);
            }
        }
    }

    class Bonus
	{
		[JsonIgnore] public string listDisplay { get { return minBoosts + "-" + maxBoosts + " " + autoPowers; } }
		public int minBoosts;
		public int maxBoosts;
		public string autoPowers;
		public string requires;
	}

	class Boost
	{
		[JsonIgnore] public string listDisplay { get { return letter + ": " + displayName; } }
		public string letter;
		public string name;
		public string displayName;
		public string description;
		public string shortHelp;
		public string slotRequires;
		public List<BoostType> aspects;
		public List<string> salvage;
	}

	class BoostSetData
	{
		public string name;
		public string displayName;
		public string iconName;
		public SetGroup setGroup;
		public string conversionGroups;
		public int minLevel;
		public int maxLevel;
		public int minSlotLevel;
		public string boostsAllowed;
		public List<Boost> boostList;
		public List<Bonus> bonusList;
		public string meritTabName;
		public string rarity;
		public string sku;

        [JsonIgnore] public PString pstring = new PString();
        [JsonIgnore] public Recipes recipes = new Recipes();
        [JsonIgnore] public ProductCatalog catalog = new ProductCatalog();
        [JsonIgnore] public BoostSets boostsets = new BoostSets();

        ResourceManager resourceManager = Properties.Resources.ResourceManager;

		public string GetProductCatalog()
		{
			foreach (Boost boost in boostList)
			{
                string s = "\tTitle <<" + displayName + ": " + boost.displayName + ">>" + Environment.NewLine;
				s += "\tSKU " + sku + boost.letter + Environment.NewLine;
				s += "\tInventoryType Voucher" + Environment.NewLine;
				s += "\tInventoryCount 1" + Environment.NewLine;
				s += "\tGlobal 1" + Environment.NewLine;
				s += "\tItemKey Alt_" + sku.ToLower() + boost.letter.ToLower() + Environment.NewLine;
                catalog.Add(sku + boost.letter, s);
			}
			return catalog.ToString();
		}

		public string GetDropRecipe(List<int> craftingCost, List<Salvage> salvageList)
		{
			foreach (Boost boost in boostList)
			{
                for (int level = minLevel; level <= maxLevel; level++)
				{
                    string s = "    DisplayName                    \"" + pstring.Add(displayName + ": " + boost.displayName + " (Recipe)") + "\"" + Environment.NewLine;
					s += "    DisplayHelp                    \"" + pstring.Add("This recipe builds the " + displayName + ": " + boost.displayName + " Enhancement") + "\"" + Environment.NewLine;
					s += "    DisplayTabName                 \"P1273912828\"" + Environment.NewLine;
					s += "    Type                           Drop" + Environment.NewLine;
					s += "    Workshop                       Worktable_Invention" + Environment.NewLine;
					s += "    Icon                           " + iconName + Environment.NewLine;
					s += "    Level                          " + level + Environment.NewLine;
					s += "    Rarity                         " + rarity + Environment.NewLine;
					s += "    MaxInvAmount                   100" + Environment.NewLine;
					s += "    NumUses                        1" + Environment.NewLine;
					s += "    CreationCost                   " + craftingCost[level - 1] + Environment.NewLine;
					s += "    SellToVendor                   " + (level * 200) + Environment.NewLine;
                    s += "     " + Environment.NewLine;
                    s += "    CreatesEnhancement             1" + Environment.NewLine;
                    s += "    EnhancementReward              Boosts.Crafted_" + boost.name + ".Crafted_" + boost.name + Environment.NewLine;
                    s += "     " + Environment.NewLine;
                    if (boost.salvage != null)
					{
						foreach (string sid in boost.salvage)
						{
							Salvage salvage = salvageList.Find(x => x.name.Equals(sid));
							if (salvage.level == Salvage.Level.Low && level < 26) s += "    SalvageComponent 1 " + sid + Environment.NewLine;
							else if (salvage.level == Salvage.Level.Mid && level > 25 && level < 41) s += "    SalvageComponent 1 " + sid + Environment.NewLine;
							else if (salvage.level == Salvage.Level.High && level > 40) s += "    SalvageComponent 1 " + sid + Environment.NewLine;
						}
					}
                    recipes.Add(boost.name + "_" + level, s);
                }
			}
            return recipes.ToString();
		}

		public string GetMeritsRecipe()
		{
			foreach (Boost boost in boostList)
			{
                string s = "    DisplayName                    \"" + pstring.Add(displayName + ": " + boost.displayName + " (Recipe)") + "\"" + Environment.NewLine;
			    s += "    DisplayHelp                    \"P1426840851\"" + Environment.NewLine;
				s += "    DisplayTabName                 \"" + pstring.Add(meritTabName) + "\"" + Environment.NewLine;
				s += "    Type                           Workshop" + Environment.NewLine;
				s += "    Workshop                       Worktable_Merit" + Environment.NewLine;
				s += "    Icon                           " + iconName + Environment.NewLine;
				s += "    LevelMin                       " + minLevel + Environment.NewLine;
				s += "    LevelMax                       " + maxLevel + Environment.NewLine;
				s += "    Rarity                         " + rarity + Environment.NewLine;
				s += "    MaxInvAmount                   1" + Environment.NewLine;
				s += "    NumUses                        1" + Environment.NewLine;
				s += "    SellToVendor                   100" + Environment.NewLine;
                s += "    Flags                          NoGenericBadgeCredit" + Environment.NewLine;
                s += "     " + Environment.NewLine;
                s += "    CreatesRecipe                  1" + Environment.NewLine;
                s += "    RecipeReward                   " + boost.name + Environment.NewLine;
                s += "     " + Environment.NewLine;
                if (rarity == "Uncommon") s += "    SalvageComponent 20 S_MeritReward" + Environment.NewLine;
				else if (rarity == "Rare") s += "    SalvageComponent 50 S_MeritReward" + Environment.NewLine;
				else if (rarity == "VeryRare") s += "    SalvageComponent 100 S_MeritReward" + Environment.NewLine;
                recipes.Add("Merit_" + boost.name, s);
            }
            return recipes.ToString();
        }

        public string GetStoreRecipe()
		{
			foreach (Boost boost in boostList)
			{
	            string s = "    DisplayName                    \"" + pstring.Add(displayName + ": " + boost.displayName) + "\"" + Environment.NewLine;
				s += "    DisplayHelp                    \"P1426840851\"" + Environment.NewLine;
				s += "    DisplayTabName                 \"P2968462820\"" + Environment.NewLine;
				s += "    Type                           Workshop" + Environment.NewLine;
				s += "    Workshop                       Worktable_Alt" + Environment.NewLine;
				s += "    Icon                           " + iconName + Environment.NewLine;
				s += "    Level                          1" + Environment.NewLine;
				s += "    Rarity                         " + rarity + Environment.NewLine;
				s += "    MaxInvAmount                   1" + Environment.NewLine;
				s += "    NumUses                        1" + Environment.NewLine;
				s += "    SellToVendor                   100" + Environment.NewLine;
                s += "    Flags                          NoGenericBadgeCredit, Voucher" + Environment.NewLine;
                s += "     " + Environment.NewLine;
                s += "    CreatesEnhancement             1" + Environment.NewLine;
                s += "    EnhancementReward              Boosts.Attuned_" + boost.name + ".Attuned_" + boost.name + Environment.NewLine;
                s += "     " + Environment.NewLine;
                recipes.Add("Alt_" + sku.ToLower() + boost.letter.ToLower(), s);
            }
            return recipes.ToString();
        }

        public string GetBoostSetDef()
		{
			string s = "\tName " + name + Environment.NewLine;
			s += "\tDisplayName " + pstring.Add(displayName) + Environment.NewLine;
			s += "\tGroupName " + setGroup.groupName + Environment.NewLine;
			s += "\tConversionGroups " + conversionGroups + Environment.NewLine;
			s += "\tMinLevel " + minLevel + Environment.NewLine;
			s += "\tMaxLevel " + maxLevel + Environment.NewLine;
			s += "\tinclude defs/boostsets/" + setGroup.include + ".powerinc" + Environment.NewLine;
			if (boostList != null)
			{
				foreach (Boost boost in boostList)
				{
					s += "\tBoostLists" + Environment.NewLine;
					s += "\t{" + Environment.NewLine;
					s += "\t\tBoosts Boosts.Crafted_" + boost.name + ".Crafted_" + boost.name + Environment.NewLine;
					s += "\t\tBoosts Boosts.Attuned_" + boost.name + ".Attuned_" + boost.name + Environment.NewLine;
					s += "\t}" + Environment.NewLine;
				}
			}
			if (bonusList != null)
			{
				foreach (Bonus bonus in bonusList)
				{
					s += "\tBonuses" + Environment.NewLine;
					s += "\t{" + Environment.NewLine;
					s += "\t\tMinBoosts " + bonus.minBoosts + Environment.NewLine;
					s += "\t\tMaxBoosts " + bonus.maxBoosts + Environment.NewLine;
					s += "\t\tAutoPowers " + bonus.autoPowers + Environment.NewLine;
					if (!string.IsNullOrEmpty(bonus.requires)) s += "\t\tRequires " + bonus.requires + Environment.NewLine;
					s += "\t}" + Environment.NewLine;
				}
			}
            boostsets.Add(name, s);
            return boostsets.ToString();
		}

		public string GetBoostsCategories(List<string> categories)
		{
            if (boostList != null)
            {
                string[] prefixes = new string[2] { "Boosts.Crafted_", "Boosts.Attuned_" };
                foreach (string prefix in prefixes)
                {
                    foreach (Boost boost in boostList)
                    {
                        if (categories.Contains(prefix + boost.name) == false)
                        {
                            categories.Add(prefix + boost.name);
                        }
                    }
                }
            }

            string s = "PowerCategory Boosts" + Environment.NewLine;
            s += "{" + Environment.NewLine;
            s += "    DisplayName \"P1604142960\"" + Environment.NewLine;
            foreach (string ps in categories)
            {
                s += "    PowerSets " + ps + Environment.NewLine;
            }
            s += Environment.NewLine + "}" + Environment.NewLine;
            return s;
        }

        public string GetBoostsPowerSet(Boost boost, string prefix)
		{
			string s = "PowerSet Boosts." + prefix + boost.name + Environment.NewLine;
			s += "{" + Environment.NewLine;
			s += "    Name \"" + prefix + boost.name + "\"" + Environment.NewLine;
			s += "    DisplayName \"" + pstring.Add(prefix.Replace("_", " ") + boost.name.Replace("_", " ")) + "\"" + Environment.NewLine;
			s += "    DisplayShortHelp \"" + pstring.Add("Set: " + displayName) + "\"" + Environment.NewLine;
			s += "    DisplayHelp \"" + pstring.Add(boost.description) + "\"" + Environment.NewLine;
			s += "    IconName \"" + iconName + "\"" + Environment.NewLine + Environment.NewLine;
			s += "    Powers Boosts." + prefix + boost.name + "." + prefix + boost.name + Environment.NewLine;
			s += "    Available 0" + Environment.NewLine;
			s += "    AIMaxLevel 0" + Environment.NewLine;
			s += "    AIMinRankCon -9999" + Environment.NewLine;
			s += "    AIMaxRankCon 9999" + Environment.NewLine;
			s += "    MinDifficulty 2" + Environment.NewLine;
			s += "    MaxDifficulty 9999" + Environment.NewLine + Environment.NewLine;
			s += "}" + Environment.NewLine;
			return s;
		}

		public Bitmap GetIcon(Config cfg, string border)
		{
			Bitmap icon = (Bitmap)resourceManager.GetObject(border);
			icon.SetResolution(96, 96);

			Graphics graphics = Graphics.FromImage(icon);

			string pog = (setGroup != null) ? setGroup.boostsAllowed : String.Empty;
			if (!String.IsNullOrEmpty(pog))
			{
				if (pog.IndexOf(",") > 0) pog = pog.Substring(0, pog.IndexOf(",")).Trim();
				Bitmap iconPog = new Bitmap((Bitmap)resourceManager.GetObject(pog));
				if (iconPog != null)
				{
					iconPog.SetResolution(96, 96);
					graphics.DrawImage(iconPog, 8, 8);
				}
			}

			if (File.Exists(cfg.images + iconName + ".png"))
			{
				Bitmap iconFace = new Bitmap(cfg.images + iconName + ".png");
				if (iconFace != null)
				{
					iconFace.SetResolution(96, 96);
					iconFace = new Bitmap(iconFace);
					graphics.DrawImage(iconFace, 9, 9);
				}
			}

			return icon;
		}

		public string GetPowers(Boost boost, bool attuned, string prefix, string catalyzed)
		{
			int attribCount = boost.aspects.Count - 1;
			string s = "Power Boosts." + prefix + boost.name + "." + prefix + boost.name + Environment.NewLine;
			s += "{" + Environment.NewLine;
			s += "    Name                 \"" + prefix + boost.name + "\"" + Environment.NewLine;
			s += "    DisplayName          \"" + pstring.Add(displayName + ": " + boost.displayName) + "\"" + Environment.NewLine;
			s += "    Type                 kBoost" + Environment.NewLine;
			s += "    Accuracy             1" + Environment.NewLine;
			s += "    EntsAffected         kCaster" + Environment.NewLine;
			s += "    EntsAutoHit          kCaster" + Environment.NewLine;
			s += "    Target               kCaster" + Environment.NewLine;
			s += "    ActivatePeriod       10" + Environment.NewLine;
			s += "    EffectArea           kCharacter" + Environment.NewLine;
			s += "    BoostsAllowed        kScience_Boost, kTechnology_Boost, kMagic_Boost, kMutation_Boost, kNatural_Boost" + (String.IsNullOrEmpty(boostsAllowed) ? String.Empty : ", " + boostsAllowed) + Environment.NewLine;
            if (!String.IsNullOrEmpty(catalyzed)) s += "    BoostCatalystConversion Boosts." + catalyzed + boost.name + "." + catalyzed + boost.name + Environment.NewLine;
            s += "    " + (attuned ? "BoostUsePlayerLevel  " : "BoostBoostable       ") + "kTrue" + Environment.NewLine;
			s += "    BoostIgnoreEffectiveness kTrue" + Environment.NewLine;
            if (rarity == "VeryRare") s += "    BoostAlwaysCountForSet kTrue" + Environment.NewLine;
            if (attuned) s += "    BoostLicenseLevel    0" + Environment.NewLine;
            s += "    BoostCombinable      kFalse" + Environment.NewLine;
			s += "    MinSlotLevel         " + (minSlotLevel - 1) + Environment.NewLine;
            if (!String.IsNullOrEmpty(boost.slotRequires)) s += "    SlotRequires " + boost.slotRequires + Environment.NewLine;
			s += "    DisplayShortHelp     \"" + pstring.Add(boost.shortHelp) + "\"" + Environment.NewLine;
			s += "    DisplayHelp          \"" + pstring.Add(boost.description) + "\"" + Environment.NewLine;
			s += "    IconName             \"" + iconName + "\"" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

			foreach (BoostType aspect in boost.aspects)
			{
				foreach (AttribMod attrib in aspect.attribMods[attribCount])
				{
					s += "    AttribMod" + Environment.NewLine;
					s += "    {" + Environment.NewLine;
					s += "        Name                 \"" + attrib.name + "\"" + Environment.NewLine;
					s += "        Table                \"" + attrib.table + "\"" + Environment.NewLine;
					s += "        Aspect               " + (String.IsNullOrEmpty(attrib.aspect) ? "kStr" : attrib.aspect) + Environment.NewLine;
					s += "        Attrib               " + attrib.attrib + Environment.NewLine;
					s += "        Target               " + (String.IsNullOrEmpty(attrib.target) ? "kSelf" : attrib.target) + Environment.NewLine;
                    s += "        Scale                " + attrib.scale + Environment.NewLine;
					s += "        Type                 kMagnitude" + Environment.NewLine;
					s += "        AllowStrength        kFalse" + Environment.NewLine;
					s += "        AllowResistance      " + (String.IsNullOrEmpty(attrib.allowResistance) ? "kFalse" : attrib.allowResistance) + Environment.NewLine;
					s += "        Delay                0" + Environment.NewLine;
					s += "        Duration             " + (String.IsNullOrEmpty(attrib.duration) ? "10.25" : attrib.duration) + Environment.NewLine;
					s += "        Magnitude            1" + Environment.NewLine;
					s += "        Period               0" + Environment.NewLine;
					s += "        Chance               " + (String.IsNullOrEmpty(attrib.chance) ? "1" : attrib.chance) + Environment.NewLine;
                    s += "        NearGround           kFalse" + Environment.NewLine;
					s += "        CancelOnMiss         " + (String.IsNullOrEmpty(attrib.cancelOnMiss) ? "kFalse" : attrib.cancelOnMiss) + Environment.NewLine;
                    s += "        BoostTemplate        " + (String.IsNullOrEmpty(attrib.boostTemplate) ? "kTrue" : attrib.boostTemplate) + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.displayAttackerHit)) s += "        DisplayAttackerHit   \"" + pstring.Add(attrib.displayAttackerHit) + "\"" + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.displayVictimHit)) s += "        DisplayVictimHit     \"" + pstring.Add(attrib.displayVictimHit) + "\"" + Environment.NewLine;
                    s += "        StackType            " + (String.IsNullOrEmpty(attrib.stackType) ? "kReplace" : attrib.stackType) + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.requires)) s += "        Requires             " + attrib.requires + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.reward)) s += "        Reward               " + attrib.reward + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.procsPerMinute)) s += "        ProcsPerMinute       " + attrib.procsPerMinute + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.allowCombatMods)) s += "        AllowCombatMods      " + attrib.allowCombatMods + Environment.NewLine;
                    if (!String.IsNullOrEmpty(attrib.boostModAllowed)) s += "        BoostModAllowed      " + attrib.boostModAllowed + Environment.NewLine;
                    s += "    }" + Environment.NewLine;
				}
			}

			s += "}" + Environment.NewLine;
			return s;
		}
	}
}
