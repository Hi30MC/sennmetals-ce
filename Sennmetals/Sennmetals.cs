using Quintessential;
using SennmetalsUtil;

namespace Sennmetals;

internal class Sennmetals : QuintessentialMod {
    public override string ModId => "sennmetals";
    public static Sennmetals Instance { get; }

    public override void Load() {
        Logger.Log("Sennmetals: Opposing cultural norms!");
    }

    public override void LoadContent() {
        Logger.Log("Sennmetals: Holding a mirror to the Neumetals...");
        Atoms.LoadAtoms();
        ExportManager.ExportAtoms();
        ImportManager.ImportNeuvolics();
        Parts.AddPartTypes();
        GlyphLUT.GenerateLUTs();
        Logger.Log("Sennmetals: Neumetals crying at their reflection, transition done!");
    }
    public override void LoadCompatContent() { }
    public override void FinaliseContent() { }

    public override void PostLoad() { }
    public override void Unload() {
        Logger.Log("Sennmetals: Senneumetals going to bed, o lape pona!");
    }
}
