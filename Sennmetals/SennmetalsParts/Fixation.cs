using Quintessential;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SennmetalsUtil;
using Sennmetals;

namespace SennmetalsParts;

public static class FixationGlyph
{
    #region GlyphMeta
    public static readonly HexIndex FixationHole1Hex = new(-1, 0);
    public static readonly HexIndex FixationHole2Hex = new(0, 0);
    public static readonly HexIndex FixationHole3Hex = new(1, 0);
    public static readonly HexIndex FixationZephironIrisHex = new(1, -2);
    public static readonly HexIndex FixationNeumetalIrisHex = new(-1, 2);

    public static readonly Texture FixationHoleZephironHalfActive = AssetLoaderHelper.LoadTexture("textures/parts/Hi30MC/Sennmetals/zephiron_half_active");
    public static readonly Texture FixationHoleZephironInactive = AssetLoaderHelper.LoadTexture("textures/parts/Hi30MC/Sennmetals/zephiron_inactive");
    public static readonly Texture FixationHoleSenneumetalActive = AssetLoaderHelper.LoadTexture("textures/parts/Hi30MC/Sennmetals/senneumetal_active");
    public static readonly Texture FixationHoleSennmetalInactive = AssetLoaderHelper.LoadTexture("textures/parts/Hi30MC/Sennmetals/sennmetal_inactive");

    public static readonly string oldFixationString = ImportManager.NeuvolicsGlyphs.GetFixation().description;
    public static readonly string newFixationString = Regex.Replace(oldFixationString, "neu", "senneu")
                                                    + " Inputting a pair of zephiron translates between most senneumetallic pairs.";
    #endregion

    internal static HashSet<HexIndex> OccupiedHexes = [];
    public static bool OccupiedHexesStale = true;

    public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
    {
        // hold on to your butts!
        // Logger.Log("hold on to your butts");

        PartSimState pss = editor.GetSimulation().GetSimState(part);
        IntermediatePartState partDataWrapper = editor.GetIntermState(part, pos);
        float time = editor.GetCycleTime();

        if (OccupiedHexesStale)
        {
            OccupiedHexes.Clear();
            OccupiedHexes.UnionWith(editor.GetSolution().GetOccupiedHexes(MaybeHelper.empty, (PartLayer)0));
            OccupiedHexesStale = false;
        }

        int invertedNetMask = 0;

        HexIndex[] nettingHexes = new HexIndex[4] { new(0, 1), new(-1, 1), new(0, -1), new(1, -1) };

        // Logger.Log("check if live");
        IScreen TOS = GameLogic.instance.GetCurrentScreen();

        if (TOS is not SolutionEditorScreen)
        {
            // Not interacting with it, not dragging
            goto NetRemoval;
        }

        IInputMode mode = ((SolutionEditorScreen)TOS).currentInputMode;

        if (mode is not PartDraggingInputMode)
        {
            // not dragging at all
            goto NetRemoval;
        }


        // Logger.Log("check dragging");
        PartDraggingInputMode drag = (PartDraggingInputMode)mode;
        if (((List<PartDraggingInputMode.DraggedPart>)typeof(PartDraggingInputMode).GetField("draggedParts", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(drag)).Any((d) => d.part == part))
        {
            // we are dragging, and this part is in it.
            goto FixationDrawing;
        }
        // Logger.Log("remove nets");
    NetRemoval:
        for (int i = 0; i < nettingHexes.Length; i++)
        {
            if (OccupiedHexes.Contains(part.InFrontBy(nettingHexes[i])))
            {
                // if hex contains a part, disable it
                invertedNetMask |= 1 << i;
            }
        }

        // Logger.Log("draw fix");
    FixationDrawing:
        // SNDZGF
        int atomsPresent = 0b000000;
        HexIndex[] holes = new HexIndex[] { FixationHole1Hex, FixationHole2Hex, FixationHole3Hex };
        foreach (HexIndex h in holes)
        {
            foreach (Molecule m in editor.GetSimulation().GetMolecules())
            {
                if (m.GetAtoms().Count == 1 && m.GetAtoms().TryGetValue(part.InFrontBy(h), out Atom a))
                {
                    // Logger.Log("if atom on top?");
                    AtomType aT = a.atomType;
                    if (aT == ImportManager.NeuvolicsAtoms.GetFrixon())
                    {
                        atomsPresent |= (atomsPresent & 0b000001) != 0 ? 0b001000 : 0b000001;
                    }
                    else if (aT == ImportManager.NeuvolicsAtoms.GetGelaron())
                    {
                        atomsPresent |= (atomsPresent & 0b000010) != 0 ? 0b001000 : 0b000010;
                    }
                    else if (aT == ImportManager.NeuvolicsAtoms.GetZephiron())
                    {
                        atomsPresent |= (atomsPresent & 0b000100) != 0 ? 0b001000 : 0b000100;
                    }
                    else if (ImportManager.NeuvolicsAtoms.GetNeumetalIndex(aT) != -1)
                    {
                        atomsPresent |= 0b010000;
                    }
                    else if (Atoms.IsSennmetal(aT))
                    {
                        atomsPresent |= 0b100000;
                    }
                }
            }
        }

        // Logger.Log("drawing shit");
        Vector2 pivot = new(122, 191);
        Vector2 offset = Vector2.Zero;
        // Logger.Log("draw base");
        renderer.RenderBase(ImportManager.NeuvolicsFixationTextures.GetFixationBase(), offset, pivot, 0);

        for (int i = 0; i < nettingHexes.Length; i++)
        {
            if (((invertedNetMask >> i) & 1) == 0)
            {
                // Logger.Log("draw net" + i);
                renderer.RenderBase(ImportManager.NeuvolicsFixationTextures.GetFixationNets()[i], offset, pivot, 0);
            }
        }

        // input rendering
        // Logger.Log("input rendering");
        {
            Texture neumetalReadout = FixationHoleSenneumetalActive;
            // Logger.Log("Checking atomsPresent for Senneumetals: " + (atomsPresent & 0b110000));
            switch (atomsPresent & 0b110000)
            {      //0bSNDZGF
                case 0b010000:
                    // neumetal x1
                    neumetalReadout = ImportManager.NeuvolicsFixationTextures.GetFixationHoleNeumetalInactive();
                    break;
                case 0b100000:
                    // senneumetal x1
                    neumetalReadout = FixationHoleSennmetalInactive;
                    break;
                default:
                    // 0b000000: none, so keep default.
                    // 0b110000: impossible case.
                    break;
            }

            Texture volicReadout = ImportManager.NeuvolicsFixationTextures.GetFixationHoleVolicActive();
            // Logger.Log("Checking atomsPresent for Volics: " + (atomsPresent & 0b001111));
            switch (atomsPresent & 0b1111)
            {     // 0bDZGF
                case 0b0001:
                    // frixon x1
                    volicReadout = ImportManager.NeuvolicsFixationTextures.GetFixationHoleFrixonHalfActive();
                    break;
                case 0b0010:
                    // gelaron x1
                    volicReadout = ImportManager.NeuvolicsFixationTextures.GetFixationHoleGelaronHalfActive();
                    break;
                case 0b0100:
                    // zephiron x1
                    volicReadout = FixationHoleZephironHalfActive;
                    break;
                case 0b1001:
                    // frixon x2
                    volicReadout = ImportManager.NeuvolicsFixationTextures.GetFixationHoleFrixonInactive();
                    break;
                case 0b1010:
                    // gelaron x2
                    volicReadout = ImportManager.NeuvolicsFixationTextures.GetFixationHoleGelaronInactive();
                    break;
                case 0b1100:
                    //zephiron x2
                    volicReadout = FixationHoleZephironInactive;
                    break;
                default:
                    // 0000: no volics are present.
                    // 0011: 1 frixon and 1 gelaron are present, invalid.
                    // 0101: 1 frixon and 1 zephiron are present, invalid.
                    // 0110: 1 gelaron and 1 zephiron are present, invalid.
                    // 0111: 1 frixon, 1 gelaron, 1 zephiron are present, invalid.
                    // 1000: impossible state. (double with no atoms on)
                    // 1011: 2 frix 1 gel or vice versa, invalid
                    // 1101: 2 zeph 1 frix or vice versa, invalid
                    // 1110: 2 zeph 1 gel or vice versa, invalid.
                    // 1111: impossible state (double with all three atoms on, impossible by pidgeonhole).
                    break;
            }
            foreach (HexIndex h in holes)
            {
                renderer.RenderShadowCircle(Assets.textures.parts.projection_glyph.quicksilver_input, h, 0);
                renderer.RenderUpright(ImportManager.NeuvolicsFixationTextures.GetFixationHoleBar(), h, Vector2.Zero);
                renderer.RenderUpright(neumetalReadout, h, Vector2.Zero);
                renderer.RenderUpright(volicReadout, h, Vector2.Zero);
            }
        }

        // irises
        // Logger.Log("draw irises");
        API.DrawIris(renderer, partDataWrapper, FixationZephironIrisHex, time,
                               ImportManager.NeuvolicsFixationTextures.GetFixationZephironIris(),
                               pss.isProcessing ? API.ConvertToMaybe(pss.processingAtoms[0]) : MaybeHelper.empty);

        API.DrawIris(renderer, partDataWrapper, FixationNeumetalIrisHex, time,
                               ImportManager.NeuvolicsFixationTextures.GetFixationNeumetalIris(),
                               pss.isProcessing ? API.ConvertToMaybe(pss.processingAtoms[1]) : MaybeHelper.empty);

        renderer.RenderBase(ImportManager.NeuvolicsFixationTextures.GetFixationConnectors(), offset, pivot, 0);
    }

    public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first) {
        if (first)
        {
            // Logger.Log("fixation found");
            HexIndex hLeft = part.InFrontBy(FixationHole1Hex);
            HexIndex hCenter = part.InFrontBy(FixationHole2Hex);
            HexIndex hRight = part.InFrontBy(FixationHole3Hex);
            HexIndex iZ = part.InFrontBy(FixationZephironIrisHex);
            HexIndex iN = part.InFrontBy(FixationNeumetalIrisHex);

            if (sim.FindAtom(iZ).HasValue()
                || sim.FindAtom(iN).HasValue()) // blocked
            {
                return false;
            }
            // Logger.Log("Valid Outputs");
            if (!sim.FindAtom(hLeft).GetOrDefault(out AtomReference hLeftAtom)      // no left
                || !sim.FindAtom(hCenter).GetOrDefault(out AtomReference hCenterAtom)  // no center
                || !sim.FindAtom(hRight).GetOrDefault(out AtomReference hRightAtom) // no right
                || hLeftAtom.inMultiAtomMolecule // left bondy
                || hLeftAtom.isHeldByArm // left grabby
                || hLeftAtom.inMultiAtomMolecule // center bondy
                || hLeftAtom.isHeldByArm // center grabby
                || hLeftAtom.inMultiAtomMolecule // right bondy
                || hLeftAtom.isHeldByArm) // right grabby
            {
                return false;
            }
            // Logger.Log("Valid Inputs"  + " " +  hLeftAtom.field_2280.ToString()  + " " +   hRightAtom.field_2280.ToString()  + " " +   hCenterAtom.field_2280.ToString());
            if (!GlyphLUT.FixationLUT.TryGetValue(
                new Tuple<AtomType, AtomType, AtomType>(hLeftAtom.atomType, hRightAtom.atomType, hCenterAtom.atomType),
                out AtomType output))
            {
                return false;
            }
            // Logger.Log("Valid Output");
            Logger.Log(output.ToString());

            hLeftAtom.molecule.RemoveAtom(hLeftAtom.pos);
            hCenterAtom.molecule.RemoveAtom(hCenterAtom.pos);
            hRightAtom.molecule.RemoveAtom(hRightAtom.pos);
            // Logger.Log("deleted atoms");

            sim.solutionEditor.DrawFallingAtom(hLeftAtom);
            sim.solutionEditor.DrawFallingAtom(hCenterAtom);
            sim.solutionEditor.DrawFallingAtom(hRightAtom);
            // Logger.Log("drew falling atoms");

            sim.SpawnColliderRelative(part, FixationZephironIrisHex);
            sim.SpawnColliderRelative(part, FixationNeumetalIrisHex);
            // Logger.Log("Add Small Collider falling atoms");

            pss.isProcessing = true;
            pss.processingAtoms = new AtomType[2] { ImportManager.NeuvolicsAtoms.GetZephiron(), output };
            // Logger.Log("sent to render");
            ImportManager.NeuvolicsSounds.GetFixationSound().PlaySound(sim.solutionEditor.method_506());
            // Logger.Log("sound played");
            return true;
        } else if (pss.isProcessing)
        {
            if (pss.processingAtoms[0] is not null)
            {
                sim.SpawnAtomRelative(pss.processingAtoms[0], part, FixationZephironIrisHex);
            }
            if (pss.processingAtoms[1] is not null)
            {
                sim.SpawnAtomRelative(pss.processingAtoms[1], part, FixationNeumetalIrisHex);
            }
            return true;
        }
        return false;
    }
}
