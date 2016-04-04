
namespace EfficientJsonImporter
{


    public abstract class TabularData
    {
        public TabularData()
        { }


        public abstract string FileName{ get; }


        public abstract void InsertRow(System.Text.StringBuilder sb);


    }


}

