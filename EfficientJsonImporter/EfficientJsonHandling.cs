
namespace EfficientJsonImporter
{


    public class EfficientJsonHandling
    {


        public static void Test()
        {
            
            using (System.IO.FileStream fs = new System.IO.FileStream(@"/root/Downloads/startups.stackexchange.com/Badges.xml", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {

                using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                {

                    using (Newtonsoft.Json.JsonTextReader reader = new Newtonsoft.Json.JsonTextReader(sr))
                    {
                        while (reader.Read())
                        {

                            if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
                            {
                                // Load each object from the stream and do something with it
                                Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Load(reader);
                                System.Console.WriteLine(obj["id"] + " - " + obj["name"]);
                            } // End if (reader.TokenType == JsonToken.StartObject) 

                        } // Whend

                    } // End Using reader

                } // End using sr

            } // End Using fs

        } // End Sub Test


    } // End class EfficientJsonHandling


} // End Namespace EfficientJsonImporter
