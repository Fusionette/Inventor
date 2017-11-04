using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Force.Crc32;
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
		public string scale;
		public string reward;
	}

	class BoostType
	{
		public string displayName { get; set; }
		public string description;
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
		public List<BoostType> aspects;
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

		Dictionary<string, string> pstringCache = new Dictionary<string, string>();
		ResourceManager resourceManager = Properties.Resources.ResourceManager;

		public string PString(string s)
		{
			string ps = "P" + Crc32Algorithm.Compute(Encoding.ASCII.GetBytes(s));
			if (!pstringCache.ContainsKey(ps))
			{
				pstringCache.Add(ps, s);
			}
			return ps;
		}

		public string DumpCache()
		{
			string s = String.Empty;
			foreach (KeyValuePair<string, string> entry in pstringCache)
			{
				s += "\"" + entry.Key + "\" \"" + entry.Value + "\"\n";
			}
			pstringCache.Clear();
			return s;
		}

		public string GetProductCatalog()
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				s += "CatalogItem\n";
				s += "\tTitle <<" + displayName + ": " + boost.displayName + ">>\n";
				s += "\tSKU " + sku + boost.letter + "\n";
				s += "\tInventoryType Voucher\n";
				s += "\tInventoryCount 1\n";
				s += "\tGlobal 1\n";
				s += "\tItemKey Alt_" + sku.ToLower() + boost.letter.ToLower() + "\n";
				s += "End\n\n";
			}
			return s;
		}

		public string GetDropRecipe(List<int> craftingCost)
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				for (int level = minLevel; level <= maxLevel; level++)
				{
					s += "DetailRecipe " + boost.name + "_" + level + "\n{\n";
					s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName) + "\"\n";
					s += "\tDisplayHelp \"" + PString("This recipe builds the " + displayName + ": " + boost.displayName + " Enhancement") + "\"\n";
					s += "\tDisplayTabName \"P1273912828\"\n";
					s += "\tType Drop\n\tWorkshop Worktable_Invention\n";
					s += "\tEnhancementReward Boosts.Crafted_" + boost.name + ".Crafted_" + boost.name + "\n";
					s += "\tIcon " + iconName + "\n";
					s += "\tLevel " + level + "\n";
					s += "\tRarity " + rarity + "\n";
					s += "\tMaxInvAmount 100\n\tNumUses 1\n";
					s += "\tCreationCost " + craftingCost[level - 1] + "\n";
					s += "\tSellToVendor " + (level * 200) + "\n";
					s += "\tCreatesEnhancement 1\n\n";
					s += "}\n\n";
				}
			}
			return s;
		}

		public string GetMeritsRecipe()
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				s += "DetailRecipe Merit_" + boost.name + "\n{\n";
				s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName + " (Recipe)") + "\"\n";
				s += "\tDisplayHelp \"P1426840851\"\n";
				s += "\tDisplayTabName \"" + PString(meritTabName) + "\"\n";
				s += "\tType Workshop\n\tWorkshop Worktable_Merit\n";
				s += "\tRecipeReward " + boost.name + "\n";
				s += "\tIcon " + iconName + "\n";
				s += "\tLevelMin " + minLevel + "\n";
				s += "\tLevelMax " + maxLevel + "\n";
				s += "\tRarity " + rarity + "\n";
				s += "\tMaxInvAmount 1\n\tNumUses 1\n\tSellToVendor 100\n\tCreatesRecipe 1\n\tFlags NoGenericBadgeCredit\n";
				if (rarity == "Uncommon") s += "\n\tSalvageComponent 20 S_MeritReward\n";
				else if (rarity == "Rare") s += "\n\tSalvageComponent 50 S_MeritReward\n";
				else if (rarity == "VeryRare") s += "\n\tSalvageComponent 100 S_MeritReward\n";
				s += "}\n\n";
			}
			return s;
		}

		public string GetStoreRecipe()
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				s += "DetailRecipe Alt_" + sku.ToLower() + boost.letter.ToLower() + "\n{\n";
				s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName) + "\"\n";
				s += "\tDisplayHelp \"P1426840851\"\n";
				s += "\tDisplayTabName \"P2968462820\"\n";
				s += "\tType Workshop\n\tWorkshop Worktable_Alt\n";
				s += "\tEnhancementReward Boosts.Attuned_" + boost.name + ".Attuned_" + boost.name + "\n";
				s += "\tIcon " + iconName + "\n";
				s += "\tLevel 1\n";
				s += "\tRarity " + rarity + "\n";
				s += "\tMaxInvAmount 1\n\tNumUses 1\n\tSellToVendor 100\n\tCreatesEnhancement 1\n\tFlags NoGenericBadgeCredit, Voucher\n}\n\n";
			}
			return s;
		}

		public string GetBoostSetDef()
		{
			string s = "BoostSet\n{\n";
			s += "\tName " + name + "\n";
			s += "\tDisplayName " + PString(displayName) + "\n";
			s += "\tGroupName " + setGroup.groupName + "\n";
			s += "\tConversionGroups " + conversionGroups + "\n";
			s += "\tMinLevel " + minLevel + "\n";
			s += "\tMaxLevel " + maxLevel + "\n";
			s += "\tinclude defs/boostsets/" + setGroup.include + ".powerinc\n";
			if (boostList != null)
			{
				foreach (Boost boost in boostList)
				{
					s += "\tBoostLists\n\t{\n";
					s += "\t\tBoosts Boosts.Crafted_" + boost.name + ".Crafted_" + boost.name + "\n";
					s += "\t\tBoosts Boosts.Attuned_" + boost.name + ".Attuned_" + boost.name + "\n";
					s += "\t}\n";
				}
			}
			if (bonusList != null)
			{
				foreach (Bonus bonus in bonusList)
				{
					s += "\tBonuses\n\t{\n";
					s += "\t\tMinBoosts " + bonus.minBoosts + "\n";
					s += "\t\tMaxBoosts " + bonus.maxBoosts + "\n";
					s += "\t\tAutoPowers " + bonus.autoPowers + "\n";
					if (!string.IsNullOrEmpty(bonus.requires)) s += "\t\tRequires " + bonus.requires + "\n";
					s += "\t}\n";
				}
			}
			s += "}\n";
			return s;
		}

		public string GetBoostsCategories()
		{
			string s = "PowerCategory Boosts\n{\n";
			s += "\tDisplayName \"P1604142960\"\n";

			if (boostList != null)
			{
				foreach (Boost boost in boostList) s += "\tPowerSets Boosts.Crafted_" + boost.name + "\n";
				foreach (Boost boost in boostList) s += "\tPowerSets Boosts.Attuned_" + boost.name + "\n";
			}
			s += "}\n";
			return s;
		}

		private string BoostPowerSet(Boost boost, string prefix)
		{
			string s = "PowerSet Boosts." + prefix + boost.name + "\n{\n";
			s += "\tName \"" + prefix + boost.name + "\"\n";
			s += "\tDisplayName \"" + PString(boost.name.Replace("_", " ")) + "\"\n";
			s += "\tDisplayShortHelp \"" + PString("Set: " + displayName) + "\"\n";
			s += "\tDisplayHelp \"" + PString(boost.description) + "\"\n";
			s += "\tIconName \"" + iconName + "\"\n\n";
			s += "\tPowers Boosts." + prefix + boost.name + "." + prefix + boost.name + "\n";
			s += "\tAvailable 0\n";
			s += "\tAIMaxLevel 0\n";
			s += "\tAIMinRankCon -9999\n";
			s += "\tAIMaxRankCon 9999\n";
			s += "\tMinDifficulty 2\n";
			s += "\tMaxDifficulty 9999\n";
			s += "}\n";
			return s;
		}

		public string GetBoostsPowerSets()
		{
			string s = String.Empty;
			if (boostList != null)
			{
				foreach (Boost boost in boostList) s += BoostPowerSet(boost, "Crafted_");
				foreach (Boost boost in boostList) s += BoostPowerSet(boost, "Attuned_");
			}
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
			string s = "Power Boosts." + prefix + boost.name + "." + prefix + boost.name + "\n{\n";
			s += "\tName \"" + prefix + boost.name + "\"\n";
			s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName) + "\"\n";
			s += "\tType kBoost\n";
			s += "\tAccuracy 1\n";
			s += "\tEntsAffected kCaster\n";
			s += "\tEntsAutoHit kCaster\n";
			s += "\tTarget kCaster\n";
			s += "\tRange 0\n";
			s += "\tEnduranceCost 0\n";
			s += "\tIdeaCost 0\n";
			s += "\tInterruptTime 0\n";
			s += "\tTimeToActivate 0\n";
			s += "\tRechargeTime 0\n";
			s += "\tActivatePeriod 10\n";
			s += "\tEffectArea kCharacter\n";
			s += "\tActivatePeriod 10\n";
			s += "\tRadius 0\n";
			s += "\tArc 0\n";
			s += "\tBoostsAllowed kScience_Boost, kTechnology_Boost, kMagic_Boost, kMutation_Boost, kNatural_Boost" + (String.IsNullOrEmpty(boostsAllowed) ? String.Empty : ", " + boostsAllowed) + "\n";
			if (!String.IsNullOrEmpty(catalyzed)) s += "\tBoostCatalystConversion Boosts." + catalyzed + boost.name + "." + catalyzed + boost.name + "\n";
			s += (attuned ? "\tBoostUsePlayerLevel True\n" : "\tBoostBoostable True\n");
			s += "\tBoostIgnoreEffectiveness True\n";
			s += "\tBoostCombinable False\n";
			s += "\tMinSlotLevel " + (minSlotLevel - 1) + "\n";
			s += "\tBoostAlwaysCountForSet True\n";
			if (attuned) s += "\tBoostLicenseLevel    0\n";
			s += "\tDisplayShortHelp \"" + PString("Set: " + displayName) + "\"\n";
			s += "\tDisplayHelp \"" + PString(boost.description) + "\"\n";
			s += "\tIconName \"" + iconName + "\"\n";
			s += "\tTimeToConfirm 0\n\n";

			foreach (BoostType aspect in boost.aspects)
			{
				foreach (AttribMod attrib in aspect.attribMods[attribCount])
				{
					s += "\tAttribMod\n\t{\n";
					s += "\t\tName \"" + attrib.name + "\"\n";
					s += "\t\tTable \"" + attrib.table + "\"\n";
					s += "\t\tAspect " + (String.IsNullOrEmpty(attrib.aspect) ? "kStr" : attrib.aspect) + "\n";
					s += "\t\tAttrib " + attrib.attrib + "\n";
					s += "\t\tTarget kSelf\n";
					s += "\t\tScale " + attrib.scale + "\n";
					s += "\t\tType kMagnitude\n";
					s += "\t\tAllowStrength kFalse\n";
					s += "\t\tAllowResistance kFalse\n";
					s += "\t\tDelay 0\n";
					s += "\t\tDuration 10.25\n";
					s += "\t\tMagnitude 1\n";
					s += "\t\tPeriod 0\n";
					s += "\t\tChance 1\n";
					s += "\t\tNearGround kFalse\n";
					s += "\t\tCancelOnMiss kFalse\n";
					s += "\t\tBoostTemplate kTrue\n";
					s += "\t\tStackType kReplace\n";
					if (!String.IsNullOrEmpty(attrib.reward)) s += "\t\tReward " + attrib.reward + "\n";
					s += "\t}\n";
				}
			}

			s += "}\n";
			return s;
		}
	}
}
