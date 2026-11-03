using System.Collections.Generic;
using System;
using Sennmetals;

namespace SennmetalsUtil;

public static class GlyphLUT {
    public static Dictionary<Tuple<AtomType, AtomType, AtomType>, AtomType> FixationLUT = [];

    public static void GenerateLUTs()
    {
        GenerateFixationLUT();
    }

    public static void GenerateFixationLUT()
    {
        // Logger.Log("import neumetals 0");
        AtomType mitrum = ImportManager.NeuvolicsAtoms.GetMitrum();
        // Logger.Log("import neumetals 1");
        AtomType iridium = ImportManager.NeuvolicsAtoms.GetIridium();
        // Logger.Log("import neumetals 2");
        AtomType hestium = ImportManager.NeuvolicsAtoms.GetHestium();
        // Logger.Log("import neumetals 3");
        AtomType azulum = ImportManager.NeuvolicsAtoms.GetAzulum();
        // Logger.Log("import neumetals 4");
        AtomType taceum = ImportManager.NeuvolicsAtoms.GetTaceum();
        // Logger.Log("import neumetals done");

        // Logger.Log("import volics");
        AtomType zephiron = ImportManager.NeuvolicsAtoms.GetZephiron();
        AtomType frixon = ImportManager.NeuvolicsAtoms.GetFrixon();
        AtomType gelaron = ImportManager.NeuvolicsAtoms.GetGelaron();

        // Logger.Log("define metals");
        AtomType eitros = Atoms.eitros;
        AtomType sordi = Atoms.sordi;
        AtomType typhor = Atoms.typhor;
        AtomType rofor = Atoms.rofor;
        AtomType serket = Atoms.serket;

        // Sennmetal -> Neumetal
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(eitros, zephiron, zephiron), mitrum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(sordi, zephiron, zephiron), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(typhor, zephiron, zephiron), typhor); // does not transmute to anything new!!
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(rofor, zephiron, zephiron), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(serket, zephiron, zephiron), taceum);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, eitros, zephiron), mitrum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, sordi, zephiron), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, typhor, zephiron), typhor); // does not transmute to anything new!!
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, rofor, zephiron), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, serket, zephiron), taceum);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, eitros), mitrum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, sordi), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, typhor), typhor); // does not transmute to anything new!!
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, rofor), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, serket), taceum);

        // Neumetal -> Sennmetal
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(mitrum, zephiron, zephiron), eitros);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(iridium, zephiron, zephiron), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(hestium, zephiron, zephiron), hestium); //does not transmute to anything new!!
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(azulum, zephiron, zephiron), rofor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(taceum, zephiron, zephiron), serket);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, mitrum, zephiron), eitros);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, iridium, zephiron), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, hestium, zephiron), hestium); //does not transmute to anything new!!
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, azulum, zephiron), rofor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, taceum, zephiron), serket);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, mitrum), eitros);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, iridium), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, hestium), hestium); //does not transmute to anything new!!
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, azulum), rofor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(zephiron, zephiron, taceum), serket);

        // frix sennmetal cycle
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(eitros, frixon, frixon), serket);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(sordi, frixon, frixon), eitros);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(typhor, frixon, frixon), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(rofor, frixon, frixon), typhor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(serket, frixon, frixon), rofor);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, eitros, frixon), serket);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, sordi, frixon), eitros);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, typhor, frixon), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, rofor, frixon), typhor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, serket, frixon), rofor);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, eitros), serket);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, sordi), eitros);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, typhor), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, rofor), typhor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, serket), rofor);

        // gel sennmetal cycle
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(eitros, gelaron, gelaron), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(sordi, gelaron, gelaron), typhor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(typhor, gelaron, gelaron), rofor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(rofor, gelaron, gelaron), serket);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(serket, gelaron, gelaron), eitros);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, eitros, gelaron), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, sordi, gelaron), typhor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, typhor, gelaron), rofor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, rofor, gelaron), serket);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, serket, gelaron), eitros);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, eitros), sordi);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, sordi), typhor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, typhor), rofor);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, rofor), serket);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, serket), eitros);

        // frix neumetal cycle
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(mitrum, frixon, frixon), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(taceum, frixon, frixon), mitrum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(azulum, frixon, frixon), taceum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(hestium, frixon, frixon), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(iridium, frixon, frixon), hestium);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, mitrum, frixon), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, taceum, frixon), mitrum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, azulum, frixon), taceum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, hestium, frixon), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, iridium, frixon), hestium);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, mitrum), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, taceum), mitrum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, azulum), taceum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, hestium), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(frixon, frixon, iridium), hestium);

        // gel neumetal cycle
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(mitrum, gelaron, gelaron), taceum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(taceum, gelaron, gelaron), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(azulum, gelaron, gelaron), hestium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(hestium, gelaron, gelaron), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(iridium, gelaron, gelaron), mitrum);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, mitrum, gelaron), taceum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, taceum, gelaron), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, azulum, gelaron), hestium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, hestium, gelaron), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, iridium, gelaron), mitrum);

        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, mitrum), taceum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, taceum), azulum);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, azulum), hestium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, hestium), iridium);
        FixationLUT.Add(new Tuple<AtomType, AtomType, AtomType>(gelaron, gelaron, iridium), mitrum);
    }
}
