using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace AjaxMin.Deployment
{
    class Program
    {
        static int Main(string[] args)
        {
            // get the product version. We include this assembly information in
            // all our projects so they will be the same
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // the first parameter is a folder
            var directoryInfo = new DirectoryInfo(args[0]);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            // the rest of the parameters are nuspec files
            for (var ndx = 1; ndx < args.Length; ++ndx)
            {
                // read the source spec
                string specSource;
                var fileInfo = new FileInfo(args[ndx]);
                using (var reader = new StreamReader(fileInfo.FullName))
                {
                    specSource = reader.ReadToEnd();
                }

                // replace the version variable
                specSource = specSource.Replace("$version$", version);

                // now write the results to the destination folder with the same file name
                using (var writer = new StreamWriter(Path.Combine(directoryInfo.FullName, fileInfo.Name), false, Encoding.UTF8))
                {
                    writer.Write(specSource);
                }
            }

            return 0;
        }
    }
}
