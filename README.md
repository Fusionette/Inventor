Inventor
========

This tool can create and edit Invention Sets in a format that the game client can read,
and will also generate recipe and enhancement thumbnail images to go with them.

Usage
-----

When you start the program, it will dump its configuration files to the "config" folder.
This is not very elegant and will be changed in the future, but it is a quick and dirty
way to edit the included resources without much issue.

Some sets are included in the examples folder. Use the Load JSON button to view them.
click the Save JSON button to save your own copy. The Create Data Files button creates
internal files used by the game client, though they can't currently be used for anything.
Images for the set will be dumped in the thumbs folder.

By default, the program will save your process when you exit to autosave.json in the
folder where the program was started. You can disable this by editing the Config.json file.

When entering a Set Name, the Internal Name and Icon Name will be filled automatically.
You can still change them after the fact. If the Icon Name matches a PNG image in the
images folder, the program will use it to render the enhancement and recipe thumbnails.
Note that the image has to be 46x46 pixels in size; anything extra will be cropped.

After entering basic information about the set such as the level, go to the Enhancement
tab in order to start adding enhancements. The first thing you should enter is an
identifier from A to F for this particular enhancement; if one already exists with that
letter, its data will be loaded. Then, you can select an Aspect from the dropdown list
and click the Add button to add it to the Enhancement. An Enhancement can have up to
four aspects. What they do specifically is defined in BoostTypes.json and you can edit
them there or add your own.

The Enhancement Name and Description will be updated every time you add or modify the
Aspects list. Because of this, edit those fields last if you need to.

The Set Bonus tab is very incomplete at the moment, but it does let you manually enter
Set Bonus powers that will activate when you have a certain number of Enhancements.

The Recipes tab is even more incomplete, and the only thing that currently matters is
the Rarity of the set.

Enhancements, Set Bonus and Aspects can be modified by selecting any item and using the
Up and Down keys to move it around; or the Delete key to remove it.
