using MonoMod.ModInterop;
using System;
using Quintessential;
using Sennmetals;

namespace Util;

public static class API

{
    public static void DrawIris
    (
        PartRenderer renderer,
        IntermediatePartState partDataWrapper,
        HexIndex offset,
        float time,
        Texture[] irisFrames,
        Maybe<AtomType> risingAtom,
        bool forceOpen = false
    )
    {
        int num = irisFrames.Length - 1;
        bool flag = false;
        Molecule param_ = null;
        Vector2 param_2 = partDataWrapper.pos + HexGrid.standardGrid.ToPixelCoords(offset).Rotated(partDataWrapper.rotation);
        bool flag2 = risingAtom.GetOrDefault(out AtomType param_3);
        renderer.RenderRotating(Assets.textures.parts.animismus.output_under_iris, offset, Vector2.Zero);
        if (forceOpen || flag2)
        {
            num = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * (float)irisFrames.Length), 0, irisFrames.Length - 1);
            if (flag2)
            {
                flag = (double)time > 0.5;
                param_ = Molecule.CreateMonoatomic(param_3);
                if (!flag)
                {
                    Editor.RenderMolecule(param_, param_2, new HexIndex(0, 0), 0f, 1f, time, 1f, isOutputRender: false, null);
                }
            }
        }

        renderer.RenderUpright(irisFrames[num], offset, Vector2.Zero);
        renderer.RenderRotating(Assets.textures.parts.animismus.output_above_iris, offset, Vector2.Zero);
        if (flag)
        {
            Editor.RenderMolecule(param_, param_2, new HexIndex(0, 0), 0f, 1f, time, 1f, isOutputRender: false, null);
        }
    }

    public static Maybe<T> ConvertToMaybe<T>(T data)
    {
        if (data != null)
        {
            return Maybe<T>.From(data);
        }

        return MaybeHelper.empty;
    }
}

public class ImportManager
{
    public static void ImportNeuvolics()
    {
        typeof(NeuvolicsAtoms).ModInterop();
        typeof(NeuvolicsGlyphs).ModInterop();
        typeof(NeuvolicsFixationTextures).ModInterop();
        typeof(NeuvolicsSounds).ModInterop();
    }

    [ModImportName("Neuvolics.Atoms")]
    public static class NeuvolicsAtoms
    {
        public static Func<AtomType> GetZephiron;
        public static Func<AtomType> GetFrixon;
        public static Func<AtomType> GetGelaron;

        public static Func<AtomType> GetMitrum;
        public static Func<AtomType> GetIridium;
        public static Func<AtomType> GetHestium;
        public static Func<AtomType> GetAzulum;
        public static Func<AtomType> GetTaceum;

        public static Func<AtomType, int> GetNeumetalIndex;
    }

    [ModImportName("Neuvolics.Glyphs")]
    public static class NeuvolicsGlyphs
    {
        public static Func<PartType> GetFixation;
    }

    [ModImportName("Neuvolics.Sounds")]
    public static class NeuvolicsSounds
    {
        public static Func<Sound> GetFixationSound;
    }

    [ModImportName("Neuvolics.Textures.Fixation")]
    public static class NeuvolicsFixationTextures
    {
        public static Func<Texture> GetFixationBase;
        public static Func<Texture[]> GetFixationNets;
        public static Func<Texture> GetFixationConnectors;
        public static Func<Texture> GetFixationHoleBar;
        public static Func<Texture> GetFixationHoleNeumetalActive;
        public static Func<Texture> GetFixationHoleNeumetalInactive;
        public static Func<Texture> GetFixationHoleVolicActive;
        public static Func<Texture> GetFixationHoleFrixonHalfActive;
        public static Func<Texture> GetFixationHoleFrixonInactive;
        public static Func<Texture> GetFixationHoleGelaronHalfActive;
        public static Func<Texture> GetFixationHoleGelaronInactive;

        public static Func<Texture[]> GetFixationZephironIris;
        public static Func<Texture[]> GetFixationNeumetalIris;
    }
}

public class ExportManager() {
    public static void ExportAtoms()
    {
        typeof(AtomExports).ModInterop();
    }

    [ModExportName("Sennmetals.Atoms")]
    public static class AtomExports
    {
        public static AtomType GetEitros() => Atoms.eitros;
        public static AtomType GetRofor() => Atoms.rofor;
        public static AtomType GetSerket() => Atoms.serket;
        public static AtomType GetSordi() => Atoms.sordi;
        public static AtomType GetTyphor() => Atoms.typhor;

        public static bool IsSennmetal(AtomType sennmetal) => Atoms.IsSennmetal(sennmetal);
    }
}
