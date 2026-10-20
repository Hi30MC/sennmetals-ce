using Quintessential;
using SennmetalsParts;
using SennmetalsUtil;
using static Quintessential.PartCycleDelegate;

namespace Sennmetals;

internal static class Parts {

    public static PartType Fixation = ImportManager.NeuvolicsGlyphs.GetFixation();

    public static void AddPartTypes()
    {
        // change fixation description
        Fixation.description = Translations.Translate(FixationGlyph.newFixationString);

        // change render method
        for (int i = 0; i < QApi.PartRenderers.Count; i++)
        {
            QApi.PartRenderers[i] = QApi.PartRenderers[i].Item1
                                   .Invoke(new(Fixation, false)) ?
                                    new(QApi.PartRenderers[i].Item1, FixationGlyph.Render) :
                                    QApi.PartRenderers[i];
        }

        // change cycle method
        Fixation.CycleDelegate = new(PartCycleExecutionType.Normal, FixationGlyph.OnCycle);
    }
}
