using System.ComponentModel;

namespace StudentAdministrationSystem.Extensions
{
    public enum Result
    {
        [Description("PASS")]
        PASS,
        [Description("PASS COMPENSATION")]
        PASSCOMPENSATION,
        [Description("FAIL")]
        FAIL,
        [Description("DISTINCTION")]
        DISTINCTION
    }
}