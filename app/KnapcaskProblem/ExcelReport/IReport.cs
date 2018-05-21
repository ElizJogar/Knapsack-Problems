using System.IO;

namespace ExcelReport
{
    public interface IReport
    {
        void Create();
        DirectoryInfo GetDir();
    }
}
