using CalamityFly.Config;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public class MurasamaUnNerf : GlobalItem {
    public override bool IsLoadingEnabled(Mod mod) {
        return UnNerfsConfig.Instance.murasamaUnnerf;
    }


    public override void SetDefaults(Item item) {
        if (item.type == ModContent.ItemType<Murasama>()) {
            item.damage = 3001;
            item.scale = 2f;
        }
    }

    public override void ModifyWeaponCrit(Item item, Player player, ref float crit) {
        if (item.type == ModContent.ItemType<Murasama>()) {
            crit = 30f; // 30% + 4%
        }
    }
}

public class MurasamaSlashUnNerf : GlobalProjectile {
    public override bool IsLoadingEnabled(Mod mod) {
        return UnNerfsConfig.Instance.murasamaUnnerf;
    }

    public override void SetDefaults(Projectile proj) {
        if (proj.type == ModContent.ProjectileType<MurasamaSlash>()) {
            proj.idStaticNPCHitCooldown = 6;
            proj.scale = 2f;
        }
    }
}