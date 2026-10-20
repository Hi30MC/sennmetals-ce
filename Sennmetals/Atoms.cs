using Quintessential;

namespace Sennmetals;

public static class Atoms {
    public static AtomType eitros, rofor, serket, sordi, typhor;

    public static bool IsSennmetal(AtomType type) {
        return type == Atoms.typhor || type == Atoms.sordi || type == Atoms.eitros || type == Atoms.rofor || type == Atoms.serket;
    }

    public static void LoadAtoms()
    {
        eitros = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/eitros/eitros_difsym"),
            shadow = Assets.textures.atoms.shadow,
            isMetallic = true,
            metallicTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/eitros/eitros_diffuse"),
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/eitros/eitros_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/eitros/eitros_rimlight"),
            },
        };
        rofor = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/rofor/rofor_difsym"),
            shadow = Assets.textures.atoms.shadow,
            isMetallic = true,
            metallicTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/rofor/rofor_diffuse"),
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/rofor/rofor_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/rofor/rofor_rimlight"),
            },
        };
        serket = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/serket/serket_difsym"),
            shadow = Assets.textures.atoms.shadow,
            isMetallic = true,
            metallicTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/serket/serket_diffuse"),
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/serket/serket_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/serket/serket_rimlight"),
            },
        };
        sordi = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/sordi/sordi_difsym"),
            shadow = Assets.textures.atoms.shadow,
            isMetallic = true,
            metallicTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/sordi/sordi_diffuse"),
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/sordi/sordi_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/sordi/sordi_rimlight"),
            },
        };
        typhor = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/typhor/typhor_difsym"),
            shadow = Assets.textures.atoms.shadow,
            isMetallic = true,
            metallicTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/typhor/typhor_diffuse"),
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/typhor/typhor_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/Hi30MC/Sennmetals/typhor/typhor_rimlight"),
            },
        };

        Sennmetals.Instance.AddAtomType(eitros, "eitros");
        Sennmetals.Instance.AddAtomType(rofor, "rofor");
        Sennmetals.Instance.AddAtomType(serket, "serket");
        Sennmetals.Instance.AddAtomType(sordi, "sordi");
        Sennmetals.Instance.AddAtomType(typhor, "typhor");

    }
}
