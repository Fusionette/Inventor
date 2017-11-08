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
				s += "\"" + entry.Key + "\" \"" + entry.Value + "\"" + Environment.NewLine;
			}
			pstringCache.Clear();
			return s;
		}

		public string GetProductCatalog()
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				s += "CatalogItem" + Environment.NewLine;
				s += "\tTitle <<" + displayName + ": " + boost.displayName + ">>" + Environment.NewLine;
				s += "\tSKU " + sku + boost.letter + Environment.NewLine;
				s += "\tInventoryType Voucher" + Environment.NewLine;
				s += "\tInventoryCount 1" + Environment.NewLine;
				s += "\tGlobal 1" + Environment.NewLine;
				s += "\tItemKey Alt_" + sku.ToLower() + boost.letter.ToLower() + Environment.NewLine;
				s += "End" + Environment.NewLine + Environment.NewLine;
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
					s += "DetailRecipe " + boost.name + "_" + level + Environment.NewLine;
					s += "{" + Environment.NewLine;
					s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName) + "\"" + Environment.NewLine;
					s += "\tDisplayHelp \"" + PString("This recipe builds the " + displayName + ": " + boost.displayName + " Enhancement") + "\"" + Environment.NewLine;
					s += "\tDisplayTabName \"P1273912828\"" + Environment.NewLine;
					s += "\tType Drop" + Environment.NewLine;
					s += "\tWorkshop Worktable_Invention" + Environment.NewLine;
					s += "\tEnhancementReward Boosts.Crafted_" + boost.name + ".Crafted_" + boost.name + Environment.NewLine;
					s += "\tIcon " + iconName + Environment.NewLine;
					s += "\tLevel " + level + Environment.NewLine;
					s += "\tRarity " + rarity + Environment.NewLine;
					s += "\tMaxInvAmount 100" + Environment.NewLine;
					s += "\tNumUses 1" + Environment.NewLine;
					s += "\tCreationCost " + craftingCost[level - 1] + Environment.NewLine;
					s += "\tSellToVendor " + (level * 200) + Environment.NewLine;
					s += "\tCreatesEnhancement 1" + Environment.NewLine + Environment.NewLine;
					s += "}" + Environment.NewLine + Environment.NewLine;
				}
			}
			return s;
		}

		public string GetMeritsRecipe()
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				s += "DetailRecipe Merit_" + boost.name + Environment.NewLine;
				s += "{" + Environment.NewLine; ;
				s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName + " (Recipe)") + "\"" + Environment.NewLine;
				s += "\tDisplayHelp \"P1426840851\"" + Environment.NewLine;
				s += "\tDisplayTabName \"" + PString(meritTabName) + "\"" + Environment.NewLine;
				s += "\tType Workshop" + Environment.NewLine;
				s += "\tWorkshop Worktable_Merit" + Environment.NewLine;
				s += "\tRecipeReward " + boost.name + Environment.NewLine;
				s += "\tIcon " + iconName + Environment.NewLine;
				s += "\tLevelMin " + minLevel + Environment.NewLine;
				s += "\tLevelMax " + maxLevel + Environment.NewLine;
				s += "\tRarity " + rarity + Environment.NewLine;
				s += "\tMaxInvAmount 1" + Environment.NewLine;
				s += "\tNumUses 1" + Environment.NewLine;
				s += "\tSellToVendor 100" + Environment.NewLine;
				s += "\tCreatesRecipe 1" + Environment.NewLine;
				s += "\tFlags NoGenericBadgeCredit" + Environment.NewLine + Environment.NewLine;
				if (rarity == "Uncommon") s += "\tSalvageComponent 20 S_MeritReward" + Environment.NewLine;
				else if (rarity == "Rare") s += "\tSalvageComponent 50 S_MeritReward" + Environment.NewLine;
				else if (rarity == "VeryRare") s += "\tSalvageComponent 100 S_MeritReward" + Environment.NewLine;
				s += "}" + Environment.NewLine + Environment.NewLine;
			}
			return s;
		}

		public string GetStoreRecipe()
		{
			string s = String.Empty;
			foreach (Boost boost in boostList)
			{
				s += "DetailRecipe Alt_" + sku.ToLower() + boost.letter.ToLower() + Environment.NewLine;
				s += "{" + Environment.NewLine;
				s += "\tDisplayName \"" + PString(displayName + ": " + boost.displayName) + "\"" + Environment.NewLine;
				s += "\tDisplayHelp \"P1426840851\"" + Environment.NewLine;
				s += "\tDisplayTabName \"P2968462820\"" + Environment.NewLine;
				s += "\tType Workshop" + Environment.NewLine;
				s += "\tWorkshop Worktable_Alt" + Environment.NewLine;
				s += "\tEnhancementReward Boosts.Attuned_" + boost.name + ".Attuned_" + boost.name + Environment.NewLine;
				s += "\tIcon " + iconName + Environment.NewLine;
				s += "\tLevel 1" + Environment.NewLine;
				s += "\tRarity " + rarity + Environment.NewLine;
				s += "\tMaxInvAmount 1" + Environment.NewLine;
				s += "\tNumUses 1" + Environment.NewLine;
				s += "\tSellToVendor 100" + Environment.NewLine;
				s += "\tCreatesEnhancement 1" + Environment.NewLine;
				s += "\tFlags NoGenericBadgeCredit, Voucher" + Environment.NewLine;
				s += "}" + Environment.NewLine + Environment.NewLine;
			}
			return s;
		}

		public string GetBoostSetDef()
		{
			string s = "BoostSet" + Environment.NewLine;
			s += "{" + Environment.NewLine;
			s += "\tName " + name + Environment.NewLine;
			s += "\tDisplayName " + PString(displayName) + Environment.NewLine;
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
			s += "}" + Environment.NewLine + Environment.NewLine;
			return s;
		}

		public string GetBoostsCategories()
		{
			string s = "PowerCategory Boosts" + Environment.NewLine;
			s += "{" + Environment.NewLine;
			s += "\tDisplayName \"P1604142960\"" + Environment.NewLine;

			if (boostList != null)
			{
				foreach (Boost boost in boostList) s += "\tPowerSets Boosts.Crafted_" + boost.name + Environment.NewLine;
				foreach (Boost boost in boostList) s += "\tPowerSets Boosts.Attuned_" + boost.name + Environment.NewLine;
			}
			s += "}" + Environment.NewLine;
			return s;
		}

		private string BoostPowerSet(Boost boost, string prefix)
		{
			string s = "PowerSet Boosts." + prefix + boost.name + Environment.NewLine;
			s += "{" + Environment.NewLine;
			s += "\tName             \"" + prefix + boost.name + "\"" + Environment.NewLine;
			s += "\tDisplayName      \"" + PString(boost.name.Replace("_", " ")) + "\"" + Environment.NewLine;
			s += "\tDisplayShortHelp \"" + PString("Set: " + displayName) + "\"" + Environment.NewLine;
			s += "\tDisplayHelp      \"" + PString(boost.description) + "\"" + Environment.NewLine;
			s += "\tIconName         \"" + iconName + "\"" + Environment.NewLine + Environment.NewLine;
			s += "\tPowers Boosts." + prefix + boost.name + "." + prefix + boost.name + Environment.NewLine;
			s += "\tAvailable 0" + Environment.NewLine;
			s += "\tAIMaxLevel 0" + Environment.NewLine;
			s += "\tAIMinRankCon -9999" + Environment.NewLine;
			s += "\tAIMaxRankCon 9999" + Environment.NewLine;
			s += "\tMinDifficulty 2" + Environment.NewLine;
			s += "\tMaxDifficulty 9999" + Environment.NewLine;
			s += "}" + Environment.NewLine + Environment.NewLine;
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
			string s = "Power Boosts." + prefix + boost.name + "." + prefix + boost.name + Environment.NewLine;
			s += "{" + Environment.NewLine;
			s += "\tName                 \"" + prefix + boost.name + "\"" + Environment.NewLine;
			s += "\tDisplayName          \"" + PString(displayName + ": " + boost.displayName) + "\"" + Environment.NewLine;
			s += "\tType                 kBoost" + Environment.NewLine;
			s += "\tAccuracy             1" + Environment.NewLine;
			s += "\tEntsAffected         kCaster" + Environment.NewLine;
			s += "\tEntsAutoHit          kCaster" + Environment.NewLine;
			s += "\tTarget               kCaster" + Environment.NewLine;
			s += "\tRange                0" + Environment.NewLine;
			s += "\tEnduranceCost        0" + Environment.NewLine;
			s += "\tIdeaCost             0" + Environment.NewLine;
			s += "\tInterruptTime        0" + Environment.NewLine;
			s += "\tTimeToActivate       0" + Environment.NewLine;
			s += "\tRechargeTime         0" + Environment.NewLine;
			s += "\tActivatePeriod       10" + Environment.NewLine;
			s += "\tEffectArea           kCharacter" + Environment.NewLine;
			s += "\tActivatePeriod       10" + Environment.NewLine;
			s += "\tRadius               0" + Environment.NewLine;
			s += "\tArc                  0" + Environment.NewLine;
			s += "\tBoostsAllowed        kScience_Boost, kTechnology_Boost, kMagic_Boost, kMutation_Boost, kNatural_Boost" + (String.IsNullOrEmpty(boostsAllowed) ? String.Empty : ", " + boostsAllowed) + Environment.NewLine;
			if (!String.IsNullOrEmpty(catalyzed)) s += "\tBoostCatalystConversion Boosts." + catalyzed + boost.name + "." + catalyzed + boost.name + Environment.NewLine;
			s += "\t" + (attuned ? "BoostUsePlayerLevel  " : "BoostBoostable       ") + "True" + Environment.NewLine;
			s += "\tBoostIgnoreEffectiveness True" + Environment.NewLine;
			s += "\tBoostCombinable      False" + Environment.NewLine;
			s += "\tMinSlotLevel         " + (minSlotLevel - 1) + Environment.NewLine;
			s += "\tBoostAlwaysCountForSet True" + Environment.NewLine;
			if (attuned) s += "\tBoostLicenseLevel    0" + Environment.NewLine;
			s += "\tDisplayShortHelp     \"" + PString("Set: " + displayName) + "\"" + Environment.NewLine;
			s += "\tDisplayHelp          \"" + PString(boost.description) + "\"" + Environment.NewLine;
			s += "\tIconName             \"" + iconName + "\"" + Environment.NewLine;
			s += "\tTimeToConfirm        0" + Environment.NewLine + Environment.NewLine;

			foreach (BoostType aspect in boost.aspects)
			{
				foreach (AttribMod attrib in aspect.attribMods[attribCount])
				{
					s += "\tAttribMod" + Environment.NewLine;
					s += "\t{" + Environment.NewLine;
					s += "\t\tName                 \"" + attrib.name + "\"" + Environment.NewLine;
					s += "\t\tTable                \"" + attrib.table + "\"" + Environment.NewLine;
					s += "\t\tAspect               " + (String.IsNullOrEmpty(attrib.aspect) ? "kStr" : attrib.aspect) + Environment.NewLine;
					s += "\t\tAttrib               " + attrib.attrib + Environment.NewLine;
					s += "\t\tTarget               kSelf" + Environment.NewLine;
					s += "\t\tScale                " + attrib.scale + Environment.NewLine;
					s += "\t\tType                 kMagnitude" + Environment.NewLine;
					s += "\t\tAllowStrength        kFalse" + Environment.NewLine;
					s += "\t\tAllowResistance      kFalse" + Environment.NewLine;
					s += "\t\tDelay                0" + Environment.NewLine;
					s += "\t\tDuration             10.25" + Environment.NewLine;
					s += "\t\tMagnitude            1" + Environment.NewLine;
					s += "\t\tPeriod               0" + Environment.NewLine;
					s += "\t\tChance               1" + Environment.NewLine;
					s += "\t\tNearGround           kFalse" + Environment.NewLine;
					s += "\t\tCancelOnMiss         kFalse" + Environment.NewLine;
					s += "\t\tBoostTemplate        kTrue" + Environment.NewLine;
					s += "\t\tStackType            kReplace" + Environment.NewLine;
					if (!String.IsNullOrEmpty(attrib.reward)) s += "\t\tReward               " + attrib.reward + Environment.NewLine;
					s += "\t}" + Environment.NewLine;
				}
			}

			s += "}" + Environment.NewLine;
			return s;
		}
	}
}
