
namespace EfficientJsonImporter
{

    using System.IO;
    using Newtonsoft;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;


    public class EfficientJsonHandling
    {
        public EfficientJsonHandling()
        { }


        public static void Test()
        {
            
            using (FileStream fs = new FileStream(@"/root/Downloads/startups.stackexchange.com/Badges.xml", FileMode.Open, FileAccess.Read))
            { 
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonToken.StartObject)
                            {
                                // Load each object from the stream and do something with it
                                JObject obj = JObject.Load(reader);
                                System.Console.WriteLine(obj["id"] + " - " + obj["name"]);

                            }
                        } // Whend

                    } // End Using reader

                } // End using sr

            } // End Using fs

        } // End Sub 


    } // End class EfficientJsonHandling


} // End Namespace EfficientJsonImporter
