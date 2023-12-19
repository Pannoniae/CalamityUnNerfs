using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public class TooltipEditor : GlobalItem {
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
        #region utils

        // These functions are shorthand to invoke ApplyTooltipEdits using the above predicates.
        void EditTooltipByNum(int lineNum, Action<TooltipLine> action) =>
            ApplyTooltipEdits(tooltips, LineNum(lineNum), action);

        // This is a modular tooltip editor which loops over all tooltip lines of an item,
        // selects all those which match an arbitrary function you provide,
        // then edits them using another arbitrary function you provide.
        void ApplyTooltipEdits(IList<TooltipLine> lines, Func<Item, TooltipLine, bool> predicate,
            Action<TooltipLine> action) {
            foreach (TooltipLine line in lines)
                if (predicate.Invoke(item, line))
                    action.Invoke(line);
        }

        // This function produces simple predicates to match a specific line of a tooltip, by number/index.
        Func<Item, TooltipLine, bool> LineNum(int n) =>
            (i, l) => l.Mod == "Terraria" && l.Name == $"Tooltip{n}";

        // This function produces simple predicates to match a specific line of a tooltip, by name.
        Func<Item, TooltipLine, bool> LineName(string s) =>
            (i, l) => l.Mod == "Terraria" && l.Name == s;

        #endregion

        if (item.type == ItemID.EmpressFlightBooster) {
            EditTooltipByNum(0, line => line.Text = "Grants infinite wing and rocket boot flight");
        }

        if (item.type == ItemID.WormScarf) {
            EditTooltipByNum(0, line => line.Text = line.Text.Replace("10%", "17%"));
        }

        // Yoyo Glove/Bag apply a 0.5x damage multiplier on the second yoyo
        if (item.type == ItemID.YoyoBag || item.type == ItemID.YoYoGlove) {
            EditTooltipByNum(0, line => {
                var pos = line.Text.LastIndexOf('\n');
                line.Text = line.Text[..pos];
                //line.Text += "\nSecondary yoyos will do 50% less damage";
            });
        }
    }
}