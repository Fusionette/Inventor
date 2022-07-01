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
		List<Salvage> salvage;
        List<SetBonus> setBonusData;
		List<int> craftingCost;
        List<string> boostCategories = new List<string>();

		public Form1()
		{
			InitializeComponent();
			config = JsonConvert.DeserializeObject<Config>(LoadConfig("Config", Properties.Resources.Config));
			setGroups = JsonConvert.DeserializeObject<List<SetGroup>>(LoadConfig("SetGroups", Properties.Resources.SetGroups));
			boostTypes = JsonConvert.DeserializeObject<List<BoostType>>(LoadConfig("BoostTypes", Properties.Resources.BoostTypes));
			salvage = JsonConvert.DeserializeObject<List<Salvage>>(LoadConfig("Salvage", Properties.Resources.Salvage));
            setBonusData = JsonConvert.DeserializeObject<List<SetBonus>>(LoadConfig("SetBonus", Properties.Resources.SetBonus));
            craftingCost = JsonConvert.DeserializeObject<List<int>>(LoadConfig("CraftingCost", Properties.Resources.CraftingCost));
            foreach (SetGroup group in setGroups) setGroupName.Items.Add(group);
            foreach (BoostType boostType in boostTypes) boostTypeList.Items.Add(boostType);
            setBonusList.DisplayMember = "displayList";
            setBonusList.DataSource = setBonusData;
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
			File.WriteAllText(filename, JsonConvert.SerializeObject(boostSet, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
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
            createDataFiles.Enabled = false;
            UpdateBoostSet();
			Directory.CreateDirectory(config.data + "Defs/Account");
			Directory.CreateDirectory(config.data + "Defs/Boostsets");
			Directory.CreateDirectory(config.data + "Defs/Powers");
			Directory.CreateDirectory(config.data + "Defs/Invention");
			Directory.CreateDirectory(config.data + "Texts/English/Bases");
			Directory.CreateDirectory(config.data + "Texts/English/Boostset");
			Directory.CreateDirectory(config.data + "Texts/English/Powers");

            boostSet.catalog.AddFile(config.data + "Defs/Account/Product_Catalog.def");
            File.WriteAllText(config.data + "Defs/Account/Product_Catalog.def", boostSet.GetProductCatalog());

            boostSet.pstring.AddFile(config.data + "texts/English/Bases/Recipes.ms");

            boostSet.recipes.AddFile(config.data + "Defs/Invention/Recipes.recipe");
            File.WriteAllText(config.data + "Defs/Invention/Recipes.recipe", boostSet.GetDropRecipe(craftingCost, salvage));
            boostSet.recipes.AddFile(config.data + "Defs/Invention/Merits.recipe");
            File.WriteAllText(config.data + "Defs/Invention/Merits.recipe", boostSet.GetMeritsRecipe());
            boostSet.recipes.AddFile(config.data + "Defs/Invention/Store.recipe");
            File.WriteAllText(config.data + "Defs/Invention/Store.recipe", boostSet.GetStoreRecipe());

            File.WriteAllText(config.data + "texts/English/Bases/Recipes.ms", boostSet.pstring.ToString());

            boostSet.pstring.AddFile(config.data + "Texts/English/Boostset/Boostsets.ms");
            boostSet.boostsets.AddFile(config.data + "Defs/Boostsets/Boostsets.def");
			File.WriteAllText(config.data + "Defs/Boostsets/Boostsets.def", boostSet.GetBoostSetDef());
			File.WriteAllText(config.data + "Texts/English/Boostset/Boostsets.ms", boostSet.pstring.ToString());

            boostCategories.Clear();
            if (File.Exists(config.data + "Defs/Powers/Boosts.categories"))
            {
                string line;
                StreamReader file = new StreamReader(config.data + "Defs/Powers/Boosts.categories");
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.ToUpper().IndexOf("POWERSETS ") == 0)
                    {
                        boostCategories.Add(line.Substring(10).Trim());
                    }
                }

                file.Close();
            }
            File.WriteAllText(config.data + "Defs/Powers/Boosts.categories", boostSet.GetBoostsCategories(boostCategories));

            foreach (Boost boost in boostSet.boostList)
            {
                Directory.CreateDirectory(config.data + "Defs/Powers/Boosts/Crafted_" + boost.name);
                File.WriteAllText(config.data + "Defs/Powers/Boosts/Crafted_" + boost.name + ".powersets", boostSet.GetBoostsPowerSet(boost, "Crafted_"));
                File.WriteAllText(config.data + "Defs/Powers/Boosts/Crafted_" + boost.name + "/Crafted_" + boost.name + ".powers", boostSet.GetPowers(boost, false, "Crafted_", "Attuned_"));
                Directory.CreateDirectory(config.data + "Defs/Powers/Boosts/Attuned_" + boost.name);
                File.WriteAllText(config.data + "Defs/Powers/Boosts/Attuned_" + boost.name + ".powersets", boostSet.GetBoostsPowerSet(boost, "Attuned_"));
                File.WriteAllText(config.data + "Defs/Powers/Boosts/Attuned_" + boost.name + "/Attuned_" + boost.name + ".powers", boostSet.GetPowers(boost, true, "Attuned_", null));
            }

            File.WriteAllText(config.data + "Texts/English/Powers/Boosts." + boostSet.name + ".ms", boostSet.pstring.ToString());

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
					enhancementsList += "Boosts.Crafted.Crafted_" + boost.name + "\t" + boostSet.name + ".png" + Environment.NewLine;
					enhancementsList += "Boosts.Attuned.Attuned_" + boost.name + "\tAttuned_" + boostSet.name + ".png" + Environment.NewLine;
					if (config.makeSuperior) enhancementsList += "Boosts.Superior.Superior_" + boost.name + "\tSuperior_" + boostSet.name + ".png" + Environment.NewLine;
					for (int i = boostSet.minLevel; i <= boostSet.maxLevel; i++)
					{
						recipesList += boost.name + "_" + i + "\t" + boostSet.name + ".png" + Environment.NewLine;
					}
				}

				File.WriteAllText(config.thumbs + "enhancements_images.txt", enhancementsList);
				File.WriteAllText(config.thumbs + "recipes_images.txt", recipesList);
			}
            createDataFiles.Enabled = true;
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
			boostShortHelp.Text = boost.shortHelp;
			boostAspectList.Items.Clear();
			if (boost.aspects != null) foreach (BoostType type in boost.aspects) boostAspectList.Items.Add(type);
            boostDescription.Text = boost.description;
            boostSlotRequires.Text = boost.slotRequires;
            boostLetter.TextChanged += boostLetter_TextChanged;
			UpdateSalvageTree(boost);
		}

		private void UpdateAspectList()
		{
			string boostList = String.Empty;
			string descriptionList = String.Empty;
			string shortHelpList = string.Empty;
			foreach (BoostType type in boostAspectList.Items)
			{
				boostList += type.displayName + "/";
				shortHelpList += type.shortHelp + ", ";
				descriptionList += type.description + " ";
			}
			if (boostList.EndsWith("/")) boostList = boostList.Substring(0, boostList.Length - 1);
			if (shortHelpList.EndsWith(", ")) shortHelpList = shortHelpList.Substring(0, shortHelpList.Length - 2);
			boostDisplayName.Text = boostList;
			boostShortHelp.Text = shortHelpList;
			boostDescription.Text = descriptionList;
		}

		private void boostAddAspect_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(boostLetter.Text) && (boostTypeList.SelectedItem != null) && (!boostAspectList.Items.Contains(boostTypeList.SelectedItem)) && (boostAspectList.Items.Count < 4))
			{
				boostAspectList.Items.Add(boostTypeList.SelectedItem);
				UpdateAspectList();
			}
		}

		private void boostSave_Click(object sender, EventArgs e)
		{
			Boost boost = new Boost();
			if (!string.IsNullOrEmpty(boostLetter.Text))
			{
				List<Boost> sortableList = new List<Boost>();

				boost.letter = boostLetter.Text.ToUpper();
				boost.name = boostName.Text.Trim();
				boost.displayName = boostDisplayName.Text.Trim();
				boost.description = boostDescription.Text.Trim();
                boost.shortHelp = boostShortHelp.Text.Trim();
                boost.slotRequires = boostSlotRequires.Text.Trim();
                boost.aspects = new List<BoostType>();
				foreach (BoostType type in boostAspectList.Items) boost.aspects.Add(type);

				boost.salvage = new List<string>();
				foreach (TreeNode level in salvageTree.Nodes)
				{
					foreach (TreeNode salvage in level.Nodes) boost.salvage.Add(salvage.Name);
				}

				// Rebuild the Enhancements list to keep it sorted and prevent duplicate letters.
				foreach (Boost b in boostList.Items) if (!b.letter.Equals(boost.letter)) sortableList.Add(b);
				sortableList.Add(boost);
				sortableList = sortableList.OrderBy(b => b.letter).ToList();
				boostList.Items.Clear();
				foreach (Boost b in sortableList) boostList.Items.Add(b);
				tabs.SelectTab(tabSet);
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
			recipeTabName.Text = "Recipe|IO Set|" + setGroupName.Text + "|" + setDisplayName.Text.Trim();
		}

		private void setGroupName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (setGroupName.SelectedItem != null)
			{
				SetGroup group = (SetGroup)setGroupName.SelectedItem;
				conversionGroups.Text = group.conversionGroup;
				setBoostsAllowed.Text = group.boostsAllowed;
			}
			recipeTabName.Text = "Recipe|IO Set|" + setGroupName.Text + "|" + setDisplayName.Text.Trim();
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

		private void boostList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (boostList.SelectedItem != null)
			{
				Boost boost = (Boost)boostList.SelectedItem;
				boostLetter.Text = boost.letter;
				tabs.SelectTab(tabBoosts);
			}
		}

		private void tabs_Selected(object sender, TabControlEventArgs e)
		{
			// Enhancement and Recipes tab share some controls.
			if (tabs.SelectedIndex >= 1 && tabs.SelectedIndex <= 2)
			{
				boostName.Parent = tabs.TabPages[tabs.SelectedIndex];
				boostNameLabel.Parent = tabs.TabPages[tabs.SelectedIndex];
				boostLetter.Parent = tabs.TabPages[tabs.SelectedIndex];
				boostLetterLabel.Parent = tabs.TabPages[tabs.SelectedIndex];
				boostSave.Parent = tabs.TabPages[tabs.SelectedIndex];
			}
		}

		private void UpdateSalvageList(TreeNode node)
		{
			if (node.Parent == null)
			{
				salvageList.Items.Clear();
				foreach (Salvage s in salvage)
				{
					if (node.Index == (int) s.level && !node.Nodes.ContainsKey(s.name)) salvageList.Items.Add(s);
				}
			}
			else
			{
				UpdateSalvageList(node.Parent);
			}
		}

		private void AddSalvageToTree(Salvage s, TreeNode node)
		{
			TreeNode n = new TreeNode(s.displayName);
			n.Name = s.name;
			if (s.rarity > Salvage.Rarity.Common) n.BackColor = (s.rarity == Salvage.Rarity.Uncommon) ? Color.Yellow : Color.Orange;
			node.Nodes.Add(n);
			UpdateSalvageList(node);
			node.Expand();
		}

		private void UpdateSalvageTree(Boost boost)
		{
			List<TreeNode> lvlNodes = new List<TreeNode>();
			lvlNodes.Add(new TreeNode("Low Level (10-25)"));
			lvlNodes.Add(new TreeNode("Mid Level (26-40)"));
			lvlNodes.Add(new TreeNode("High Level (41-50)"));

			if (boost.salvage != null)
			{
				foreach (string sid in boost.salvage)
				{
					Salvage s = salvage.Find(x => x.name.Equals(sid));
					AddSalvageToTree(s, lvlNodes[(int)s.level]);
				}
			}

			salvageTree.Nodes.Clear();
			foreach (TreeNode node in lvlNodes) salvageTree.Nodes.Add(node);
			salvageList.Items.Clear();
		}

		private void salvageTree_KeyDown(object sender, KeyEventArgs e)
		{
			TreeView tree = (TreeView)sender;
			if (e.KeyCode == Keys.Delete && tree.SelectedNode.Parent != null)
			{
				tree.SelectedNode.Remove();
				UpdateSalvageList(tree.SelectedNode);
			}
		}

		private void salvageTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			UpdateSalvageList(e.Node);
		}

		private void salvageAdd_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(boostLetter.Text) &&  salvageList.SelectedItem != null)
			{
				Salvage s = (Salvage)salvageList.SelectedItem;
				AddSalvageToTree(s, salvageTree.Nodes[(int)s.level]);
			}
		}

        private void setBonusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBonus selected = (SetBonus)setBonusList.SelectedItem;
            setBonusLabel.Text = selected.displayShortHelp + Environment.NewLine + selected.displayHelp;
            bonusAutoPowers.Text = selected.fullName;
        }
    }
}
