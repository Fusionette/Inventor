using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace Inventor
{
	public partial class Form1 : Form
	{
		Config config = new Config();
		BoostSetData boostSet = new BoostSetData();
		List<SetGroup> setGroups;
		List<BoostType> boostTypes;
		List<int> craftingCost;

		public Form1()
		{
			InitializeComponent();
			config = JsonConvert.DeserializeObject<Config>(LoadConfig("Config", Properties.Resources.Config));
			setGroups = JsonConvert.DeserializeObject<List<SetGroup>>(LoadConfig("SetGroups", Properties.Resources.SetGroups));
			boostTypes = JsonConvert.DeserializeObject<List<BoostType>>(LoadConfig("BoostTypes", Properties.Resources.BoostTypes));
			craftingCost = JsonConvert.DeserializeObject<List<int>>(LoadConfig("CraftingCost", Properties.Resources.CraftingCost));
			foreach (SetGroup group in setGroups) setGroupName.Items.Add(group);
			foreach (BoostType boostType in boostTypes) boostTypeList.Items.Add(boostType);
			this.Icon = Icon.FromHandle(boostSet.GetIcon(config, "Superior").GetHicon());
		}

		private string LoadConfig(string filename, string resource)
		{
			if (File.Exists("config/" + filename + ".json")) return File.ReadAllText("config/" + filename + ".json");
			Directory.CreateDirectory("config");
			File.WriteAllText("config/" + filename + ".json", resource);
			return resource;
		}

		private void LoadBoostset(string filename)
		{
			if (File.Exists(filename))
			{
				boostSet = JsonConvert.DeserializeObject<BoostSetData>(File.ReadAllText(filename));
				UpdateFormData();
				this.Icon = Icon.FromHandle(boostSet.GetIcon(config, "Crafted").GetHicon());
			}
		}

		private void SaveBoostset(string filename)
		{
			UpdateBoostSet();
			File.WriteAllText(filename, JsonConvert.SerializeObject(boostSet));
		}

		private void UpdateFormData()
		{
			// Don't fire events while loading.
			setDisplayName.TextChanged -= setDisplayName_TextChanged;
			setGroupName.SelectedIndexChanged -= setGroupName_SelectedIndexChanged;
			setIconName.TextChanged -= setIconName_TextChanged;
			setMinLevel.ValueChanged -= setMinLevel_ValueChanged;

			setDisplayName.Text = boostSet.displayName;
			if (boostSet.setGroup != null) setGroupName.Text = boostSet.setGroup.displayName;
			setIconName.Text = boostSet.iconName;
			if (boostSet.minLevel > 0) setMinLevel.Value = boostSet.minLevel;

			setName.Text = boostSet.name;
			conversionGroups.Text = boostSet.conversionGroups;
			setBoostsAllowed.Text = boostSet.boostsAllowed;
			if (boostSet.maxLevel > 0) setMaxLevel.Value = boostSet.maxLevel;
			if (boostSet.minSlotLevel > 0) slotMinLevel.Value = boostSet.minSlotLevel;
			boostList.Items.Clear();
			foreach (Boost boost in boostSet.boostList) boostList.Items.Add(boost);
			bonusList.Items.Clear();
			foreach (Bonus bonus in boostSet.bonusList) bonusList.Items.Add(bonus);
			recipeTabName.Text = boostSet.meritTabName;
			recipeSku.Text = boostSet.sku;
			recipeRarity.Text = boostSet.rarity;
			this.Icon = Icon.FromHandle(boostSet.GetIcon(config, "Crafted").GetHicon());

			// Restore events.
			setDisplayName.TextChanged += setDisplayName_TextChanged;
			setGroupName.SelectedIndexChanged += setGroupName_SelectedIndexChanged;
			setIconName.TextChanged += setIconName_TextChanged;
			setMinLevel.ValueChanged += setMinLevel_ValueChanged;
		}

		private void UpdateBoostSet()
		{
			if (boostSet.boostList == null) boostSet.boostList = new List<Boost>();
			if (boostSet.bonusList == null) boostSet.bonusList = new List<Bonus>();
			boostSet.displayName = setDisplayName.Text.Trim();
			boostSet.iconName = setIconName.Text.Trim();
			boostSet.name = setName.Text.Trim();
			boostSet.setGroup = (SetGroup)setGroupName.SelectedItem;
			boostSet.conversionGroups = conversionGroups.Text.Trim();
			boostSet.boostsAllowed = setBoostsAllowed.Text.Trim();
			boostSet.minLevel = (int)setMinLevel.Value;
			boostSet.maxLevel = (int)setMaxLevel.Value;
			boostSet.minSlotLevel = (int)slotMinLevel.Value;
			boostSet.boostList.Clear();
			foreach (Boost boost in boostList.Items) boostSet.boostList.Add(boost);
			boostSet.bonusList.Clear();
			foreach (Bonus bonus in bonusList.Items) boostSet.bonusList.Add(bonus);
			boostSet.meritTabName = recipeTabName.Text.Trim();
			boostSet.sku = recipeSku.Text.ToUpper();
			boostSet.rarity = recipeRarity.Text.Trim();
			this.Icon = Icon.FromHandle(boostSet.GetIcon(config, "Crafted").GetHicon());
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (config.autoSave) SaveBoostset("autosave.json");
		}

		private void createDataFiles_Click(object sender, EventArgs e)
		{
			UpdateBoostSet();
			Directory.CreateDirectory(config.data + "Defs/Account");
			Directory.CreateDirectory(config.data + "Defs/Boostsets");
			Directory.CreateDirectory(config.data + "Defs/Powers");
			Directory.CreateDirectory(config.data + "Defs/Invention");
			Directory.CreateDirectory(config.data + "Texts/English/Bases");
			Directory.CreateDirectory(config.data + "Texts/English/Boostset");
			Directory.CreateDirectory(config.data + "Texts/English/Powers");

			File.WriteAllText(config.data + "Defs/Account/Product_Catalog.def", boostSet.GetProductCatalog());
			File.WriteAllText(config.data + "Defs/Invention/Recipes.recipe", boostSet.GetDropRecipe(craftingCost));
			File.WriteAllText(config.data + "Defs/Invention/Merits.recipe", boostSet.GetMeritsRecipe());
			File.WriteAllText(config.data + "Defs/Invention/Store.recipe", boostSet.GetStoreRecipe());
			File.WriteAllText(config.data + "texts/English/Bases/Recipes.ms", boostSet.DumpCache());

			File.WriteAllText(config.data + "Defs/Boostsets/Boostsets.def", boostSet.GetBoostSetDef());
			File.WriteAllText(config.data + "Texts/English/Boostset/Boostsets.ms", boostSet.DumpCache());

			File.WriteAllText(config.data + "Defs/Powers/Boosts.categories", boostSet.GetBoostsCategories());
			File.WriteAllText(config.data + "Defs/Powers/Boosts.powersets", boostSet.GetBoostsPowerSets());
			foreach (Boost boost in boostSet.boostList) File.WriteAllText(config.data + "Defs/Powers/Boosts_Crafted_" + boost.name + ".powers", boostSet.GetPowers(boost, false, "Crafted_", "Attuned_"));
			foreach (Boost boost in boostSet.boostList) File.WriteAllText(config.data + "Defs/Powers/Boosts_Attuned_" + boost.name + ".powers", boostSet.GetPowers(boost, true, "Attuned_", null));
			File.WriteAllText(config.data + "Texts/English/Powers/Boosts." + boostSet.name + ".ms", boostSet.DumpCache());

			if (File.Exists(config.images + boostSet.iconName + ".png"))
			{
				Directory.CreateDirectory(config.thumbs + "enhancements");
				Directory.CreateDirectory(config.thumbs + "recipes");
				Directory.CreateDirectory(config.thumbs + "smallenhancements");
				Directory.CreateDirectory(config.thumbs + "smallrecipes");

				Bitmap icon = boostSet.GetIcon(config, "Recipe");
				icon.Save(config.thumbs + "recipes/" + boostSet.name + ".png", ImageFormat.Png);
				icon = new Bitmap(icon, new Size(47, 32));
				icon.Save(config.thumbs + "smallrecipes/" + boostSet.name + ".png", ImageFormat.Png);

				icon = boostSet.GetIcon(config, "Crafted");
				icon.Save(config.thumbs + "enhancements/" + boostSet.name + ".png", ImageFormat.Png);
				this.Icon = Icon.FromHandle(icon.GetHicon());
				icon = new Bitmap(icon, new Size(32, 32));
				icon.Save(config.thumbs + "smallenhancements/" + boostSet.name + ".png", ImageFormat.Png);

				icon = boostSet.GetIcon(config, "Attuned");
				icon.Save(config.thumbs + "enhancements/Attuned_" + boostSet.name + ".png", ImageFormat.Png);
				icon = new Bitmap(icon, new Size(32, 32));
				icon.Save(config.thumbs + "smallenhancements/Attuned_" + boostSet.name + ".png", ImageFormat.Png);

				if (config.makeSuperior)
				{
					icon = boostSet.GetIcon(config, "Superior");
					icon.Save(config.thumbs + "enhancements/Superior_" + boostSet.name + ".png", ImageFormat.Png);
					icon = new Bitmap(icon, new Size(32, 32));
					icon.Save(config.thumbs + "smallenhancements/Superior_" + boostSet.name + ".png", ImageFormat.Png);
				}

				string enhancementsList = String.Empty;
				string recipesList = String.Empty;

				foreach (Boost boost in boostSet.boostList)
				{
					enhancementsList += "Boosts.Crafted.Crafted_" + boost.name + "\t" + boostSet.name + ".png\n";
					enhancementsList += "Boosts.Attuned.Attuned_" + boost.name + "\tAttuned_" + boostSet.name + ".png\n";
					if (config.makeSuperior) enhancementsList += "Boosts.Superior.Superior_" + boost.name + "\tSuperior_" + boostSet.name + ".png\n";
					for (int i = boostSet.minLevel; i <= boostSet.maxLevel; i++)
					{
						recipesList += boost.name + "_" + i + "\t" + boostSet.name + ".png\n";
					}
				}

				File.WriteAllText(config.thumbs + "enhancements_images.txt", enhancementsList);
				File.WriteAllText(config.thumbs + "recipes_images.txt", recipesList);
			}
		}

		private void bonusAddNew_Click(object sender, EventArgs e)
		{
			Bonus bonus = new Bonus();
			if (!string.IsNullOrEmpty(bonusAutoPowers.Text))
			{
				bonus.minBoosts = (int)bonusMinBoosts.Value;
				bonus.maxBoosts = (int)bonusMaxBoosts.Value;
				bonus.autoPowers = bonusAutoPowers.Text.Trim();
				bonus.requires = bonusRequires.Text.Trim();
				bonusList.Items.Add(bonus);
				bonusAutoPowers.Text = String.Empty;
				bonusRequires.Text = String.Empty;
			}
		}

		private void boostLetter_TextChanged(object sender, EventArgs e)
		{
			Boost boost = new Boost();
			boost.letter = boostLetter.Text.ToUpper();

			if (!String.IsNullOrEmpty(boost.letter) && boost.letter[0] >= 'A' && (boost.letter[0] <= 'F'))
			{
				boost.name = setName.Text.Trim() + "_" + boostLetter.Text.ToUpper();
				foreach (Boost savedBoost in boostList.Items)
				{
					if (savedBoost.letter.Equals(boost.letter))
					{
						boost = savedBoost;
						break;
					}
				}
			}
			else
			{
				boost.letter = String.Empty;
			}

			boostLetter.TextChanged -= boostLetter_TextChanged;
			boostLetter.Text = boost.letter;
			boostName.Text = boost.name;
			boostDisplayName.Text = boost.displayName;
			boostAspectList.Items.Clear();
			if (boost.aspects != null) foreach (BoostType type in boost.aspects) boostAspectList.Items.Add(type);
			boostDescription.Text = boost.description;
			boostLetter.TextChanged += boostLetter_TextChanged;
		}

		private void UpdateAspectList()
		{
			string boostList = String.Empty;
			string boostDescriptionList = String.Empty;
			foreach (BoostType type in boostAspectList.Items)
			{
				boostList += type.displayName + "/";
				boostDescriptionList += type.description + " ";
			}
			if (boostList.EndsWith("/")) boostList = boostList.Substring(0, boostList.Length - 1);
			boostDisplayName.Text = boostList;
			boostDescription.Text = boostDescriptionList;
		}

		private void boostAddAspect_Click(object sender, EventArgs e)
		{
			if ((boostTypeList.SelectedItem != null) && (!boostAspectList.Items.Contains(boostTypeList.SelectedItem)) && (boostAspectList.Items.Count < 4))
			{
				boostAspectList.Items.Add(boostTypeList.SelectedItem);
				UpdateAspectList();
			}
		}

		private void boostAddNew_Click(object sender, EventArgs e)
		{
			Boost boost = new Boost();
			if (!string.IsNullOrEmpty(boostLetter.Text))
			{
				List<Boost> sortableList = new List<Boost>();

				boost.letter = boostLetter.Text.ToUpper();
				boost.name = boostName.Text.Trim();
				boost.displayName = boostDisplayName.Text.Trim();
				boost.description = boostDescription.Text.Trim();
				boost.aspects = new List<BoostType>();
				foreach (BoostType type in boostAspectList.Items) boost.aspects.Add(type);

				// Rebuild the Enhancements list to keep it sorted and prevent duplicate letters.
				foreach (Boost b in boostList.Items) if (!b.letter.Equals(boost.letter)) sortableList.Add(b);
				sortableList.Add(boost);
				sortableList = sortableList.OrderBy(b => b.letter).ToList();
				boostList.Items.Clear();
				foreach (Boost b in sortableList) boostList.Items.Add(b);
			}
		}

		private bool ListKeyHandler(ListBox list, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && list.SelectedIndex >= 0)
			{
				list.Items.RemoveAt(list.SelectedIndex);
				return true;
			}
			else if (e.KeyCode == Keys.Up && list.SelectedItem != null && list.SelectedIndex >= 1)
			{
				int i = list.SelectedIndex;
				object selected = list.SelectedItem;
				list.Items.Remove(selected);
				list.Items.Insert(i - 1, selected);
				list.SetSelected(i, true);
				return true;
			}
			else if (e.KeyCode == Keys.Down && list.SelectedItem != null && list.SelectedIndex < list.Items.Count - 1)
			{
				int i = list.SelectedIndex;
				object selected = list.SelectedItem;
				list.Items.Remove(selected);
				list.Items.Insert(i + 1, selected);
				list.SetSelected(i, true);
				return true;
			}
			return false;
		}

		private void boostList_KeyDown(object sender, KeyEventArgs e)
		{
			ListKeyHandler(boostList, e);
		}

		private void bonusList_KeyDown(object sender, KeyEventArgs e)
		{
			ListKeyHandler(bonusList, e);
		}

		private void boostAspectList_KeyDown(object sender, KeyEventArgs e)
		{
			if (ListKeyHandler(boostAspectList, e)) UpdateAspectList();
		}

		private void loadJson_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = Application.StartupPath;
			ofd.Filter = "JSON files (*.json)|*.json";
			if (ofd.ShowDialog() == DialogResult.OK) LoadBoostset(ofd.FileName);
		}

		private void saveJson_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.InitialDirectory = Application.StartupPath;
			sfd.Filter = "JSON files (*.json)|*.json";
			sfd.FileName = setName.Text;
			sfd.DefaultExt = ".json";
			if (sfd.ShowDialog() == DialogResult.OK) SaveBoostset(sfd.FileName);
		}

		private void setDisplayName_TextChanged(object sender, EventArgs e)
		{
			setName.Text = setDisplayName.Text.Trim().Replace(" ", "_").Replace("'", String.Empty);
			setIconName.Text = "E_ICON_" + setName.Text;
			recipeTabName.Text = "IO Set|Recipe|" + setGroupName.Text + "|" + setDisplayName.Text.Trim();
		}

		private void setGroupName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (setGroupName.SelectedItem != null)
			{
				SetGroup group = (SetGroup)setGroupName.SelectedItem;
				conversionGroups.Text = group.conversionGroup;
				setBoostsAllowed.Text = group.boostsAllowed;
			}
			recipeTabName.Text = "IO Set|Recipe|" + setGroupName.Text + "|" + setDisplayName.Text.Trim();
			UpdateBoostSet();
		}

		private void setMinLevel_ValueChanged(object sender, EventArgs e)
		{
			slotMinLevel.Value = setMinLevel.Value > 3 ? setMinLevel.Value - 3 : 1;
		}

		private void setIconName_TextChanged(object sender, EventArgs e)
		{
			if (File.Exists(config.images + setIconName.Text.Trim() + ".png")) UpdateBoostSet();
		}
	}
}
